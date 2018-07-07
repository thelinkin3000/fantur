import { Component, OnInit} from '@angular/core';
import { Pago } from '../../models/pago';
import { PagosService } from '../../services/pagos.service'

@Component({
  selector: 'app-pagos',
  templateUrl: './pagos.component.html',
  styleUrls: ['./pagos.component.css']
})

export class PagosComponent implements OnInit {

  pago: Pago;
  
  constructor(private pagosService: PagosService) {
  }

  confirmarPago() {
    this.pagosService.save(this.pago).subscribe(result => {
          console.log(result);
        },
        error => { console.log(error) });
  }  
  
  ngOnInit() {
    this.pago = new Pago();
  }   

}
