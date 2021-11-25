using NUnit.Framework;
using PokemonAPI.Models;
using PokemonAPI.Models.ModelsForSpecies.LanguageFlavourText;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon2.Unit.Tests
{
    public class PokemonModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_Should_Work()
        {
            var model = new PokemonModel(new PokemonSpeciesModel());

            model.ShouldNotBeNull();
        }
        [Test]
        public void Constructor_Should_Set_Name()
        {
            var name = "eevee";
            var model = new PokemonModel(new PokemonSpeciesModel { Name = name } );

            model.Name.ShouldBe(name);
        }


        [Test]
        public void Constructor_Should_Set_Habitat()
        {
            var name = "cave";
            var model = new PokemonModel(new PokemonSpeciesModel { Habitat = new HabitatModel { Name = name } } );

            model.Habitat.ShouldBe(name);
        }

        [Test]
        public void Constructor_Should_Default_isLegendary_to_false()
        {

            var model = new PokemonModel(new PokemonSpeciesModel());

            model.IsLegendary.ShouldBeFalse();
        }

        [Test]
        public void Constructor_Should_Set_isLegendary()
        {
            
            var model = new PokemonModel(new PokemonSpeciesModel { IsLegendary = true });

            model.IsLegendary.ShouldBe(true);
        }


        [Test]
        public void Constructor_Should_Set_NotFound_Text_If_FlavourTextEntries_Null()
        {

            // arrange
            var model = new PokemonModel(new PokemonSpeciesModel());
            var speciesModel = new PokemonSpeciesModel();

            // act
            speciesModel.FlavorTextEntries = null;

            // assert
            model.Description.ShouldBe("No description found...Rare.");
        }

        [Test] // not passing for the right reasons
        public void Constructor_Should_Remove_Line_Breaks_From_Description()
        {

            // arrange
            string description = "sffsfsdf\nsfsdsdfs";
            string expectedDescription = "sffsfsdf sfsdsdfs";

            var FlavTextEntry = new FlavorTextEntryModel { FlavorText = description, Language = new LanguageFlavorModel { Name = "en" } };

            var pokemonSpeciesModel = new PokemonSpeciesModel { FlavorTextEntries = new List<FlavorTextEntryModel> {
                FlavTextEntry
            } };

            // act
            var model = new PokemonModel(pokemonSpeciesModel);


            // assert

            model.Description.ShouldBe(expectedDescription);
            
        }


    }
}