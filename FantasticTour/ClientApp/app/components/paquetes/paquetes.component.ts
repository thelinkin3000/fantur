import { Component, OnInit } from '@angular/core';
import { Paquete } from '../../models/paquete';
import { HttpClient } from '@angular/common/http';
import { PaquetesService } from '../../services/paquetes.service';

@Component({
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.css']
})
export class PaquetesComponent implements OnInit {

  paquetes: Paquete[];

  constructor(private paquetesService : PaquetesService) { }

  ngOnInit() {
    this.paquetesService.getPaquetes().subscribe(result => {
      console.log(result);
      this.paquetes = result;
    }, error => console.error(error));
  }

}