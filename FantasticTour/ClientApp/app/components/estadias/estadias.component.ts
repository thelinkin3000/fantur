import { Component, OnInit } from '@angular/core';
import { Estadia } from '../../models/estadia';
import { HttpClient } from '@angular/common/http';
import { EstadiasService } from '../../services/estadias.service';

@Component({
  selector: 'app-estadias',
  templateUrl: './estadias.component.html',
  styleUrls: ['./estadias.component.css']
})
export class EstadiasComponent implements OnInit {

    estadias: Estadia[];

    constructor(private estadiasService : EstadiasService) {
        
    }

    ngOnInit() {
        this.estadiasService.getEstadias().subscribe(result => {
            this.estadias = result;
        }, error => console.error(error));
        
    }
}