namespace Example02.Binders;

public class BooleanModelBinder
{
    public bool Value { get; private init; }

    public static bool TryParse(string value, out BooleanModelBinder model)
    {
        var (isTrue, isFalse) = (IsTrueValue(value), IsFalseValue(value));
        if (!isTrue && !isFalse)
        {
            model = null;
        }

        model = new BooleanModelBinder
        {
            Value = isTrue
        };

        return true;
    }
    
    private static bool IsTrueValue(string value) => IgnoreCaseEquals(value, "True")
                                                     || IgnoreCaseEquals(value, "Yes")
                                                     || IgnoreCaseEquals(value, "Oui")
                                                     || IgnoreCaseEquals(value, "1");
    
    private static bool IsFalseValue(string value) => IgnoreCaseEquals(value, "False")
                                                      || IgnoreCaseEquals(value, "No")
                                                      || IgnoreCaseEquals(value, "Non")
                                                      || IgnoreCaseEquals(value, "0");

    private static bool IgnoreCaseEquals(string left, string right) => string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
}