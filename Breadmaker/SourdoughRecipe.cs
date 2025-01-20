using System.Text;

namespace Breadmaker;

public record SourdoughRecipe
{
    public double StarterMass { get; }
    public double StarterHydration { get; }
    public double FinalHydration { get; }
    public double FinalMass { get; }

    public SourdoughRecipe(double starterMass, double starterHydration, double finalHydration, double finalMass)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(starterMass);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(starterHydration);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(finalHydration);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(finalMass);

        (StarterMass, StarterHydration, FinalHydration, FinalMass) =
            (starterMass, starterHydration, finalHydration, finalMass);
    }

    /// <summary>
    /// Calculates and returns a list of ingredients needed for baking.
    /// </summary>
    /// <returns>A collection of bread ingredients: starter, water, and flour.</returns>
    public IEnumerable<Ingredient> ComputeIngredients()
    {
        // Using more descriptive variable names and simplified calculations
        var starterRatio = 1 + StarterHydration;
        var starterDry = StarterMass / starterRatio;
        var starterWet = StarterHydration * starterDry;

        var flour = (FinalMass - starterDry * (1 + FinalHydration)) / (1 + FinalHydration);

        return
        [
            new(1, "Starter", StarterMass),
            new(2, "Water", FinalMass - starterDry - starterWet - flour),
            new(3, "Flour", flour)
        ];
    }

    public static (double hydration, IEnumerable<Ingredient> ingredients) DoughComponents(double starterHydration, double desiredHydration, double desiredMass, double starterRatio)
    {
        var starter = desiredMass * starterRatio;
        var recipe = new SourdoughRecipe(starter, starterHydration, desiredHydration, desiredMass);
        var ingredients = recipe.ComputeIngredients();
        var water = ingredients.Single(i => i.Name == "Water").Mass;
        var flour = ingredients.Single(i => i.Name == "Flour").Mass;

        return new
        (
            Hydration(starter, starterHydration, water, flour),
            ingredients
        );
    }

    public static double Hydration(double starter, double starterHydration, double water, double flour)
    {
        var starterRatio = starterHydration + 1.0;
        var wet = water + (starter * starterHydration / starterRatio);
        var dry = flour + (starter / starterRatio);
        return wet / dry;
    }

    public override string ToString() => $"{nameof(StarterMass)}: {StarterMass} | {nameof(StarterHydration)}: {StarterHydration}"
        + $" | {nameof(FinalHydration)}: {FinalHydration} | {nameof(FinalMass)}: {FinalMass}";
}