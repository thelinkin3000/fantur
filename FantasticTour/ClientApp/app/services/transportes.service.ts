import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transporte } from '../models/transporte';
import { RequestResultVm } from '../models/RequestResultVm';


@Injectable()
export class TransportesService {
  private httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("userToken")
    })
};

constructor(private httpClient: HttpClient) {}

url: string = '/api/Transportes';

public getAll(): Observable<RequestResultVm> {
    return this.httpClient.get<RequestResultVm>(this.url);
}

public get(id: string): Observable<RequestResultVm> {
    return this.httpClient.get<RequestResultVm>(this.url + '/' + id);
}

public save(transporte:Transporte) {
    return this.httpClient
        .post<Transporte>('api/Transportees', transporte, this.httpOptions);
}

}
