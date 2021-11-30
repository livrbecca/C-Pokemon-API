using PokemonAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonAPI.Models
{
    public class PokemonModel
    {
        private readonly IPokemonService _pokemonService;

        public PokemonModel(PokemonSpeciesModel pokemonSpecies, IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
            Description = _pokemonService.SetDescriptionAndLanguage(pokemonSpecies, "en");
            Name = pokemonSpecies.Name;


            if (pokemonSpecies.Habitat != null)
            {
                Habitat = pokemonSpecies.Habitat.Name;
            }
            else
            {
                Habitat = "Habitat unknown...";
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
