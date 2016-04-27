using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace LengthOnPropertyValue
{
  public class Test : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      
      return ValidationResult.Success;
    }
  }

  public class LengthOnOtherPropertyValueAttribute : ValidationAttribute, IClientValidatable
  {
    private readonly string propertyNameToCheck;
    private readonly string propertyValueToCheck;
    private readonly int maxLengthOnMatch;
    private readonly int maxLength;

    public LengthOnOtherPropertyValueAttribute(string propertyNameToCheck, string propertyValueToCheck, int maxLengthOnMatch, int maxLength)
    {
      this.propertyValueToCheck = propertyValueToCheck;
      this.maxLengthOnMatch = maxLengthOnMatch;
      this.maxLength = maxLength;
      this.propertyNameToCheck = propertyNameToCheck;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var propertyName = validationContext.ObjectType.GetProperty(propertyNameToCheck);
      if (propertyName == null)
        return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { propertyNameToCheck }));

      var propertyValue = propertyName.GetValue(validationContext.ObjectInstance, null) as string;

      if (propertyValueToCheck.Equals(propertyValue, StringComparison.InvariantCultureIgnoreCase) && value != null && ((string)value).Length > maxLengthOnMatch)
        return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, maxLengthOnMatch));

      if (value != null && ((string)value).Length > maxLength)
        return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, maxLength));

      return ValidationResult.Success;
    }

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
      var rule = new ModelClientValidationRule
      {
        ErrorMessage = string.Format(ErrorMessageString, metadata.GetDisplayName(), maxLengthOnMatch),
        ValidationType = "lengthonotherpropertyvalue"
      };
      rule.ValidationParameters.Add("nametocheck", propertyNameToCheck);
      rule.ValidationParameters.Add("valuetocheck", propertyValueToCheck);
      rule.ValidationParameters.Add("maxlengthonmatch", maxLengthOnMatch);
      rule.ValidationParameters.Add("maxlength", maxLength);
      yield return rule;
    }

    /*
    //Client-Side -> NOT in the DOMReady, because it is too late
    (function ($) {
      jQuery.validator.addMethod('lengthonotherpropertyvalue',
        function (value, element, params) {
          var propertyValue = $('#' + params.nametocheck).val();
          if (propertyValue === params.valuetocheck && value.length > params.maxlengthonmatch)
            return false;
          if (propertyValue !== params.valuetocheck && value.length > params.maxlength)
            return false;
          return true;
        });

      jQuery.validator.unobtrusive.adapters.add('lengthonotherpropertyvalue',
        ['nametocheck', 'valuetocheck', 'maxlength', 'maxlengthonmatch'],
        function (options) {
          options.rules['lengthonotherpropertyvalue'] = {
            nametocheck: options.params.nametocheck,
            valuetocheck: options.params.valuetocheck,
            maxlength: options.params.maxlength,
            maxlengthonmatch: options.params.maxlengthonmatch
          };
          options.messages['lengthonotherpropertyvalue'] = options.message;
        });
    })(jQuery);
    */
  }
}
