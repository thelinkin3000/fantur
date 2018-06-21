import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Estadia } from '../../models/estadia';
import { Observable } from 'rxjs';
import { EstadiasService } from '../../services/estadias.service';

@Component({
  selector: 'app-estadia-editor',
  templateUrl: './estadia-editor.component.html',
  styleUrls: ['./estadia-editor.component.css']
})

export class EstadiaEditorComponent implements OnInit {

    public estadia : Estadia;
    private id: number;

    constructor(private route: ActivatedRoute, private estadiasService:EstadiasService, private router:Router) {
    }

    save() {
        this.estadiasService.saveEstadia(this.estadia).subscribe(result => {
                this.router.navigate(['estadias']);
            },
            error => { console.log(error) });
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
            this.estadiasService.getEstadia(this.id.toString()).subscribe(result => {
                    console.log(result);
                    this.estadia = result;
                },
                error => console.error(error));
        } else {
            this.estadia = new Estadia();
        }
  }

}