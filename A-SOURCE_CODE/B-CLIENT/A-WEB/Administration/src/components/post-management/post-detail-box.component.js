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
var core_1 = require("@angular/core");
var ClientTimeService_1 = require("../../services/ClientTimeService");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var FindPostViewModel_1 = require("../../viewmodels/post/FindPostViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var PostDetailBoxComponent = (function () {
    function PostDetailBoxComponent(clientTimeService, clientConfigurationService) {
        this.clientTimeService = clientTimeService;
        this.clientConfigurationService = clientConfigurationService;
        // Event emitter initialization.
        this.changeCommentsPage = new core_1.EventEmitter();
    }
    // Callback which is fired when component has been initiated.
    PostDetailBoxComponent.prototype.ngOnInit = function () {
        var condition = new FindPostViewModel_1.FindPostViewModel();
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMinPageRecords();
        condition.pagination = pagination;
        this.searchPostDetailCondition = condition;
    };
    // Check whether post contains any comments or not.
    PostDetailBoxComponent.prototype.hasComments = function () {
        // Result is blank.
        if (this.searchCommentsResult == null)
            return false;
        // Comments list is empty.
        var comments = this.searchCommentsResult.comments;
        if (comments == null || comments.length < 1)
            return false;
        if (this.searchCommentsResult.total < 1)
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
        this.changeCommentsPage.emit(page);
    };
    return PostDetailBoxComponent;
}());
PostDetailBoxComponent = __decorate([
    core_1.Component({
        selector: 'post-detail-box',
        templateUrl: 'post-detail-box.component.html',
        inputs: ['maxComments', 'postDetails', 'searchCommentsResult', 'isSearchingPost', 'isSearchingComments'],
        outputs: ['changeCommentsPage'],
        providers: [
            ClientTimeService_1.ClientTimeService,
            ClientConfigurationService_1.ClientConfigurationService
        ]
    }),
    __metadata("design:paramtypes", [ClientTimeService_1.ClientTimeService,
        ClientConfigurationService_1.ClientConfigurationService])
], PostDetailBoxComponent);
exports.PostDetailBoxComponent = PostDetailBoxComponent;
//# sourceMappingURL=post-detail-box.component.js.map