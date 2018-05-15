import { Component, OnInit, Inject } from '@angular/core';
import { Hotel } from '../../models/hotel';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hoteles',
  templateUrl: './hoteles.component.html',
  styleUrls: ['./hoteles.component.css']
})
export class HotelesComponent implements OnInit {

    hoteles: Hotel[];
    
    constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        httpClient.get<Hotel[]>(baseUrl + 'api/Hoteles').subscribe(result => {
            console.log(result);
            this.hoteles = result;
        }, error => console.error(error));
    }

  ngOnInit() {
  }

}
