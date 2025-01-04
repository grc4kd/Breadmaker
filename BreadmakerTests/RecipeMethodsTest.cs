using System.Diagnostics;
using System.Reflection;
using Breadmaker;

namespace BreadmakerTests;

public class RecipeMethodsTest
{
    [Fact]
    public void TestBasicRecipe()
    {
        // create a basic sourdough recipe from output parameters for the final dough
        var recipe = new SourdoughRecipe(starterMass: 289, starterHydration: 1, finalHydration: 0.68, finalMass: 1000);

        // the calculator should output a list similar to this expected test object
        var expectedIngredients = new List<Ingredient>
        {
            new("Starter", 289),
            new("Water", 260.261905),
            new("Flour", 450.738095)
        };

        var actualIngredients = recipe.ComputeIngredients();

        AssertSimilarIngredientLists(expectedIngredients, actualIngredients, precision: 6);
    }

    [Fact]
    public void GetDebuggerDisplay_ReturnsCorrectString()
    {
        // Arrange
        var expectedDebug = "StarterMass: 100 | StarterHydration: 0.5 | FinalHydration: 0.68 | FinalMass: 500";
        var starterMass = 100;
        var starterHydration = 0.5;
        var finalHydration = 0.68;
        var finalMass = 500;

        // Act
        var sourdoughRecipe = new SourdoughRecipe(starterMass, starterHydration, finalHydration, finalMass);
        var actualDebug = sourdoughRecipe.ToString();

        // Assert
        Assert.Equal(expectedDebug, actualDebug);
    }

    [Theory]
    [InlineData(-1, 0.5, 0.68, 1000)]
    [InlineData(289, -0.1, 0.68, 1000)]
    [InlineData(289, 0.5, -0.1, 1000)]
    [InlineData(289, 0.5, 0.68, -500)]
    public void InvalidSourdoughRecipeParam_ThrowsArgumentOutOfRangeException(double starterMass, double starterHydration, double finalHydration, double finalMass)
    {
        // Act and Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new SourdoughRecipe(starterMass, starterHydration, finalHydration, finalMass);
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void SelfTest_AssertSimilarIngredientLists(int precision)
    {
        AssertSimilarIngredientLists(expectedIngredients: [], actualIngredients: [], precision);        
    }

    /// <summary>
    /// Compare two lists of <see cref="Ingredient"/>s, rounding to check with limited precision. Each ingredient should appear only once by name.
    /// </summary>
    private static void AssertSimilarIngredientLists(List<Ingredient> expectedIngredients, IEnumerable<Ingredient> actualIngredients, int precision)
    {
        foreach ((var expectedIngredient, var actualIngredient) in from expectedIngredient in expectedIngredients
                                                                   let actualIngredient = actualIngredients.Single(i => i.Name == expectedIngredient.Name)
                                                                   select (expectedIngredient, actualIngredient))
        {
            Assert.Equal(Math.Round(expectedIngredient.Mass, precision), Math.Round(actualIngredient.Mass, precision));
        }
    }
}
