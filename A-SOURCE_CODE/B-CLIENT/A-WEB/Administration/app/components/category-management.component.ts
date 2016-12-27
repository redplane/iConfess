import {Component, OnInit} from '@angular/core'
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {ClientCategoryService} from "../services/clients/ClientCategoryService";
import {IClientCategoryService} from "../interfaces/services/IClientCategoryService";
import {TimeService} from "../services/TimeService";
import {ITimeService} from "../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {FindCategoriesViewModel} from "../viewmodels/category/FindCategoriesViewModel";
import {ClientApiService} from "../services/ClientApiService";
import {Response} from "@angular/http";
import {ResponseAnalyzeService} from "../services/ResponseAnalyzeService";
import {ConfigurationService} from "../services/ClientConfigurationService";
import {Pagination} from "../viewmodels/Pagination";
import {ModalDirective} from "ng2-bootstrap";
import {Category} from "../models/Category";

declare var $: any;

@Component({
    selector: 'category-management',
    templateUrl: './app/views/pages/category-management.component.html',
    providers: [
        ClientCategoryService,
        TimeService,
        ClientApiService,
        ResponseAnalyzeService,
        ConfigurationService
    ],
})

export class CategoryManagementComponent implements OnInit {

    // List of categories responded from service.
    private _categorySearchResult: CategorySearchDetailViewModel;

    // Service which handles time conversion.
    private _timeService: ITimeService;

    // Service which provides function to analyze response sent back from server.
    private _responseAnalyzeService: ResponseAnalyzeService;

    // Service which handles category business.
    private _clientCategoryService: IClientCategoryService;

    // Service which provides functions to access application configuration.
    private _clientConfigurationService: ConfigurationService;

    // Whether records are being searched or not.
    private _isLoading: boolean;

    // Conditions which are used for searching categories.
    private _findCategoriesViewModel: FindCategoriesViewModel;

    // Category which is currently selected to be edited/deleted.
    private _selectCategoryDetail : CategoryDetailViewModel;

    // Initiate component with dependency injections.
    public constructor(categoryService: ClientCategoryService, timeService: TimeService,
                       responseAnalyzeService: ResponseAnalyzeService, configurationService: ConfigurationService) {
        this._clientCategoryService = categoryService;
        this._timeService = timeService;
        this._responseAnalyzeService = responseAnalyzeService;

        // Find configuration service in IoC.
        this._clientConfigurationService = configurationService;

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
            // Call category service to delete the selected category.
            this._clientCategoryService.deleteCategories(findCategoriesConditions)
                .then((response: Response | any) => {

                    // Reload the search records list.
                    this.clickSearch();
                })
                .catch((response:any) => {
                    console.log(response);
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


        // Call service to update category information.
        this._clientCategoryService.changeCategoryDetails(category.id, category)
            .then((response: Response | any) => {
                console.log(response);

                // Reload the categories list.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                console.log(response);
            });

    }

    // Callback which is fired when a category should be created into system.
    public clickInitiateCategory(category: any): void{

        // Make content be loaded.
        this._isLoading = true;

        // Call service to initiate category.
        this._clientCategoryService.initiateCategory(category)
            .then((response: Response | any) => {

                console.log(response);

                // Cancel content loading.
                this._isLoading = false;
            })
            .catch((response: Response | any) => {

                // Cancel content loading.
                this._isLoading = false;
            });
    }
    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Freeze the find box.
        this._isLoading = true;

        // Reset the selected category detail.
        this._selectCategoryDetail = null;

        // Find categories by using specific conditions.
        this._clientCategoryService.findCategories(this._findCategoriesViewModel)
            .then((response: Response)=> {

                // Update categories list.
                this._categorySearchResult = response.json();

                // Unfreeze the category find box.
                this._isLoading = false;
            })
            .catch((response: Response | any) => {
                this._isLoading = false;
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

        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this._clientConfigurationService.findMaxPageRecords();
        this._findCategoriesViewModel.pagination = pagination;
    }
}