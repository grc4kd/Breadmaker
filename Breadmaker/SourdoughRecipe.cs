using System.Diagnostics;

namespace Breadmaker;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record SourdoughRecipe
{
    public decimal StarterMass { get; }
    public decimal StarterHydration { get; }
    public decimal FinalHydration { get; }
    public decimal FinalMass { get; }

    public SourdoughRecipe(decimal starterMass, decimal starterHydration, decimal finalHydration, decimal finalMass)
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
        var starterDry = StarterMass / (StarterHydration + 1.0m);
        var starterWet = StarterHydration * StarterMass / (StarterHydration + 1.0m);

        // Calculate flour needed
        var flour = (FinalMass - starterDry - (FinalHydration * starterDry)) / (FinalHydration + 1.0m);

        return
        [
            new("Starter", StarterMass),
            new("Water", FinalMass - starterDry - starterWet - flour),
            new("Flour", flour)
        ];
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
