"use strict";
var ValidationService = (function () {
    function ValidationService() {
        /*
        * Convert validation property sent back from back-end.
        * */
        this.findFrontendValidationModel = function (dictionary, pointer, parameter) {
            // Invalid parameter.
            if (parameter == null)
                return;
            if (parameter instanceof Array) {
                for (var index = 0; index < parameter.length; index++) {
                    var property = parameter[index];
                    if (dictionary != null && dictionary[property] != null && dictionary[property].length > 0)
                        pointer[dictionary[property]] = true;
                    else
                        pointer[property] = true;
                }
                return;
            }
            // Find all properties of parameter.
            var properties = Object.keys(parameter);
            for (var i = 0; i < properties.length; i++) {
                var key = properties[i];
                if (pointer[key] == null)
                    pointer[key] = {};
                this.findFrontendValidationModel(dictionary, pointer[key], parameter[key]);
            }
        };
    }
    /*
    * Build a structure of validation messages sent back from web service.
    * Such as: conditions.pagination.records
    * */
    ValidationService.prototype.findPropertiesValidationMessages = function (parameter) {
        // Find list of keys in object.
        var keys = Object.keys(parameter);
        // Initiate properties list after refinement.
        var properties = {};
        // Get through every key in parameter.
        for (var i = 0; i < keys.length; i++) {
            // Find key in list of keys.
            var key = this.findCamelCasePropertyName(keys[i]);
            // Invalid key.
            if (key == null || key.length < 1)
                continue;
            // Find validation message of the specific key.
            this.findPropertyValidationMessages(properties, key, parameter[key]);
        }
        return properties;
    };
    /*
     * Build a structure of validation messages sent back from web service.
     * Such as: conditions.pagination.records
     * Scope: one property.
     * */
    ValidationService.prototype.findPropertyValidationMessages = function (sourceProperty, property, value) {
        // Split property by character .
        var keys = property.split('.');
        // Initiate pointer.
        var pointer = sourceProperty;
        // Find the last index of keys.
        var lastIndex = keys.length - 1;
        // Get through every key.
        for (var i = 0; i < lastIndex; i++) {
            // Find the key.
            var key = keys[i];
            // Key is invalid.
            if (key == null || key.length < 1)
                continue;
            // Key already exists in the source property.
            if (pointer[key] != null) {
                pointer = pointer[key];
                continue;
            }
            // Extend the property.
            pointer[key] = {};
            pointer = pointer[key];
        }
        // Find the last key of object.
        var lastKey = this.findCamelCasePropertyName(keys[lastIndex]);
        // Key hasn't been specified.
        if (pointer[lastKey] == null) {
            pointer[lastKey] = value;
        }
    };
    /*
    * Find property name in camel-case.
    * */
    ValidationService.prototype.findCamelCasePropertyName = function (propertyName) {
        return propertyName.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
            if (+match === 0)
                return '';
            return index == 0 ? match.toLowerCase() : match.toUpperCase();
        });
    };
    return ValidationService;
}());
exports.ValidationService = ValidationService;
//# sourceMappingURL=ParameterValidationService.js.map