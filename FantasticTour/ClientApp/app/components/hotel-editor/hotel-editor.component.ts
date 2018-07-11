import { Component, OnInit, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { FormControl } from "@angular/forms";

import { Hotel } from "../../models/hotel";
import { Ciudad } from "../../models/ciudad";
import { AutocompleteResultVm } from "../../models/autocompleteResultVm";
import { RequestResultVm } from "../../models/RequestResultVm";
import { Observable, BehaviorSubject } from "rxjs";
import { HotelesService } from "../../services/hoteles.service";
import { CiudadesService } from "../../services/ciudades.service";
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';




@Component({
  selector: "app-hotel-editor",
  templateUrl: "./hotel-editor.component.html",
  styleUrls: ["./hotel-editor.component.css"]
})

export class HotelEditorComponent implements OnInit {

    hotel : Hotel;
    private id: number;
    searchTerm = new FormControl();
    subject = new BehaviorSubject<AutocompleteResultVm[]>([]);
    constructor(private route: ActivatedRoute,
        private hotelesService: HotelesService,
        private ciudadesService: CiudadesService,
        private modalService: NgbModal,
        private router: Router) {
    }

    save() {
        this.hotelesService.save(this.hotel).subscribe(result => {
            if (result.valid)
                this.router.navigate(["hoteles"]);
            else {
                this.displayError(result.message, "Whoops");
            }
            },
            error => { console.log(error) });
    }

    searchResults(): Observable<AutocompleteResultVm[]> {
        return this.subject.asObservable();
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

        if (this.id != null) {
            this.hotelesService.get(this.id.toString()).subscribe(result => {
                    if (result.valid) {
                        this.hotel = JSON.parse(result.message);
                        if (this.hotel.ciudadId != 0) {
                            this.ciudadesService.get(this.hotel.ciudadId.toString()).subscribe(result => {
                                if (result.valid) {
                                    let ciudad = JSON.parse(result.message);
                                    this.searchTerm.setValue(ciudad.nombre, { emitEvent: false });
                                }               
                            });
                        }
                    } else {
                        this.displayError("Ocurrió un error", "Whoops");
                    }
                },
                error => this.displayError(error, "Whoops"));
            
        } else {
            this.hotel = new Hotel();
        }
        this.searchTerm.valueChanges.subscribe(data => {
            this.autocomplete(data);
        });
    }

    async select(item : AutocompleteResultVm) {
        console.log("this item");
        console.log(JSON.stringify(item));
        console.log(JSON.stringify(this.searchTerm.value));
        this.searchTerm.setValue(item.text);
        console.log(JSON.stringify(this.searchTerm.value));
        this.hotel.ciudadId = item.value;
        console.log(this.hotel.ciudadId);
        this.subject.next([]);
    }

    clearAutocomplete() {
        this.subject.next([]);
    }

    autocomplete(query: string) : AutocompleteResultVm[] {
        if (query) {
            this.ciudadesService.autocomplete(query).subscribe(result => {
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
                if (res.length > 5)
                    res.splice(5, res.length - 5);
                this.subject.next(res);
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
}
