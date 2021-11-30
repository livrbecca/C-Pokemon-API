using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PokemonAPI.BaseResponse;
using PokemonAPI.Controllers;
using PokemonAPI.Models;
using PokemonAPI.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon2.Unit.Tests
{
    public class PokemonControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Return_OK_Status_Code_If_Request_Successful()
        {

            // arrange
            var mockPokemonService = new Mock<IPokemonService>();
        
            var eeveeSpeciesModel = new PokemonSpeciesModel()
            {
                Name = "eevee"
            };
            
            var returnedResult = new Result<PokemonSpeciesModel>()
            {
                Data = eeveeSpeciesModel,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                ErrorMessage = string.Empty,
            };
            
            var pokemon =  new PokemonModel(returnedResult.Data, mockPokemonService.Object); // should have name of eevee

            mockPokemonService.Setup(x => x.GetPokemonSpeciesData(It.IsAny<string>())).ReturnsAsync(returnedResult);


            // the service being tested is the controller (SUT)
            var serviceBeingTested = new PokemonController(mockPokemonService.Object);

            // act
            var getRequestResult = await serviceBeingTested.GetPokemonAsync("eevee"); // calling = act
            getRequestResult.ShouldNotBeNull(); // should always check for this
            getRequestResult.ShouldBeOfType<OkObjectResult>();
            var okObject = getRequestResult as OkObjectResult;

            // getRequestResult.result.value.name
            var testValue = getRequestResult;

            okObject.StatusCode.ShouldBe(200);
           
            Assert.AreEqual(pokemon, okObject.Value);
            //pokemon.Name.ShouldBeOfType<int>();
        }

        [Test]
        public async Task Should_Return_404_Status_Code_If_Request_Unsuccessful()
        {

            var speciesModel = new PokemonSpeciesModel()
            {
                Name = "0fj35r23wrf",
                
            };

            var returnedResult = new Result<PokemonSpeciesModel>()
            {
                ErrorMessage = $"Could not find: the pokemon {speciesModel.Name}\nplease try again.", // is this not testing my hard-coded string instead of the one in the controller?
                HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                Data = speciesModel, // breaks if you change this to null, double check why
            };
           
            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(x => x.GetPokemonSpeciesData(It.IsAny<string>())).ReturnsAsync(returnedResult); // returns a Result of type PokemonSpeciesModel
            
            // SUT
            var SUT = new PokemonController(mockPokemonService.Object);

            var pokemon = new PokemonModel(returnedResult.Data, mockPokemonService.Object);

            // act
            var getRequestResults = await SUT.GetPokemonAsync("0fj35r23wrf");
            getRequestResults.ShouldNotBeNull();
            getRequestResults.ShouldBeOfType<ObjectResult>();

            var objectResult = getRequestResults as ObjectResult;
            objectResult.StatusCode.ShouldBe(404);
            returnedResult.ErrorMessage.ShouldContain($"Could not find: the pokemon {speciesModel.Name}\nplease try again.");

        }

        [Test]
        public async Task Null_Input_Handled()
        {
            var mockPokemonService = new Mock<IPokemonService>();
            var SUT = new PokemonController(mockPokemonService.Object);
            var getRequestResults = await SUT.GetPokemonAsync(null);

            getRequestResults.ShouldBeOfType<ObjectResult>();
            var objectResult = getRequestResults as ObjectResult;
            objectResult.StatusCode.ShouldBe(400);

        }


        // test the catch block
        // issues: always 404, unsure how to get into catch block
    }
}
