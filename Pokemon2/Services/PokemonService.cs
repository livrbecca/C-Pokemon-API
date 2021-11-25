using PokemonAPI.BaseResponse;
using PokemonAPI.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {


        public async Task<Result<PokemonSpeciesModel>> GetPokemonSpeciesData(string pokemonName)
        {

            //pokemonName = pokemonName.ToLower(); // leaves uppercase in URL bar 

            /// if char[0] of pokemonName is upper case, change to lowercase or 301 redirect
            /// 
            // if pokemonName is capital letter
            // try 301 redirct to find lowercase version
            // else: code below
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://pokeapi.co")
                };

                var result = await client.GetAsync($"/api/v2/pokemon-species/{pokemonName.ToLower()}"); // still leaves uppercase in url bar

               
            

                if (!result.IsSuccessStatusCode)
                {
                    return new Result<PokemonSpeciesModel>
                    {
                        HttpStatusCode = result.StatusCode,
                        ErrorMessage = "Generic error message to be improved",
                        Data = null
                    };
                }

                string resultContent = await result.Content.ReadAsStringAsync();
                var detailsFromAPI = JsonSerializer.Deserialize<PokemonSpeciesModel>(resultContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return new Result<PokemonSpeciesModel>
                {
                    Data = detailsFromAPI,
                    HttpStatusCode = result.StatusCode,
                    ErrorMessage = string.Empty
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: error finding {pokemonName}");
                return new Result<PokemonSpeciesModel>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = "interal error",
                    Data = null
                };
            }

        }

     
    }
}
