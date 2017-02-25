export class ClientValidationService{

    public validationDictionary : any = {
            'INFORMATION_REQUIRED': 'required'
    };

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

            // Find the key.
            let key = keys[i];

            // Invalid key.
            if (key == null || key.length < 1)
                continue;

            // Find key in list of keys.
            let camelCaseKey = this.findCamelCasePropertyName(keys[i]);


            // Find validation message of the specific key.
            this.findPropertyValidationMessages(properties, camelCaseKey, parameter[key]);
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

            key = this.findCamelCasePropertyName(key);

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
    /*
     * Input:
     * input = {
     * 	pagination:{
     * 		index: ['INFORMATION_REQUIRED'],
     * 		records: ['RECORD_MIN_INVALID','RECORD_MAX_INVALID']
     * 		}
     * 	};
     *
     * Output:
     * output = {
     *  controls:{
     *      pagination:{
     *          controls:{
     *              index:{
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
    public findFrontendValidationModel(propertyValidationMap: any, model: any, parameter:any): void {

        // Invalid parameter.
        if (parameter == null)
            return;

        if (parameter instanceof Array){

            let errorsList = {};

            for (var index = 0; index < parameter.length; index++){
                var property = parameter[index];

                errorsList[propertyValidationMap[property]] = true;

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
        for (var i = 0; i < properties.length; i++){

            // Find the key which is currently iterated.
            var key = properties[i];

            // Initiate a controls object which contains the iterated property name.
            if (model['controls'] == null) {
                console.log('model doesn\'t have controls');
                continue;
            }


            // Property doesn't exist in the model.
            if (model['controls'][key] == null) {
                console.log(`model["controls"][${key}] doesn't exist`);
                continue;
            }

            console.log(`key = ${key}`);
            //model['controls'][key] = {};
            this.findFrontendValidationModel(propertyValidationMap, model['controls'][key], parameter[key]);
        }
    };

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