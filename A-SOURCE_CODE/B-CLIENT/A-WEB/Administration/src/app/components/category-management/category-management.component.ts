import {Component, Inject, OnInit, ViewChild} from '@angular/core'
import {Response} from "@angular/http";
import {CategoryInitiateBoxComponent} from "./category-initiate-box.component";
import {CategoryDetailBoxComponent} from "./category-detail-box.component";
import {CategoryDeleteBoxComponent} from "./category-delete-box.component";
import {SearchCategoriesViewModel} from "../../../viewmodels/category/search-categories.view-model";
import {Sorting} from "../../../models/sorting";
import {SortDirection} from "../../../enumerations/sort-direction";
import {CategorySortProperty} from "../../../enumerations/order/category-sort-property";
import {Pagination} from "../../../models/pagination";
import {SearchResult} from "../../../models/search-result";
import {CategoryDetailsViewModel} from "../../../viewmodels/category/category-details.view-model";
import {Category} from "../../../models/entities/category";
import {IClientCategoryService} from "../../../interfaces/services/api/IClientCategoryService";
import {IClientApiService} from "../../../interfaces/services/api/IClientApiService";
import {IClientToastrService} from "../../../interfaces/services/client-toastr-service.interface";
import {IClientTimeService} from "../../../interfaces/services/client-time-service.interface";
import {IClientCommonService} from "../../../interfaces/services/client-common-service.interface";
import {ModalDirective} from "ngx-bootstrap";

@Component({
    selector: 'category-management',
    templateUrl: 'category-management.component.html'
})

export class CategoryManagementComponent implements OnInit {

    //#region Properties

    // Modal which contains category initiator.
    @ViewChild('initiateCategoryInfoModal')
    public initiateCategoryInfoModal: ModalDirective;

    // Initiator contains fields to initiate category.
    @ViewChild('categoryInitiatorContent')
    public categoryInitiatorContent: CategoryInitiateBoxComponent;

    // Editor contains fields to edit category.
    @ViewChild('categoryDetailBox')
    public categoryDetailBox: CategoryDetailBoxComponent;

    // Edit modal container.
    @ViewChild('categoryDetailModal')
    public categoryDetailModal: ModalDirective;

    // Component contains information which is for deleting box.
    @ViewChild('categoryDeleteBox')
    public categoryDeleteBox: CategoryDeleteBoxComponent;

    // Modal which contains component for deleting category.
    @ViewChild('deleteCategoryModal')
    public deleteCategoryModal: ModalDirective;

    // List of categories responded from service.
    private searchResult: SearchResult<CategoryDetailsViewModel>;

    // Whether records are being searched or not.
    private isBusy: boolean;

    // Conditions which are used for searching categories.
    private conditions: SearchCategoriesViewModel;

    // List of accouts which are used for selecting.
    private accounts: Array<Account>;

    //#endregion

    //#region Constructor

    // Initiate component with dependency injections.
    public constructor(@Inject("IClientCategoryService") public clientCategoryService: IClientCategoryService,
                       @Inject("IClientApiService") public clientApiService: IClientApiService,
                       @Inject("IClientToastrService") public clientToastrService: IClientToastrService,
                       @Inject("IClientTimeService") public clientTimeService: IClientTimeService,
                       @Inject('IClientCommonService') public clientCommonService: IClientCommonService) {

        // Initiate search condition.
        let conditions = new SearchCategoriesViewModel();
        let sorting = new Sorting<CategorySortProperty>();
        sorting.direction = SortDirection.Ascending;
        sorting.property = CategorySortProperty.index;
        conditions.sorting = sorting;

        let pagination = new Pagination();
        pagination.page = 1;
        pagination.records = this.clientCommonService.getMaxPageRecords();
        conditions.pagination = pagination;

        // Update initial conditions.
        this.conditions = conditions;

        // Initiate categories search result.
        this.searchResult = new SearchResult<CategoryDetailsViewModel>();
    }

    //#endregion

    //#region Methods

    // Callback is fired when a category is created to be removed.
    public clickRemoveCategory(categoryDetail: CategoryDetailsViewModel): void {

        // Category detail is not valid.
        if (categoryDetail == null)
            return;

        console.log(categoryDetail);

        // Set the detail first.
        this.categoryDeleteBox.setDetails(categoryDetail);

        // Show the modal.
        this.deleteCategoryModal.show();
    }

    // This callback is called when user confirms to delete the selected category.
    public clickConfirmDeleteCategory() {

        // Find category by using specific conditions.
        let details = this.categoryDeleteBox.getDetails();
        if (details == null){
            return;
        }

        // Make the loading start.
        this.isBusy = true;

        // Initiate search category view model.
        let conditions = new SearchCategoriesViewModel();
        conditions.id = details.id;

        // Call category service to delete the selected category.
        this.clientCategoryService.deleteCategories(conditions)
            .then((x: Response | any) => {

                // Cancel loading state.
                this.isBusy = false;

                // Close the modal.
                this.deleteCategoryModal.hide();

                // Reload the categories list.
                this.clickSearch();
            })
            .catch((response: any) => {
                // Cancel loading.
                this.isBusy = false;

                // Proceed common invalid response.
                this.clientApiService.handleInvalidResponse(response);
            });


    }

    // Callback which is fired when change category detail button is clicked.
    public clickChangeCategoryDetail(categoryDetail: CategoryDetailsViewModel) {

        // Category detail is invalid.
        if (categoryDetail == null)
            return;

        // Set details to editor.
        this.categoryDetailBox.setDetails(categoryDetail);

        // Display the change category detail box.
        this.categoryDetailModal.show();
    }

    // Callback which is fired when change category detail action is confirmed.
    public clickConfirmChangeCategoryDetail() {

        // Selected category detail is invalid.
        let details = this.categoryDetailBox.getDetails();

        // Category details is not valid.
        if (details == null) {
            return;
        }

        // Initiate category information.
        let category = new Category();
        category.id = details.id;
        category.name = details.name;

        // Start loading.
        this.isBusy = true;

        // Call service to update category information.
        this.clientCategoryService.editCategoryDetails(category.id, category)
            .then((x: Response | any) => {
                // Close the modal.
                this.categoryDetailModal.hide();

                // Clear busy state.
                this.isBusy = false;

                // Reload the categories list.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel loading.
                this.isBusy = false;
            });

    }

    // Callback which is fired when a category should be created into system.
    public clickInitiateCategory(): void {

        // Display category initial modal.
        this.initiateCategoryInfoModal.show();
    }

    // This callback is fired when confirm button is clicked which tells client to send request to service to create a new category.
    public clickConfirmInitiateCategory(): void {

        // Find category defined in initiator dialog.
        let category = this.categoryInitiatorContent.getInitiator();

        // Invalid category detected.
        if (category == null) {
            return;
        }

        // Call service to initiate category.
        this.clientCategoryService.initiateCategory(category)
            .then((x: Response | any) => {
                // Cancel content loading.
                this.isBusy = false;

                // Parse information of response.
                let category = <Category> x.json();

                // Display notification to client screen.
                this.clientToastrService.success(`${category.name} has been created successfully`, 'System', null);

                // Close the modal.
                this.initiateCategoryInfoModal.hide();

                // Reload search results.
                this.clickSearch();
            })
            .catch((response: Response | any) => {
                // Cancel content loading.
                this.isBusy = false;

                // Proceed common function to handle invalid process.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Freeze the find box.
        this.isBusy = true;

        // Find categories by using specific conditions.
        this.clientCategoryService.getCategories(this.conditions)
            .then((x: Response) => {

                // Update categories list.
                this.searchResult = <SearchResult<CategoryDetailsViewModel>> x.json();

                // Unfreeze the category find box.
                this.isBusy = false;
            })
            .catch((response: Response | any) => {

                // Unlock screen components.
                this.isBusy = false;

                // Call common function to handle error response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when page selection is changed.
    public selectPage(page: number): void {
        // Update pagination page.
        this.conditions.pagination.page = page;

        // Call search function.
        this.clickSearch();
    }

    // This callback is fired when category management component is initiated.
    public ngOnInit() {
        // Search for categories using specific conditions.
        this.clickSearch();
    }

    //#endregion
}
