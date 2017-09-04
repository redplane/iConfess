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
Object.defineProperty(exports, "__esModule", { value: true });
/*
 * Contains common business calculation.
 * */
var AccountStatuses_1 = require("../enumerations/AccountStatuses");
var core_1 = require("@angular/core");
var ngx_paginator_option_1 = require("ngx-numeric-paginator/ngx-paginator-option");
var Dictionary_1 = require("../models/Dictionary");
var SortDirection_1 = require("../enumerations/SortDirection");
var ClientCommonService = (function () {
    //#endregion
    //#region Constructor
    // Initiate service with settings.
    function ClientCommonService() {
    }
    //#endregion
    //#region Methods
    // Find the start record page by calculating page page and page records.
    ClientCommonService.prototype.findRecordStartIndex = function (pageIndex, pageRecords, subtractIndex) {
        if (subtractIndex)
            pageIndex -= 1;
        return (pageIndex * pageRecords);
    };
    // Find the start page of record by calculating pagination instance.
    ClientCommonService.prototype.findRecordStartIndexFromPagination = function (pagination, subtractIndex) {
        // Start from 0.
        if (pagination == null)
            return 0;
        // Page page calculation.
        var pageIndex = pagination.page;
        if (subtractIndex)
            pageIndex--;
        // Start page calculation.
        var startIndex = pageIndex * pagination.records;
        return startIndex;
    };
    // Find account display from enumeration.
    ClientCommonService.prototype.getAccountStatusDisplay = function (accountStatus) {
        switch (accountStatus) {
            case AccountStatuses_1.AccountStatuses.Active:
                return 'Active';
            case AccountStatuses_1.AccountStatuses.Disabled:
                return 'Disabled';
            case AccountStatuses_1.AccountStatuses.Pending:
                return 'Pending';
            default:
                return 'N/A';
        }
    };
    // Get maximum number of records which can be displayed on the page.
    ClientCommonService.prototype.getMaxPageRecords = function () {
        return 20;
    };
    // Get paginator settings.
    ClientCommonService.prototype.getPaginationOptions = function () {
        if (this.paginatorOption != null) {
            return this.paginatorOption;
        }
        this.paginatorOption = new ngx_paginator_option_1.NgxPaginatorOption();
        this.paginatorOption.class = 'pagination pagination-sm';
        this.paginatorOption.bAutoHide = true;
        this.paginatorOption.itemCount = this.getMaxPageRecords();
        this.paginatorOption.back = 2;
        this.paginatorOption.front = 2;
        this.paginatorOption.bLastButton = true;
        this.paginatorOption.bPreviousButton = true;
        this.paginatorOption.bNextButton = true;
        this.paginatorOption.bLastButton = true;
        return this.paginatorOption;
    };
    // Search account statuses by using keyword.
    ClientCommonService.prototype.getAccountStatuses = function (keyword) {
        if (this.accountStatuses == null) {
            this.accountStatuses = new Dictionary_1.Dictionary();
            this.accountStatuses.add('Disabled', AccountStatuses_1.AccountStatuses.Disabled);
            this.accountStatuses.add('Pending', AccountStatuses_1.AccountStatuses.Pending);
            this.accountStatuses.add('Active', AccountStatuses_1.AccountStatuses.Active);
        }
        // Initiate result.
        var result = this.accountStatuses
            .getKeyValuePairs();
        // Keyword is specified. Filter statuses,
        if (keyword != null && keyword.length > 1) {
            result = result.filter(function (x) {
                return x.key.indexOf(keyword) != -1;
            });
        }
        return result;
    };
    // Get sort directions.
    ClientCommonService.prototype.getSortDirections = function (keyword) {
        if (this.sortDirections == null) {
            this.sortDirections = new Dictionary_1.Dictionary();
            this.sortDirections.add('Ascending', SortDirection_1.SortDirection.Ascending);
            this.sortDirections.add('Descending', SortDirection_1.SortDirection.Descending);
        }
        return this.sortDirections.getKeyValuePairs();
    };
    return ClientCommonService;
}());
ClientCommonService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientCommonService);
exports.ClientCommonService = ClientCommonService;
//# sourceMappingURL=ClientCommonService.js.map