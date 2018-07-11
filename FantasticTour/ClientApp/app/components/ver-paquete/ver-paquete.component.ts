import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Paquete } from '../../models/paquete';
import { Estadia } from '../../models/estadia';
import { Transporte  } from '../../models/transporte';
import { Atraccion } from '../../models/atraccion';
import { PaquetesService } from '../../services/paquetes.service';
import { AtraccionesService } from '../../services/atracciones.service';
import { TransportesService } from '../../services/transportes.service';
import { EstadiasService } from '../../services/estadias.service';

@Component({
  selector: 'app-ver-paquete',
  templateUrl: './ver-paquete.component.html',
  styleUrls: ['./ver-paquete.component.css']
})
export class VerPaqueteComponent implements OnInit {

  public paquete : Paquete;
  public estadia : Estadia;
  public transporte : Transporte;
  public atraccion : Atraccion;
  private id: number;
  private lowDate: string;
  private highDate: string;
  private origenId: number;
  private destinoId: number;


  constructor(private route: ActivatedRoute, 
      private paquetesService:PaquetesService, 
      private estadiasService:EstadiasService, 
      private transportesService:TransportesService, 
      private atraccionesService:AtraccionesService, 
      private router:Router) {
  }

  ngOnInit() {
    this.route.queryParams
    .subscribe(params => {
        console.log(params);
        if (params.id != null) {
            this.id = params.id;
        }
        console.log(this.id);
    });

    this.paquetesService.getPaquete(this.id.toString()).subscribe(result => {
        console.log(result);
        if (result.valid) {
            this.paquete = JSON.parse(result.message);
            if (this.paquete) {
                //Traemos los childs
                this.estadiasService.getEstadia(this.paquete.estadiaId.toString()).subscribe(result => {
                    if (result.valid) {
                        this.estadia = JSON.parse(result.message);
                    }
                });
                this.transportesService.get(this.paquete.transporteId.toString()).subscribe(result => {
                    if (result.valid) {
                        this.transporte = JSON.parse(result.message);
                    }
                });
                this.atraccionesService.get(this.paquete.atraccionId.toString()).subscribe(result => {
                    if (result.valid) {
                        this.atraccion = JSON.parse(result.message);
                    }
                });
            }
        }
            
    },
    error => console.error(error));
        
  }

}

