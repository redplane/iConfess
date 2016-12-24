import {Injectable} from '@angular/core';
import {FindCategoriesViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import {HyperlinkService} from "../HyperlinkService";
import {Http, Headers, RequestOptions, Response} from "@angular/http";
import 'rxjs/add/operator/toPromise';
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Pagination} from "../../viewmodels/Pagination";
import {IClientAccountService} from "../../interfaces/services/IClientAccountService";
import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientAccountService implements IClientAccountService {

    // Service which handles hyperlink.
    private _hyperlinkService: HyperlinkService;

    // HttpClient which is used for handling request to web api service.
    private _httpClient: Http;

    // Initiate instance of category service.
    public constructor(hyperlinkService: HyperlinkService, httpClient: Http){

        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;
    }

    // Find categories by using specific conditions.
    public findAccounts(findAccountsViewModel: FindAccountsViewModel) {

        // Page index should be decrease by one.
        let conditions = Object.assign({}, findAccountsViewModel);
        conditions['pagination'] = Object.assign({}, findAccountsViewModel.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._hyperlinkService.apiFindAccount, conditions, requestOptions)
            .toPromise();
    }
}