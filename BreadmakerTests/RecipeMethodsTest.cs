using Breadmaker;

namespace BreadmakerTests;

public class RecipeMethodsTest
{
    [Fact]
    public void TestBasicRecipe()
    {
        // create a basic sourdough recipe from output parameters for the final dough
        var recipe = new SourdoughRecipe(starterMass: 289, starterHydration: 1, finalHydration: 0.68m, finalMass: 1000);

        // the calculator should output a list similar to this expected test object
        var expectedIngredients = new List<Ingredient>
        {
            new("Starter", 289),
            new("Water", 260.261905m),
            new("Flour", 450.738095m)
        };

        var actual = recipe.ComputeIngredients();

        AssertSimilarIngredientLists(expectedIngredients, actual, precision: 6);
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
            new SourdoughRecipe((decimal)starterMass, (decimal)starterHydration, (decimal)finalHydration, (decimal)finalMass);
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
