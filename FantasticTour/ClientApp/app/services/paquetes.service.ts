import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Paquete } from '../models/paquete';
import { CrearPaquete } from '../models/CrearPaquete';
import { RequestResultVm } from '../models/RequestResultVm';
import { Filtro } from '../models/Filtro';


@Injectable()
export class PaquetesService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) {}

    url: string = '/api/Paquetes';

    public getPaquetes(filtro:Filtro): Observable<RequestResultVm> {
        let query: string;
        let first: boolean = true;
        if (filtro) {
            query = '?';
            if (filtro.destinoId) {
                first = false;
                query.concat('destinoId=' + filtro.destinoId.toString());
            }
            if (filtro.origenId) {
                if (first) {
                    first = false;
                } else {
                    query.concat('&');
                }
                query.concat('destinoId=' + filtro.destinoId.toString());
            }
            if (filtro.ida) {
                if (first) {
                    first = false;
                } else {
                    query.concat('&');
                }
                query.concat('ida=' + filtro.ida);
            }
            if (filtro.vuelta) {
                if (!first) {
                    query.concat('&');
                }
                query.concat('vuelta=' + filtro.ida);
            }    
        }
        let url = filtro ? this.url.concat(query) : this.url;
        return this.httpClient.get<RequestResultVm>(url);
    }

    public getPaquete(id: string): Observable<RequestResultVm> {
        return this.httpClient.get<RequestResultVm>(this.url + '/' + id);
    }
    
    public savePaquete(paquete:CrearPaquete) {
        return this.httpClient
            .post<RequestResultVm>(this.url, paquete, this.httpOptions);
    }

}
