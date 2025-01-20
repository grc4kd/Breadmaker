using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Breadmaker;

namespace BreadmakerBenchmarks;

[MemoryDiagnoser]
public class SourdoughBenchmarks
{
    private SourdoughRecipe _recipe;
    
    [GlobalSetup]
    public void Setup()
    {
        _recipe = new SourdoughRecipe(
            starterMass: 200,
            starterHydration: 1.0,
            finalHydration: 0.75,
            finalMass: 1000);
    }

    [Benchmark]
    public Ingredient[] ComputeIngredients() => _recipe.ComputeIngredients().ToArray();

    [Benchmark]
    public (double hydration, Ingredient[] ingredients) DoughComponents()
    {
        var (hydration, ingredients) = SourdoughRecipe.DoughComponents(
            starterHydration: 1.0,
            desiredHydration: 0.75,
            desiredMass: 1000,
            starterRatio: 0.2);
        return (hydration, ingredients.ToArray());
    }
} 