import { Injectable } from '@angular/core';
import { Atraccion } from '../models/Atraccion';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { RequestResultVm } from '../models/RequestResultVm';

@Injectable()
export class AtraccionesService {
    constructor(private httpClient: HttpClient) { }
    url : string = 'api/atracciones';
    autocompleteUrl:string = 'api/autocomplete/atraccion';
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };
    public getAll() {
        return this.httpClient.get<RequestResultVm>(this.url);
    }

    public get(id: string) {
        return this.httpClient.get<RequestResultVm>(this.url + '/' + id);
    }

    public save(atraccion: Atraccion) {
        return this.httpClient.post<RequestResultVm>(this.url, atraccion, this.httpOptions);
    }
    public autocomplete(query: string) {
        if (query) {
            let params: HttpParams = new HttpParams().set('query', query);
            return this.httpClient.get<RequestResultVm>(this.autocompleteUrl, { params: params });
        }
    }
}
