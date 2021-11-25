using PokemonAPI.Models.ModelsForSpecies.LanguageFlavourText;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class FlavorTextEntryModel
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

      
        [JsonPropertyName("language")]
        public LanguageFlavorModel Language { get; set; } // needs to be defined

       
        [JsonPropertyName("version")]
        public VersionFlavorModel Version { get; set; } // needs to be defined
    }
}
