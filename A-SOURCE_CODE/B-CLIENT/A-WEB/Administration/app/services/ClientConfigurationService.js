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
var core_1 = require("@angular/core");
var Dictionary_1 = require("../viewmodels/Dictionary");
var SortDirection_1 = require("../enumerations/SortDirection");
var CategorySortProperty_1 = require("../enumerations/order/CategorySortProperty");
var ConfigurationService = (function () {
    function ConfigurationService() {
        // Amount of records which can be displayed on the screen.
        this.pageRecords = [5, 10, 15, 20];
        // Initate default settings of ng2-bootstrap pagination control.
        this.ngPaginationSettings = {
            rotate: true,
            firstText: '<<',
            previousText: '<',
            nextText: '>',
            lastText: '>>'
        };
        // Initiate sort directions list.
        this.sortDirections = new Dictionary_1.Dictionary();
        this.sortDirections.insert('Ascending', SortDirection_1.SortDirection.Ascending);
        this.sortDirections.insert('Descending', SortDirection_1.SortDirection.Descending);
        // Initiate category sort properties.
        this.categorySortProperties = new Dictionary_1.Dictionary();
        this.categorySortProperties.insert('Index', CategorySortProperty_1.CategorySortProperty.index);
        this.categorySortProperties.insert('Creator', CategorySortProperty_1.CategorySortProperty.creatorIndex);
        this.categorySortProperties.insert('Name', CategorySortProperty_1.CategorySortProperty.name);
        this.categorySortProperties.insert('Created', CategorySortProperty_1.CategorySortProperty.created);
        this.categorySortProperties.insert('Last modified', CategorySortProperty_1.CategorySortProperty.lastModified);
    }
    // Maximum number of records which can be displayed on page.
    ConfigurationService.prototype.findMaxPageRecords = function () {
        return this.pageRecords[this.pageRecords.length - 1];
    };
    return ConfigurationService;
}());
ConfigurationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ConfigurationService);
exports.ConfigurationService = ConfigurationService;
//# sourceMappingURL=ClientConfigurationService.js.map