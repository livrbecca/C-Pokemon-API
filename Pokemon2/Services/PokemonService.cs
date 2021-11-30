using PokemonAPI.BaseResponse;
using PokemonAPI.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {


        public async Task<Result<PokemonSpeciesModel>> GetPokemonSpeciesData(string pokemonName)
        {

           
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://pokeapi.co")
                };


                // test result is a HttpResponseMessage
                var result = await client.GetAsync($"/api/v2/pokemon-species/{pokemonName.ToLower()}"); // still leaves uppercase in url bar

               
            
                // test for exact error message string
                if (!result.IsSuccessStatusCode)
                {
                    return new Result<PokemonSpeciesModel>
                    {
                        HttpStatusCode = result.StatusCode,
                        ErrorMessage = $"Could not find: the pokemon {pokemonName}\nplease try again.",
                        Data = null
                    };
                }

                string resultContent = await result.Content.ReadAsStringAsync();
                var detailsFromAPI = JsonSerializer.Deserialize<PokemonSpeciesModel>(resultContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

               // var setDescription = SetDescriptionAndLanguage(detailsFromAPI, "en");

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


        public string SetDescriptionAndLanguage(PokemonSpeciesModel pokemonSpecies, string languageAbbreviation)
        {
            string Description = string.Empty;

            if (pokemonSpecies.FlavorTextEntries != null)
            {
                bool IsLanguageSelectedValid = pokemonSpecies.FlavorTextEntries.Any(x => x.Language.Name == $"{languageAbbreviation}");


                if (IsLanguageSelectedValid)
                {
                    return Description = pokemonSpecies.FlavorTextEntries.First(x => x.Language.Name == $"{languageAbbreviation}").FlavorText?.Replace("\n", " ").Replace("\f", " ");
                }
                else
                {
                   return  Description = "No description found...Rare.";
                }
            }
            else
            {
              return   Description = "No description found...Rare.";
            }
        }

    }

   
}
