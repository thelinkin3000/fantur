import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Estadia } from '../models/Estadia'
import { RequestResultVm } from '../models/RequestResultVm';


@Injectable()
export class EstadiasService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) {}

    url: string = '/api/Estadias'

    public getEstadias(): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url);
    }

    public getEstadia(id: string): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url + '/' + id)
    }

    
    public saveEstadia(estadia:Estadia) {
        return this.httpClient
            .post<Estadia>(this.url, estadia, this.httpOptions);
    }

}
