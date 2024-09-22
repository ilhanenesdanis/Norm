using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Norm.Library.Attributes
{
    public class SqlInjectionAttribute : ValidationAttribute
    {
        private readonly string[] _injectionKeywords = new[]
        {
            "--", ";--", ";", "/*", "*/", "@@", "@",
        "char", "nchar", "varchar", "nvarchar",
        "alter", "begin", "cast", "create", "cursor",
        "declare", "delete", "drop", "exec", "execute",
        "fetch", "insert", "kill", "select", "sys",
        "sysobjects", "syscolumns", "table", "update"
        };
        private static readonly string[] _sqlDangerousCharacters = { "'", "--", ";", "\"", "\\" };
        public SqlInjectionAttribute()
        {
            const string defaultErrorMessage = "The input provided is invalid. Please remove any special characters and try again.";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Value is Required");
            }

            string inputValue = value.ToString() ?? string.Empty;
            //check DangerousChar 
            foreach (var dChar in _sqlDangerousCharacters)
            {
                if (inputValue.Contains(dChar))
                    return new ValidationResult("Input contains invalid characters.");

            }
            //check injectionKeys
            foreach (var keyword in _injectionKeywords)
            {
                if (Regex.IsMatch(inputValue, $@"\b{keyword}\b", RegexOptions.IgnoreCase))
                    return new ValidationResult("Input contains forbidden SQL keywords.");

            }

            return ValidationResult.Success;
        }
    }
}
