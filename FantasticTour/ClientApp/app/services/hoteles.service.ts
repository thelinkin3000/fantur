import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Hotel } from '../models/Hotel'
import { RequestResultVm } from '../models/RequestResultVm';


@Injectable()
export class HotelesService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) {}

    url: string = '/api/Hoteles';
    autocompleteUrl:string = '/api/Autocomplete/Hotel';
    public getAll(): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url);
    }

    public get(id: string): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url + '/' + id);
    }

    public save(hotel:Hotel) {
        return this.httpClient
            .post<RequestResultVm>(this.url, hotel, this.httpOptions);
    }
    
    public autocomplete(query: string) {
        if (query) {
            let params: HttpParams = new HttpParams().set('query', query);
            return this.httpClient.get<RequestResultVm>(this.autocompleteUrl, { params: params });
        }
    }

}
