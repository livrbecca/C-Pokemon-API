using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokemon2.Models.ModelsForTranslator
{
    public class Success
    {

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
