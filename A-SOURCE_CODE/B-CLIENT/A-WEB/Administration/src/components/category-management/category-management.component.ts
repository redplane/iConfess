import {Component, OnInit} from '@angular/core'
import {Response} from "@angular/http";
import {ClientCategoryService} from "../../services/clients/ClientCategoryService";
import {ClientTimeService} from "../../services/ClientTimeService";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";
import {ClientNotificationService} from "../../services/ClientNotificationService";
import {CategorySearchDetailViewModel} from "../../viewmodels/category/CategorySearchDetailViewModel";
import {CategoryDetailViewModel} from "../../viewmodels/category/CategoryDetailViewModel";
import {FindCategoriesViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import {Category} from "../../models/Category";
import {Pagination} from "../../viewmodels/Pagination";
import {ModalDirective} from "ng2-bootstrap";

@Component({
    selector: 'category-management',
    templateUrl: 'category-management.component.html',
    providers: [
        ClientCategoryService,
        ClientTimeService,

        ClientApiService,
        ClientConfigurationService,
        ClientAuthenticationService,

        ClientNotificationService
    ],
})

export class CategoryManagementComponent implements OnInit {

    // List of categories responded from service.
    private categorySearchResult: CategorySearchDetailViewModel;

    // Whether records are being searched or not.
    private isLoading: boolean;

    // Conditions which are used for searching categories.
    private findCategoriesViewModel: FindCategoriesViewModel;

    // Category which is currently selected to be edited/deleted.
    private selectCategoryDetail : CategoryDetailViewModel;

    // Initiate component with dependency injections.
    public constructor(public clientCategoryService: ClientCategoryService,
                       public clientConfigurationService: ClientConfigurationService,
                       public clientApiService: ClientApiService,
                       public clientNotificationService: ClientNotificationService,
                       public clientTimeService: ClientTimeService) {

        // Initiate categories search result.
        this.categorySearchResult = new CategorySearchDetailViewModel();
    }

    // Callback is fired when a category is created to be removed.
    public clickRemoveCategory(categoryDetail: CategoryDetailViewModel, deleteCategoryConfirmModal: any): void {

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
        let findCategoriesConditions = new FindCategoriesViewModel();
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
        this.clientCategoryService.changeCategoryDetails(category.id, category)
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
                this.clientNotificationService.success(`${information['name']} has been created successfully`, 'System');

                // Close the modal.
                initiateCategoryModal.hide();

                // Reload search results.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel content loading.
                this.isLoading = false;

                // Proceed common function to handle invalid process.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Freeze the find box.
        this.isLoading = true;

        // Reset the selected category detail.
        this.selectCategoryDetail = null;

        // Find categories by using specific conditions.
        this.clientCategoryService.findCategories(this.findCategoriesViewModel)
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
                this.clientApiService.proceedHttpNonSolidResponse(response);
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
        this.findCategoriesViewModel = new FindCategoriesViewModel();

        // Refactoring pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.findCategoriesViewModel.pagination = pagination;
    }
}