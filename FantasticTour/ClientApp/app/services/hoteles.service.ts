import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Hotel } from '../models/Hotel'


@Injectable()
export class HotelesService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };

    constructor(private httpClient: HttpClient) {}

    url: string = '/api/Hoteles';

    public getHoteles(): Observable<Hotel[]> {
        return this.httpClient.get<Hotel[]>(this.url);
    }

    public getHotel(id: string): Observable<Hotel> {
        return this.httpClient.get<Hotel>(this.url + '/' + id)
    }

    public saveHotel(hotel:Hotel) {
        return this.httpClient
            .post<Hotel>('api/Hoteles', hotel, this.httpOptions);
    }

}
