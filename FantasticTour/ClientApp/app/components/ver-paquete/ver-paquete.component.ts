import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Paquete } from '../../models/paquete';
import { PaquetesService } from '../../services/paquetes.service';

@Component({
  selector: 'app-ver-paquete',
  templateUrl: './ver-paquete.component.html',
  styleUrls: ['./ver-paquete.component.css']
})
export class VerPaqueteComponent implements OnInit {

  public paquete : Paquete;
  private id: number;


  constructor(private route: ActivatedRoute, private paquetesService:PaquetesService, private router:Router) {
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
        this.paquete = result;
    },
    error => console.error(error));
        
  }

}

