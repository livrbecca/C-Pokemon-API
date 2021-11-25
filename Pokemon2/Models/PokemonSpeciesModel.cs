using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{

    public class PokemonSpeciesModel    
    {
        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntryModel> FlavorTextEntries { get; set; } // Actually an array / list, will need to be changed. define shape - 

 
        [JsonPropertyName("habitat")]
        public HabitatModel Habitat { get; set; } // define shape - done


        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

    
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
