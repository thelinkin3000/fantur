import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transporte } from '../models/transporte';

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

public getAll(): Observable<Transporte[]> {
    return this.httpClient.get<Transporte[]>(this.url);
}

public get(id: string): Observable<Transporte> {
    return this.httpClient.get<Transporte>(this.url + '/' + id)
}

public save(transporte:Transporte) {
    return this.httpClient
        .post<Transporte>('api/Transportees', transporte, this.httpOptions);
}

}
