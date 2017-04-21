import {IDictionary} from "../IDictionary";

export interface IClientValidationService{

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
    getFrontendValidationModel(model: any, parameter:any, dictionary: IDictionary<string>): void;
}