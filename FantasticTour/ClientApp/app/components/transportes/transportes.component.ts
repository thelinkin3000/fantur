import { Component, OnInit } from '@angular/core';
import { TransportesService } from '../../services/transportes.service'
import { Transporte } from '../../models/transporte';

@Component({
  selector: 'app-transportes',
  templateUrl: './transportes.component.html',
  styleUrls: ['./transportes.component.css']
})

export class TransportesComponent implements OnInit {

    transportes: Transporte[];
    
    constructor(private transportesService: TransportesService) {
    }

    ngOnInit() {

        this.getTransporte();
    }

    getTransporte() {
        this.transportesService.getAll().subscribe(result => {
            console.log(result);
            if (result.valid) {
                this.transportes = JSON.parse(result.message);
            }
        }, error => console.error(error));
    }

}