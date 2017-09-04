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
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var SearchPostsViewModel_1 = require("../../viewmodels/post/SearchPostsViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var PostDetailBoxComponent = (function () {
    //#region Constructor
    function PostDetailBoxComponent(clientTimeService, clientConfigurationService) {
        this.clientTimeService = clientTimeService;
        this.clientConfigurationService = clientConfigurationService;
        // Event emitter initialization.
        this.changeCommentsPage = new core_1.EventEmitter();
    }
    //#endregion
    //#region Methods
    // Callback which is fired when component has been initiated.
    PostDetailBoxComponent.prototype.ngOnInit = function () {
        var condition = new SearchPostsViewModel_1.SearchPostsViewModel();
        var pagination = new Pagination_1.Pagination();
        pagination.page = 0;
        pagination.records = this.clientConfigurationService.getMinPageRecords();
        condition.pagination = pagination;
        this.searchPostDetailCondition = condition;
    };
    // Check whether post contains any comments or not.
    PostDetailBoxComponent.prototype.hasComments = function () {
        // Result is blank.
        if (this.getCommentsDetailsResult == null)
            return false;
        // Comments list is empty.
        var comments = this.getCommentsDetailsResult.records;
        if (comments == null || comments.length < 1)
            return false;
        if (this.getCommentsDetailsResult.total < 1)
            return false;
        return true;
    };
    // Check whether component is running for search tasks or not.
    PostDetailBoxComponent.prototype.isComponentBusy = function () {
        return (this.isSearchingPost || this.isSearchingComments);
    };
    // Callback which is fired when comments pagination button is clicked.
    PostDetailBoxComponent.prototype.clickSearchComments = function (parameter) {
        // Parameter is invalid.
        if (parameter == null || parameter['page'] == null) {
            this.changeCommentsPage.emit(0);
            return;
        }
        var page = parameter['page'];
        page--;
        if (page < 0)
            page = 0;
        this.changeCommentsPage.emit(page);
    };
    return PostDetailBoxComponent;
}());
PostDetailBoxComponent = __decorate([
    core_1.Component({
        selector: 'post-detail-box',
        templateUrl: 'post-detail-box.component.html',
        inputs: ['maxComments', 'postDetails', 'getCommentDetailsResult', 'isSearchingPost', 'isSearchingComments'],
        outputs: ['changeCommentsPage'],
        providers: [
            ClientConfigurationService_1.ClientConfigurationService
        ]
    }),
    __param(0, core_1.Inject("IClientTimeService")),
    __metadata("design:paramtypes", [Object, ClientConfigurationService_1.ClientConfigurationService])
], PostDetailBoxComponent);
exports.PostDetailBoxComponent = PostDetailBoxComponent;
//# sourceMappingURL=post-detail-box.component.js.map