import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Paquete } from '../models/paquete'

@Injectable()
export class PaquetesService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) {}

    url: string = '/api/Paquetes'

    public getPaquetes(): Observable<Paquete[]> {
        return this.httpClient.get<Paquete[]>(this.url);
    }

    public getPaquete(id: string): Observable<Paquete> {
        return this.httpClient.get<Paquete>(this.url + '/' + id)
    }

    
    public savePaquete(Paquete:Paquete) {
        return this.httpClient
            .post<Paquete>(this.url, Paquete, this.httpOptions);
    }

}
