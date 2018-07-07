import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Pago } from '../models/Pago'

@Injectable()
export class PagosService {

    private httpOptions = {
      headers: new HttpHeaders({
          'Content-Type': 'application/json',
      })
  };

  constructor(private httpClient: HttpClient) {}

  url: string = 'http://localhost:8000/api/pagos/save';

  public save(pago: Pago) {
    return this.httpClient.post(this.url, pago, this.httpOptions);
  }

}

