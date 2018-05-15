import { Component, OnInit, Inject } from '@angular/core';
import { Atraccion } from '../../models/atraccion';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-atracciones',
  templateUrl: './atracciones.component.html',
  styleUrls: ['./atracciones.component.css']
})
export class AtraccionesComponent implements OnInit {

    atracciones: Atraccion[];

    constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        httpClient.get<Atraccion[]>(baseUrl + 'api/Atracciones').subscribe(result => {
            this.atracciones = result;
        }, error => console.error(error));
    }

  ngOnInit() {
  }

}
