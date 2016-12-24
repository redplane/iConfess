export class ValidationService{

    /*
    * Build a structure of validation messages sent back from web service.
    * Such as: conditions.pagination.records
    * */
    public findPropertiesValidationMessages(parameter: any): any{

        // Find list of keys in object.
        let keys = Object.keys(parameter);

        // Initiate properties list after refinement.
        let properties = {};

        // Get through every key in parameter.
        for (let i = 0; i < keys.length; i++){

            // Find key in list of keys.
            let key = this.findCamelCasePropertyName(keys[i]);

            // Invalid key.
            if (key == null || key.length < 1)
                continue;

            // Find validation message of the specific key.
            this.findPropertyValidationMessages(properties, key, parameter[key]);
        }

        return properties;
    }

    /*
     * Build a structure of validation messages sent back from web service.
     * Such as: conditions.pagination.records
     * Scope: one property.
     * */
    public findPropertyValidationMessages(sourceProperty: any, property: string, value: any): any{

        // Split property by character .
        let keys = property.split('.');

        // Initiate pointer.
        let pointer = sourceProperty;

        // Find the last index of keys.
        let lastIndex = keys.length - 1;

        // Get through every key.
        for (let i = 0; i < lastIndex; i++){

            // Find the key.
            let key = keys[i];

            // Key is invalid.
            if (key == null || key.length < 1)
                continue;

            // Key already exists in the source property.
            if (pointer[key] != null){
                pointer = pointer[key];
                continue;
            }
            // Extend the property.
            pointer[key] = {};
            pointer = pointer[key];
        }

        // Find the last key of object.
        let lastKey = this.findCamelCasePropertyName(keys[lastIndex]);

        // Key hasn't been specified.
        if (pointer[lastKey] == null){
            pointer[lastKey] = value;
        }
    }

    /*
    * Convert validation property sent back from back-end.
    * */
    public findFrontendValidationModel = function(dictionary: any, pointer:any, parameter:any){

        // Invalid parameter.
        if (parameter == null)
            return;

        if (parameter instanceof Array){

            for (let index = 0; index < parameter.length; index++){
                let property = parameter[index];

                if (dictionary != null && dictionary[property] != null && dictionary[property].length > 0)
                    pointer[dictionary[property]] = true;
                else
                    pointer[property] = true;
            }

            return;
        }

        // Find all properties of parameter.
        let properties = Object.keys(parameter);

        for (let i = 0; i < properties.length; i++){
            let key = properties[i];
            if (pointer[key] == null)
                pointer[key] = {};

            this.findFrontendValidationModel(dictionary, pointer[key], parameter[key]);
        }
    }

    /*
    * Find property name in camel-case.
    * */
    public findCamelCasePropertyName(propertyName: string){
            return propertyName.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, (match, index) => {
                if (+match === 0)
                    return '';
                return index == 0 ? match.toLowerCase() : match.toUpperCase();
            });
    }

}