using PokemonAPI.BaseResponse;
using PokemonAPI.Models;

using System.Threading.Tasks;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Task<Result<PokemonSpeciesModel>> GetPokemonSpeciesData(string pokemonName);
        string SetDescriptionAndLanguage(PokemonSpeciesModel pokemonSpecies, string languageAbbreviation);
    }
}
