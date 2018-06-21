import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Ciudad } from '../models/Ciudad'
import { RequestResultVm } from '../models/RequestResultVm';

@Injectable({
  providedIn: 'root'
})
export class CiudadesService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) { }

    url: string = '/api/Ciudades';
    autocompleteUrl: string = '/api/Autocomplete/Ciudad';


    public getAll(): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url);
    }

    public get(id: string): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url + '/' + id);
    }

    public save(ciudad: Ciudad) {
        return this.httpClient
            .post<RequestResultVm>(this.url, ciudad, this.httpOptions);
    }

    public autocomplete(query: string) {
        if (query) {
            let params: HttpParams = new HttpParams().set('query', query);
            return this.httpClient.get<RequestResultVm>(this.autocompleteUrl, { params: params });
        }
    }
}
