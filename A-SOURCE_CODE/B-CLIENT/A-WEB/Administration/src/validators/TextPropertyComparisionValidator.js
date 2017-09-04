"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var TextPropertyComparisionValidator = TextPropertyComparisionValidator_1 = (function () {
    // Initiate validator with default settings.
    function TextPropertyComparisionValidator(textPropertyComparision, textPropertyComparisionMode) {
        this.textPropertyComparision = textPropertyComparision;
        this.textPropertyComparisionMode = textPropertyComparisionMode;
    }
    TextPropertyComparisionValidator.prototype.validate = function (abstractControl) {
        // Find control value.
        var controlText = abstractControl.value;
        // Find the property whose value is used in comparision.
        var targetProperty = abstractControl.root.get(this.textPropertyComparision);
        // No property is defined.
        if (targetProperty == null)
            return null;
        // Find the value of target control.
        var targetControlText = targetProperty.value;
        targetProperty.valueChanges.subscribe(function (x) {
            abstractControl.updateValueAndValidity();
        });
        return this.findValidationSummary(controlText, targetControlText);
    };
    // Find validation summary from 2 properties.
    TextPropertyComparisionValidator.prototype.findValidationSummary = function (controlText, targetControlText) {
        // Base on mode, do comparision.
        switch (this.textPropertyComparisionMode) {
            case '<':
                if (controlText < targetControlText)
                    return null;
                break;
            case '<=':
                if (controlText <= targetControlText)
                    return null;
                break;
            case '=':
                if (controlText == targetControlText)
                    return null;
                break;
            case '>=':
                if (controlText >= targetControlText)
                    return null;
                break;
            case '>':
                if (controlText > targetControlText)
                    return null;
                break;
            default:
                if (controlText === targetControlText)
                    return null;
                break;
        }
        // Validation is failed.
        return {
            textPropertyComparision: true
        };
    };
    return TextPropertyComparisionValidator;
}());
TextPropertyComparisionValidator = TextPropertyComparisionValidator_1 = __decorate([
    core_1.Directive({
        selector: '[text-property-comparision][formControlName],[text-property-comparision][formControl],[text-property-comparision][ngModel]',
        providers: [
            {
                provide: forms_1.NG_VALIDATORS,
                useExisting: core_1.forwardRef(function () { return TextPropertyComparisionValidator_1; }),
                multi: true
            }
        ]
    }),
    __param(0, core_1.Attribute('text-property-comparision')),
    __param(1, core_1.Attribute('text-property-comparision-mode')),
    __metadata("design:paramtypes", [String, String])
], TextPropertyComparisionValidator);
exports.TextPropertyComparisionValidator = TextPropertyComparisionValidator;
var TextPropertyComparisionValidator_1;
//# sourceMappingURL=TextPropertyComparisionValidator.js.map