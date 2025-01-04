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
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(starterMass, 0, nameof(starterMass));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(starterHydration, 0, nameof(starterHydration));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(finalHydration, 0, nameof(finalHydration));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(finalMass, 0, nameof(finalMass));

        StarterMass = starterMass;
        StarterHydration = starterHydration;
        FinalHydration = finalHydration;
        FinalMass = finalMass;
    }

    /// <summary>
    /// Calculates and returns a list of ingredients needed for baking.
    /// </summary>
    /// <returns>A collection of bread ingredients: starter, water, and flour.</returns>
    public IEnumerable<Ingredient> ComputeIngredients()
    {
        // Calculate intermediate values
        var starterDry = StarterMass / (StarterHydration + 1);
        var starterWet = StarterHydration * StarterMass / (StarterHydration + 1);

        // Calculate flour needed
        var flour = (FinalMass - starterDry - (FinalHydration * starterDry)) / (FinalHydration + 1);

        return
        [
            new("Starter", StarterMass),
            new("Water", FinalMass - starterDry - starterWet - flour),
            new("Flour", flour)
        ];
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append($"{nameof(StarterMass)}: {StarterMass} | ");
        stringBuilder.Append($"{nameof(StarterHydration)}: {StarterHydration} | ");
        stringBuilder.Append($"{nameof(FinalHydration)}: {FinalHydration} | ");
        stringBuilder.Append($"{nameof(FinalMass)}: {FinalMass}");

        return stringBuilder.ToString();
    }
}
