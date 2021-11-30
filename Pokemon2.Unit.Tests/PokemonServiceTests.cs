using Moq;
using NUnit.Framework;
using PokemonAPI.Models;
using PokemonAPI.Models.ModelsForSpecies.LanguageFlavourText;
using PokemonAPI.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon2.Unit.Tests
{
    public class PokemonServiceTests
    {
        [Test]
        public void Constructor_Should_Remove_Line_Breaks_From_Description()
        {

            // arrange
            string description = "sffsfsdf\nsfsdsdfs";
            var FlavTextEntry = new FlavorTextEntryModel { FlavorText = description, Language = new LanguageFlavorModel { Name = "en" } };

            var pokemonSpeciesModel = new PokemonSpeciesModel
            {
                FlavorTextEntries = new List<FlavorTextEntryModel> 
                {
                FlavTextEntry
                }
            };
            string expectedDescription = "sffsfsdf sfsdsdfs";
            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(x => x.SetDescriptionAndLanguage(pokemonSpeciesModel, "en")).Returns(expectedDescription);

            
            // act
            var model = new PokemonModel(pokemonSpeciesModel, mockPokemonService.Object);


            // assert

            model.Description.ShouldBe(expectedDescription);

        }
    }
}
