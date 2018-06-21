import { Component, OnInit, Inject } from '@angular/core';
import { Atraccion } from '../../models/atraccion';
import { HttpClient } from '@angular/common/http';
import { AtraccionesService } from '../../services/atracciones.service';

@Component({
  selector: 'app-atracciones',
  templateUrl: './atracciones.component.html',
  styleUrls: ['./atracciones.component.css']
})
export class AtraccionesComponent implements OnInit {

    atracciones: Atraccion[];

    constructor(private atraccionesService : AtraccionesService) {
        
    }

    ngOnInit() {
        this.atraccionesService.getAll().subscribe(result => {
            this.atracciones = result;
        }, error => console.error(error));
    }
}
