using System.ComponentModel.DataAnnotations;

namespace Norm.Library.Helpers
{
    public static class ModelValidationHelper
    {
        public static Tuple<bool, List<ValidationResult>> IsValid(object model)
        {
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(model);

            bool isValid = Validator.TryValidateObject(model, context, validationResult, true);

            return Tuple.Create(isValid, validationResult);
        }
    }
}
