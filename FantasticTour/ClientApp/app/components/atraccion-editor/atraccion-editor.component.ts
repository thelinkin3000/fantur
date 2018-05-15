import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';



import { Atraccion } from '../../models/Atraccion'

@Component({
  selector: 'app-atraccion-editor',
  templateUrl: './atraccion-editor.component.html',
  styleUrls: ['./atraccion-editor.component.css']
})
export class AtraccionEditorComponent implements OnInit {
    public atraccion: Atraccion;

    httpClient: HttpClient;
    baseUrl: string;
    id: number;

    constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
        this.httpClient = httpClient;
        this.baseUrl = baseUrl;
        this.route.queryParams
            .subscribe(params => {
                console.log(params);
                if (params.id != null) {
                    this.id = params.id;
                }
                console.log(this.id);
            });

        if (this.id != null) {
            this.httpClient.get<Atraccion>(baseUrl + 'api/Atracciones/' + this.id).subscribe(result => {
                   console.log("Lo bueno buenazo jovenes");
                console.log(result);
                this.atraccion = result;
            },
                error => console.error(error));
        } else {
            this.atraccion = new Atraccion();
        }
        console.log(this.atraccion);

    }

  ngOnInit() {
  }

}
