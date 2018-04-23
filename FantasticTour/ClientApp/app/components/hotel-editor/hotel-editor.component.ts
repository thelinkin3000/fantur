import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {ActivatedRoute } from '@angular/router';

import { Hotel } from '../../models/hotel';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-hotel-editor',
  templateUrl: './hotel-editor.component.html',
  styleUrls: ['./hotel-editor.component.css']
})
export class HotelEditorComponent implements OnInit {

    public hotel : Hotel;
    private id: number;
    private httpClient: HttpClient;
    private baseUrl: string;
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };

    constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
        this.httpClient = httpClient;
        this.baseUrl = baseUrl;
        this.route.queryParams
            .subscribe(params => {
                console.log(params); 
                this.id = params.id;
                console.log(this.id);
            });
        this.httpClient.get<Hotel>(baseUrl + 'api/Hoteles/' + this.id).subscribe(result => {
            console.log(result);
            this.hotel = result;
            
        }, error => console.error(error));
    }

    save(){
        this.httpClient
            .post<Hotel>(this.baseUrl + 'api/Hoteles', this.hotel, this.httpOptions)
            .subscribe(result => console.log(result));
    }

  ngOnInit() {
  }

}
