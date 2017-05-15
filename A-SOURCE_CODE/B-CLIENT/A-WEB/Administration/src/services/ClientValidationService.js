"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ClientValidationService = (function () {
    function ClientValidationService() {
        this.validationDictionary = {
            'INFORMATION_REQUIRED': 'required'
        };
    }
    /*
    * Build a structure of validation messages sent back from web service.
    * Such as: conditions.pagination.records (Used by .net core web service)
    * */
    ClientValidationService.prototype.getNetCoreValidation = function (modelState) {
        // Find list of keys in object.
        var keys = Object.keys(modelState);
        // Initiate properties list after refinement.
        var properties = {};
        // Get through every key in parameter.
        for (var i = 0; i < keys.length; i++) {
            // Find the key.
            var key = keys[i];
            // Invalid key.
            if (key == null || key.length < 1)
                continue;
            // Find key in list of keys.
            var camelCaseKey = this.getCamelCasePropertyName(keys[i]);
            // Find validation message of the specific key.
            this.getPropertyValidationMessages(properties, camelCaseKey, modelState[key]);
        }
        return properties;
    };
    /*
     * Build a structure of validation messages sent back from web service.
     * Such as: conditions.pagination.records
     * Scope: one property.
     * */
    ClientValidationService.prototype.getPropertyValidationMessages = function (sourceProperty, property, value) {
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
            key = this.getCamelCasePropertyName(key);
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
        var lastKey = this.getCamelCasePropertyName(keys[lastIndex]);
        // Key hasn't been specified.
        if (pointer[lastKey] == null) {
            pointer[lastKey] = value;
        }
    };
    /*
    * Convert validation property sent back from back-end.
    * */
    /*
     * Input:
     * input = {
     * 	pagination:{
     * 		page: ['INFORMATION_REQUIRED'],
     * 		records: ['RECORD_MIN_INVALID','RECORD_MAX_INVALID']
     * 		}
     * 	};
     *
     * Output:
     * output = {
     *  controls:{
     *      pagination:{
     *          controls:{
     *              page:{
     *                  required: true
     *              },
     *              records:{
     *                  required: true
     *              }
     *          }
     *      }
     *  }
     * }
     * */
    ClientValidationService.prototype.getFrontendValidationModel = function (model, parameter, dictionary) {
        // Invalid parameter.
        if (parameter == null)
            return;
        if (parameter instanceof Array) {
            var errorsList = {};
            for (var index = 0; index < parameter.length; index++) {
                var property = parameter[index];
                // Dictionary already contains the translation key.
                if (dictionary.containsKey(property))
                    errorsList[dictionary.getKeyItem(property)] = true;
                else
                    errorsList[property] = true;
                // Mark propery as it has been changed.
                model.markAsDirty();
                model.markAsTouched();
                model.setErrors(errorsList);
            }
            return;
        }
        // Find all properties of parameter.
        var properties = Object.keys(parameter);
        // Iterate through every property of object. (Root is validationModel)
        for (var i = 0; i < properties.length; i++) {
            // Find the key which is currently iterated.
            var key = properties[i];
            // Initiate a controls object which contains the iterated property name.
            if (model['controls'] == null) {
                console.log('model doesn\'t have controls');
                continue;
            }
            // Property doesn't exist in the model.
            if (model['controls'][key] == null) {
                console.log("model[\"controls\"][" + key + "] doesn't exist");
                continue;
            }
            console.log("key = " + key);
            //model['controls'][key] = {};
            this.getFrontendValidationModel(model['controls'][key], parameter[key], dictionary);
        }
    };
    ;
    /*
    * Find property name in camel-case.
    * */
    ClientValidationService.prototype.getCamelCasePropertyName = function (propertyName) {
        return propertyName.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
            if (+match === 0)
                return '';
            return index == 0 ? match.toLowerCase() : match.toUpperCase();
        });
    };
    return ClientValidationService;
}());
exports.ClientValidationService = ClientValidationService;
//# sourceMappingURL=ClientValidationService.js.map