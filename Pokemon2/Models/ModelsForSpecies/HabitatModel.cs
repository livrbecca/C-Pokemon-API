﻿using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class HabitatModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
