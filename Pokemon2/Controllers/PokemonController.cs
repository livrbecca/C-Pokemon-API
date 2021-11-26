using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System;
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
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }


        [HttpGet("{pokemonName}")] 
        public async Task<IActionResult> GetPokemonAsync (string pokemonName)
        // IActionResult is an interface, we can create a custom response as a return, when you use ActionResult you can return only predefined ones for returning a View or a resource. 
        // With IActionResult we can return a response, or error as well. 
        {
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

