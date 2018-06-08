import { Injectable } from '@angular/core';
import { Atraccion } from '../models/Atraccion';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AtraccionesService {
    constructor(private http: HttpClient) { }
    url : string = 'api/atracciones';
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };
    public getAll() {
        return this.http.get<Atraccion[]>(this.url);
    }

    public get(id: string) {
        return this.http.get<Atraccion>(this.url + '/' + id);
    }

    public save(atraccion: Atraccion) {
        return this.http.post<Atraccion>(this.url, atraccion, this.httpOptions);
    }

}
