using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PodcastApp.DTO.Attributes
{
    /// <summary>
    /// Combines [Required] and [BindRequired] behavior: fails if value is missing or null/empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SmartRequiredAttribute : ValidationAttribute, IModelNameProvider, IPropertyValidationFilter
    {
        private readonly RequiredAttribute _required = new RequiredAttribute();

        public string? Name { get; set; }

        public SmartRequiredAttribute()
        {
            ErrorMessage = "This field is required.";
        }

        public override bool IsValid(object? value)
        {
            // Fail if null or empty
            return _required.IsValid(value);
        }

        public override string FormatErrorMessage(string name)
        {
            return _required.FormatErrorMessage(name);
        }

        // This ensures that the property will always be validated
        public bool ShouldValidateEntry(ValidationEntry entry, ValidationEntry parentEntry)
        {
            return true;
        }
    }
}
