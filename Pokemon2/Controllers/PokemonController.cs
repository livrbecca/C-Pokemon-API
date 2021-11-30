using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon2.Services;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace PokemonAPI.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class PokemonController : ControllerBase // A base class for an MVC controller without view support.
    {
  
        private readonly IPokemonService _pokemonService;
       private readonly ITranslatorService _translator;

        public PokemonController(IPokemonService pokemonService, ITranslatorService translator)
        //ITranslator translator
        {
            _pokemonService = pokemonService;
           _translator = translator;
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<IActionResult> GetTranslatedPokemonAsync(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "input cannot tbe empty");

            }

            try
            {
                var response = await _pokemonService.GetPokemonSpeciesData(pokemonName);
                if (!response.RequestIsSuccssful)
                {
                    return StatusCode((int)response.HttpStatusCode, response.ErrorMessage);
                }
                var pokemonSpecies = response.Data;
                var pokemon = new PokemonModel(pokemonSpecies, _pokemonService);
                var shouldApplyYodaTranslation = pokemon.Habitat == "cave" || pokemon.IsLegendary;

                if (shouldApplyYodaTranslation)
                {
                    pokemon.Description = await _translator.TranslateToYoda(pokemon.Description);
                }
                else
                {
                   pokemon.Description = await _translator.TranslateToShakespeare(pokemon.Description);
                }
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet("{pokemonName}")] 
        public async Task<IActionResult> GetPokemonAsync (string pokemonName)
        // IActionResult is an interface, we can create a custom response as a return, when you use ActionResult you can return only predefined ones for returning a View or a resource. 
        // With IActionResult we can return a response, or error as well. 
        
        {
            if (string.IsNullOrEmpty(pokemonName)){
                return StatusCode((int)HttpStatusCode.BadRequest, "input cannot tbe empty");

            }

            try
            {
                // functioality that connects to the pokeapi and returns data- from service
                var response = await _pokemonService.GetPokemonSpeciesData(pokemonName);
                if (!response.RequestIsSuccssful)
                {
                    return StatusCode((int)response.HttpStatusCode, response.ErrorMessage);
                }
                var pokemonSpecies = response.Data;
                var pokemon = new PokemonModel(pokemonSpecies, _pokemonService);
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       

    }
    
}

