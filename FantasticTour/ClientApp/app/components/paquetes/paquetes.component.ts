import { Component, OnInit } from '@angular/core';
import { Paquete } from '../../models/paquete';
import { Filtro } from '../../models/Filtro';
import { HttpClient } from '@angular/common/http';
import { PaquetesService } from '../../services/paquetes.service';
import { CiudadesService } from '../../services/ciudades.service';
import { ActivatedRoute, Router } from "@angular/router";
import { FormControl } from "@angular/forms";
import { Observable, BehaviorSubject } from "rxjs";
import { AutocompleteResultVm } from "../../models/autocompleteResultVm";
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ErrorModalComponent } from '../error-modal/error-modal.component';



@Component({
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.css']
})
export class PaquetesComponent implements OnInit {

  paquetes: Paquete[];
    filtro:Filtro;
    origenSearchTerm = new FormControl();
    origenSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    destinoSearchTerm = new FormControl();
    destinoSubject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    lowDate:string;
    highDate:string;
    origenId:number;
    destinoId:number;

  constructor(private paquetesService : PaquetesService,
      private ciudadesService: CiudadesService,
      private route: ActivatedRoute,
      private modalService: NgbModal,
      private router: Router) { }

  ngOnInit() {
      this.route.queryParams
          .subscribe(params => {
              console.log(params);
              //filtros
          });

      this.filtro = new Filtro();
    this.paquetesService.getPaquetes(this.filtro).subscribe(result => {
      console.log(result);
        if (result.valid) {
            this.paquetes = JSON.parse(result.message);
        }
    }, error => console.error(error));

      this.origenSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.ciudadesService,this.origenSubject);
      });
      this.destinoSearchTerm.valueChanges.subscribe(data => {
          this.autocomplete(data,this.ciudadesService,this.destinoSubject);
      });
  }

    async select(item : AutocompleteResultVm, control:FormControl, subject:BehaviorSubject<AutocompleteResultVm[]>, func, value) {
        control.setValue(item.text);
        func(item, value);
        debugger;
        subject.next([]);
        console.log(this.filtro);
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
    setOrigen(item: AutocompleteResultVm, value:Filtro) {
        console.log(this);
        value.origenId= item.value;
    }
    setDestino(item: AutocompleteResultVm, value:Filtro) {
        value.destinoId = item.value;
    }

    filter() {
        this.paquetesService.getPaquetes(this.filtro).subscribe(result => {
            console.log(result);
            if (result.valid) {
                this.paquetes = JSON.parse(result.message);
            }
        }, error => console.error(error));
    }

}