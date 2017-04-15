import {Component, Inject, OnInit} from '@angular/core'
import {Response} from "@angular/http";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {CategoryDetailsViewModel} from "../../viewmodels/category/CategoryDetailsViewModel";
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {Category} from "../../models/entities/Category";
import {Pagination} from "../../viewmodels/Pagination";
import {ModalDirective} from "ng2-bootstrap";
import {SearchResult} from "../../models/SearchResult";
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {IClientCategoryService} from "../../interfaces/services/api/IClientCategoryService";
import {IClientToastrService} from "../../interfaces/services/IClientToastrService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Component({
    selector: 'category-management',
    templateUrl: 'category-management.component.html',
    providers: [
        ClientConfigurationService,
    ],
})

export class CategoryManagementComponent implements OnInit {

    //#region Properties

    // List of categories responded from service.
    private categorySearchResult: SearchResult<CategoryDetailsViewModel>;

    // Whether records are being searched or not.
    private isLoading: boolean;

    // Conditions which are used for searching categories.
    private findCategoriesViewModel: SearchCategoriesViewModel;

    // Category which is currently selected to be edited/deleted.
    private selectCategoryDetail : CategoryDetailsViewModel;

    //#endregion

    //#region Constructor

    // Initiate component with dependency injections.
    public constructor(@Inject("IClientCategoryService") public clientCategoryService: IClientCategoryService,
                       public clientConfigurationService: ClientConfigurationService,
                       @Inject("IClientApiService") public clientApiService: IClientApiService,
                       @Inject("IClientToastrService") public clientToastrService: IClientToastrService,
                       @Inject("IClientTimeService") public clientTimeService: IClientTimeService) {

        // Initiate categories search result.
        this.categorySearchResult = new SearchResult<CategoryDetailsViewModel>();
    }

    //#endregion

    //#region Methods

    // Callback is fired when a category is created to be removed.
    public clickRemoveCategory(categoryDetail: CategoryDetailsViewModel, deleteCategoryConfirmModal: any): void {

        // Category detail is not valid.
        if (categoryDetail == null)
            return;

        // Update category information into box.
        this.selectCategoryDetail = categoryDetail;

        // Open delete category confirmation box.
        deleteCategoryConfirmModal.show();
    }

    // This callback is called when user confirms to delete the selected category.
    public clickConfirmDeleteCategory(deleteCategoryConfirmModal: ModalDirective ){

        // Find category by using specific conditions.
        let findCategoriesConditions = new SearchCategoriesViewModel();
        findCategoriesConditions.id = this.selectCategoryDetail.id;

        // No category detail is selected.
        if (this.selectCategoryDetail != null){

            // Make the loading start.
            this.isLoading = true;

            // Call category service to delete the selected category.
            this.clientCategoryService.deleteCategories(findCategoriesConditions)
                .then((response: Response | any) => {

                    // Cancel loading state.
                    this.isLoading = false;

                    // Reload the categories list.
                    this.clickSearch();
                })
                .catch((response:any) => {
                    // Cancel loading.
                    this.isLoading = false;

                    // Proceed common invalid response.
                    this.clientApiService.handleInvalidResponse(response);
                });
        }


        // Close the modal first.
        deleteCategoryConfirmModal.hide();
    }

    // Callback which is fired when change category detail button is clicked.
    public clickChangeCategoryDetail(categoryDetail: CategoryDetailsViewModel, changeCategoryDetailModal: ModalDirective){

        // Category detail is invalid.
        if (categoryDetail == null)
            return;

        // Copy the category detail to selected category detail.
        this.selectCategoryDetail = categoryDetail;

        // Display the change category detail box.
        changeCategoryDetailModal.show();
    }

    // Callback which is fired when change category detail action is confirmed.
    public clickConfirmChangeCategoryDetail(changeCategoryInfoModal: ModalDirective){

        // Selected category detail is invalid.
        if (this.selectCategoryDetail == null)
            return;

        // Initiate category information.
        let category = new Category();
        category.id = this.selectCategoryDetail.id;
        category.name = this.selectCategoryDetail.name;

        // Close the change category info modal.
        changeCategoryInfoModal.hide();

        // Start loading.
        this.isLoading = true;

        // Call service to update category information.
        this.clientCategoryService.editCategoryDetails(category.id, category)
            .then((response: Response | any) => {
                // Reload the categories list.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel loading.
                this.isLoading = false;
            });

    }

    // Callback which is fired when a category should be created into system.
    public clickInitiateCategory(category: any, initiateCategoryModal: ModalDirective): void{

        // Make content be loaded.
        this.isLoading = true;

        // Call service to initiate category.
        this.clientCategoryService.initiateCategory(category)
            .then((response: Response | any) => {
                // Cancel content loading.
                this.isLoading = false;

                // Parse information of response.
                let information = response.json();

                // Display notification to client screen.
                this.clientToastrService.success(`${information['name']} has been created successfully`, 'System', null);

                // Close the modal.
                initiateCategoryModal.hide();

                // Reload search results.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel content loading.
                this.isLoading = false;

                // Proceed common function to handle invalid process.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Freeze the find box.
        this.isLoading = true;

        // Reset the selected category detail.
        this.selectCategoryDetail = null;

        // Find categories by using specific conditions.
        this.clientCategoryService.getCategories(this.findCategoriesViewModel)
            .then((response: Response)=> {

                // Update categories list.
                this.categorySearchResult = response.json();

                // Unfreeze the category find box.
                this.isLoading = false;
            })
            .catch((response: Response | any) => {

                // Unlock screen components.
                this.isLoading = false;

                // Call common function to handle error response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when page selection is changed.
    public clickPageChange(pagination: any): void{
        // Update pagination index.
        this.findCategoriesViewModel.pagination.index = pagination.page;

        // Call search function.
        this.clickSearch();
    }
    
    // This callback is fired when category management component is initiated.
    public ngOnInit() {
        // Initiate category search conditions.
        this.findCategoriesViewModel = new SearchCategoriesViewModel();

        // Refactoring pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        this.findCategoriesViewModel.pagination = pagination;
    }

    //#endregion
}