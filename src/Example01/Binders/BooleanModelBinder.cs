using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Example01.Binders;

public class BooleanModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);
        
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        var (isTrue, isFalse) = (IsTrueValue(value), IsFalseValue(value));
        if (isTrue || isFalse)
        {
            bindingContext.Result = ModelBindingResult.Success(isTrue);
        }

        return Task.CompletedTask;
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