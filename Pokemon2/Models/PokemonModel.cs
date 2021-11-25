using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonAPI.Models
{
    public class PokemonModel
    {
        public PokemonModel(PokemonSpeciesModel pokemonSpecies)
        {
            Name = pokemonSpecies.Name;

            // note: find elsewhere to put this logic, make it generic for different languages
            if (pokemonSpecies.FlavorTextEntries != null)
            {
                bool IsLanguageEnglish = pokemonSpecies.FlavorTextEntries.Any(x => x.Language.Name == "en");


                if (IsLanguageEnglish)
                {
                    Description = pokemonSpecies.FlavorTextEntries.First(x => x.Language.Name == "en").FlavorText?.Replace("\n", " ").Replace("\f", " ");
                }
                else
                {
                    Description = "No description found...Rare.";
                }
            }
            else
            {
                Description = "No description found...Rare.";
            }

            if (pokemonSpecies.Habitat != null)
            {
                Habitat = pokemonSpecies.Habitat.Name;
            }
            
            IsLegendary = pokemonSpecies.IsLegendary;
           
           
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }

        protected bool Equals(PokemonModel other)
        {
            return Name == other.Name && Description == other.Description && Habitat == other.Habitat && IsLegendary == other.IsLegendary;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PokemonModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Habitat, IsLegendary);
        }
    }
}
