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
