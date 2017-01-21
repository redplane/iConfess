import {Component, OnInit} from '@angular/core'
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {ClientCategoryService} from "../services/clients/ClientCategoryService";
import {TimeService} from "../services/TimeService";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {FindCategoriesViewModel} from "../viewmodels/category/FindCategoriesViewModel";
import {ClientApiService} from "../services/ClientApiService";
import {Response} from "@angular/http";
import {ClientConfigurationService} from "../services/ClientConfigurationService";
import {Pagination} from "../viewmodels/Pagination";
import {ModalDirective} from "ng2-bootstrap";
import {Category} from "../models/Category";
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";
import {ClientNotificationService} from "../services/ClientNotificationService";

declare var $: any;

@Component({
    selector: 'category-management',
    templateUrl: './app/views/pages/category-management.component.html',
    providers: [
        ClientCategoryService,
        TimeService,

        ClientApiService,
        ClientConfigurationService,
        ClientAuthenticationService,

        ClientNotificationService
    ],
})

export class CategoryManagementComponent implements OnInit {

    // List of categories responded from service.
    private _categorySearchResult: CategorySearchDetailViewModel;

    // Whether records are being searched or not.
    private _isLoading: boolean;

    // Conditions which are used for searching categories.
    private _findCategoriesViewModel: FindCategoriesViewModel;

    // Category which is currently selected to be edited/deleted.
    private _selectCategoryDetail : CategoryDetailViewModel;

    // Initiate component with dependency injections.
    public constructor(public clientCategoryService: ClientCategoryService,
                       public clientConfigurationService: ClientConfigurationService,
                       public clientApiService: ClientApiService,
                       public clientNotificationService: ClientNotificationService) {

        // Initiate categories search result.
        this._categorySearchResult = new CategorySearchDetailViewModel();
    }

    // Callback is fired when a category is created to be removed.
    public clickRemoveCategory(categoryDetail: CategoryDetailViewModel, deleteCategoryConfirmModal: any): void {

        // Category detail is not valid.
        if (categoryDetail == null)
            return;

        // Update category information into box.
        this._selectCategoryDetail = categoryDetail;

        // Open delete category confirmation box.
        deleteCategoryConfirmModal.show();
    }

    // This callback is called when user confirms to delete the selected category.
    public clickConfirmDeleteCategory(deleteCategoryConfirmModal: ModalDirective ){

        // Find category by using specific conditions.
        let findCategoriesConditions = new FindCategoriesViewModel();
        findCategoriesConditions.id = this._selectCategoryDetail.id;

        // No category detail is selected.
        if (this._selectCategoryDetail != null){

            // Make the loading start.
            this._isLoading = true;

            // Call category service to delete the selected category.
            this.clientCategoryService.deleteCategories(findCategoriesConditions)
                .then((response: Response | any) => {

                    // Cancel loading state.
                    this._isLoading = false;

                    // Reload the categories list.
                    this.clickSearch();
                })
                .catch((response:any) => {
                    // Cancel loading.
                    this._isLoading = false;

                    // Proceed common invalid response.
                    this.clientApiService.proceedHttpNonSolidResponse(response);
                });
        }


        // Close the modal first.
        deleteCategoryConfirmModal.hide();
    }

    // Callback which is fired when change category detail button is clicked.
    public clickChangeCategoryDetail(categoryDetail: CategoryDetailViewModel, changeCategoryDetailModal: ModalDirective){

        // Category detail is invalid.
        if (categoryDetail == null)
            return;

        // Copy the category detail to selected category detail.
        this._selectCategoryDetail = categoryDetail;

        // Display the change category detail box.
        changeCategoryDetailModal.show();
    }

    // Callback which is fired when change category detail action is confirmed.
    public clickConfirmChangeCategoryDetail(changeCategoryInfoModal: ModalDirective){

        // Selected category detail is invalid.
        if (this._selectCategoryDetail == null)
            return;

        // Initiate category information.
        let category = new Category();
        category.id = this._selectCategoryDetail.id;
        category.name = this._selectCategoryDetail.name;

        // Close the change category info modal.
        changeCategoryInfoModal.hide();

        // Start loading.
        this._isLoading = true;

        // Call service to update category information.
        this.clientCategoryService.changeCategoryDetails(category.id, category)
            .then((response: Response | any) => {
                // Reload the categories list.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel loading.
                this._isLoading = false;
            });

    }

    // Callback which is fired when a category should be created into system.
    public clickInitiateCategory(category: any, initiateCategoryModal: ModalDirective): void{

        // Make content be loaded.
        this._isLoading = true;

        // Call service to initiate category.
        this.clientCategoryService.initiateCategory(category)
            .then((response: Response | any) => {
                // Cancel content loading.
                this._isLoading = false;

                // Parse information of response.
                let information = response.json();

                // Display notification to client screen.
                this.clientNotificationService.success(`${information['name']} has been created successfully`, 'System');

                // Close the modal.
                initiateCategoryModal.hide();

                // Reload search results.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel content loading.
                this._isLoading = false;

                // Proceed common function to handle invalid process.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Freeze the find box.
        this._isLoading = true;

        // Reset the selected category detail.
        this._selectCategoryDetail = null;

        // Find categories by using specific conditions.
        this.clientCategoryService.findCategories(this._findCategoriesViewModel)
            .then((response: Response)=> {

                // Update categories list.
                this._categorySearchResult = response.json();

                // Unfreeze the category find box.
                this._isLoading = false;
            })
            .catch((response: Response | any) => {

                // Unlock screen components.
                this._isLoading = false;

                // Call common function to handle error response.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Callback which is fired when page selection is changed.
    public clickPageChange(pagination: any): void{
        // Update pagination index.
        this._findCategoriesViewModel.pagination.index = pagination.page;

        // Call search function.
        this.clickSearch();
    }
    
    // This callback is fired when category management component is initiated.
    public ngOnInit() {
        // Initiate category search conditions.
        this._findCategoriesViewModel = new FindCategoriesViewModel();

        // Refactoring pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this._findCategoriesViewModel.pagination = pagination;
    }
}