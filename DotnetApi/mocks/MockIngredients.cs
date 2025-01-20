using Breadmaker;

namespace DotnetApi.mocks;

public record MockIngredients(int Id, string Name, double Mass)
{
    private static int _idStep = int.MinValue;

    public static Ingredient Create(string name, double mass)
    {
        if (_idStep < int.MaxValue)
        {
            _idStep++;
        }

        return new Ingredient(_idStep, name, mass);
    }
}