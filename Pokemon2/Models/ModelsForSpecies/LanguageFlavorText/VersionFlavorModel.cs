using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models.ModelsForSpecies.LanguageFlavourText
{
   public class VersionFlavorModel
    {
        // identical to the Language model, re-factor???
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
