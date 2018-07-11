import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormControl } from "@angular/forms";
import { Observable, BehaviorSubject } from "rxjs";

import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';



import { Paquete } from '../../models/paquete';
import { Atraccion } from '../../models/atraccion';
import { Estadia } from '../../models/estadia';
import { Hotel } from '../../models/hotel';
import { Transporte } from '../../models/transporte';
import { TransportesService } from '../../services/transportes.service';
import { EstadiasService } from '../../services/estadias.service';
import { AtraccionesService } from '../../services/atracciones.service';
import { PaquetesService } from '../../services/paquetes.service';
import { HotelesService } from '../../services/hoteles.service';
import { CiudadesService } from "../../services/ciudades.service";
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { AutocompleteResultVm } from "../../models/autocompleteResultVm";
import { CrearPaquete } from '../../models/CrearPaquete';




@Component({
  selector: 'app-paquete-editor',
  templateUrl: './paquete-editor.component.html',
  styleUrls: ['./paquete-editor.component.css']
})
export class PaqueteEditorComponent implements OnInit {

    public atraccion:Atraccion;
    public estadia:Estadia;
    public hotel:Hotel;
    public transporte:Transporte;
    public paquete:Paquete;
    private id:number;
    public nombre:string;
    public fechaVencimiento:string;
    hotelSearchTerm = new FormControl();
    hotelSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    atraccionSearchTerm = new FormControl();
    atraccionSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    origenTransporteSearchTerm = new FormControl();
    origenTransporteSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    destinoTransporteSearchTerm = new FormControl();
    destinoTransporteSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    
  constructor(private route: ActivatedRoute,
      private modalService: NgbModal,
      private router: Router,
      private paquetesService:PaquetesService,
      private atraccionesService:AtraccionesService,
      private estadiasService:EstadiasService,
      private hotelesService:HotelesService,
      private transportesService:TransportesService,
      private ciudadesService: CiudadesService) { }

  ngOnInit() {
      this.route.queryParams
      .subscribe(params => {
          console.log(params);
          if (params.id != null) {
              this.id = params.id;
          }
          console.log(this.id);
      });
      if (this.id != null) {
          this.paquetesService.getPaquete(this.id.toString()).subscribe(result => {
                  if (result.valid) {
                      this.paquete = JSON.parse(result.message);
                      if (this.paquete.transporteId != 0) {
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
                          this.estadiasService.getEstadia(this.paquete.estadiaId.toString()).subscribe(result => {
                              if (result.valid) {
                                  this.estadia = JSON.parse(result.message);
                                  if (this.estadia.hotelId != 0) {
                                      this.hotelesService.get(this.estadia.hotelId.toString()).subscribe(res => {
                                          if (res.valid) {
                                              this.hotel = JSON.parse(res.message);
                                          }
                                      })
                                  }
                              }               
                          });
                      }
                  } else {
                      this.displayError("Ocurrió un error", "Whoops");
                  }
              },
              error => this.displayError(error, "Whoops"));
            
      } else {
          this.paquete = new Paquete();
          this.estadia = new Estadia();
          this.hotel = new Hotel();
          this.transporte = new Transporte();
          this.atraccion = new Atraccion();
      }
      this.origenTransporteSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.ciudadesService,this.origenTransporteSubject);
      });
      this.destinoTransporteSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.ciudadesService,this.destinoTransporteSubject);
      });
      this.hotelSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.hotelesService,this.hotelSubject);
      });
      this.atraccionSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.atraccionesService,this.atraccionSubject);
      });
  }

    async select(item : AutocompleteResultVm, control:FormControl, subject:BehaviorSubject<AutocompleteResultVm[]>, func, value) {
        control.setValue(item.text);
        func(item, value);
        debugger;
        subject.next([]);
        console.log(this.transporte);
        console.log(this.estadia);
        console.log(this.paquete);
    }

    clearAutocomplete(subject:BehaviorSubject<AutocompleteResultVm[]>) {
        subject.next([]);
    }

    autocomplete(query: string, service,subject:BehaviorSubject<AutocompleteResultVm[]>) : AutocompleteResultVm[] {
        if (query) {
            service.autocomplete(query).subscribe(result => {
                if (!result) {
                    console.error("No se recibió ningun resultado");
                    return null;
                }
                if (!result.valid) {
                    console.error(result.message);
                    return null;
                }
                console.log(result.message);
                let res: AutocompleteResultVm[] = JSON.parse(result.message);
                subject.next(res);
            });
        }
        return null;
    }

    displayError(content: string, title: string) {
        console.log(content);
        const modalReference = this.modalService.open(ErrorModalComponent);
        modalReference.componentInstance.errorMessage = content;
        modalReference.componentInstance.title = title;
    }

    //Funciones para setear valores con autocompletes
    setTransporteOrigen(item: AutocompleteResultVm, value:Transporte) {
        console.log(this);
        value.origenId = item.value;
    }
    setTransporteDestino(item: AutocompleteResultVm, value:Transporte) {
        value.destinoId = item.value;
    }
    setHotel(item: AutocompleteResultVm, value:Estadia) {
        value.hotelId = item.value;
    }
    setAtraccion(item: AutocompleteResultVm, value: Paquete) {
        console.log('Atraccion');
        console.log(item);
        console.log(value);
        value.atraccionId = item.value;
    }

    save() {
        console.log('triggered');
        let request:CrearPaquete = new CrearPaquete;
        request.atraccionId = this.paquete.atraccionId;
        request.destinoId = this.transporte.destinoId;
        request.origenId = this.transporte.origenId;
        request.ingreso = this.estadia.fechaInicio;
        request.egreso = this.estadia.fechaFin;
        request.partida = this.transporte.fechaIda;
        request.regreso = this.transporte.fechaVuelta;
        request.costoEstadia = this.estadia.costo;
        request.costoTransporte = this.transporte.costo;
        request.hotelId = this.estadia.hotelId;
        request.nombre = this.nombre;
        request.fechaVencimiento = this.fechaVencimiento;
        console.log(request);
        this.paquetesService.savePaquete(request).subscribe(result => {
            if (result.valid) {
                this.router.navigate(["paquetes"]);
            } else {
                this.displayError(result.message, "Whoops");
            }
        })
    }
}
