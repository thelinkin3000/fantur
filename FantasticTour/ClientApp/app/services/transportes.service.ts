import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transporte } from '../models/transporte';

@Injectable()
export class TransportesService {
  private httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

constructor(private httpClient: HttpClient) {}

url: string = '/api/Transportees';

public getTransportees(): Observable<Transporte[]> {
    return this.httpClient.get<Transporte[]>(this.url);
}

public getTransporte(id: string): Observable<Transporte> {
    return this.httpClient.get<Transporte>(this.url + '/' + id)
}

public saveTransporte(Transporte:Transporte) {
    return this.httpClient
        .post<Transporte>('api/Transportees', Transporte, this.httpOptions);
}

}
