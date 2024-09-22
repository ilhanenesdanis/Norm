using Norm.Library.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Norm.Library.Core.Models
{
    public sealed class Query : IValidatableObject
    {
        public required string TableName { get; set; }
        public string[]? Columns { get; set; }
        public string? Column { get; set; }
        public int Size { get; set; }
        public Dictionary<string, NormSorting>? Sorting { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(TableName))
            {
                yield return new ValidationResult("TableName field is required", new[] { nameof(TableName) });
            }

            if ((Columns == null || Columns.Length == 0) || string.IsNullOrWhiteSpace(Column))
            {
                yield return new ValidationResult(
                "Either 'Columns' or 'Column' must be provided.",
                new[] { nameof(Columns), nameof(Column) });
            }

            if (Size < 0)
            {
                yield return new ValidationResult("Size value must be greater than 0", new[] { nameof(TableName) });
            }

        }
    }
}
