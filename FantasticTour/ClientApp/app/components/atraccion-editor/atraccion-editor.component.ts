import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Atraccion } from '../../models/atraccion';
import { Observable } from 'rxjs';
import { AtraccionesService } from '../../services/atracciones.service';

@Component({
  selector: 'app-atraccion-editor',
  templateUrl: './atraccion-editor.component.html',
  styleUrls: ['./atraccion-editor.component.css']
})

export class AtraccionEditorComponent implements OnInit {

    public atraccion : Atraccion;
    private id: number;

    constructor(private route: ActivatedRoute, private atraccionesService:AtraccionesService, private router:Router) {
    }

    save() {
        this.atraccionesService.save(this.atraccion).subscribe(result => {
                this.router.navigate(['atracciones']);
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
            this.atraccionesService.get(this.id.toString()).subscribe(result => {
                    console.log(result);
                    this.atraccion = result;
                },
                error => console.error(error));
        } else {
            this.atraccion = new Atraccion();
        }
  }

}