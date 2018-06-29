import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Mailing } from '../models/Mailing'
import { RequestResultVm } from '../models/RequestResultVm';

@Injectable({
  providedIn: 'root'
})
export class MailingService {
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("userToken")
        })
    };

    constructor(private httpClient: HttpClient) { }

    url: string = '/api/Mailing/SendMailing';

    public send(mailing:Mailing) {
        return this.httpClient
            .post<RequestResultVm>(this.url, mailing, this.httpOptions);
    }
}
