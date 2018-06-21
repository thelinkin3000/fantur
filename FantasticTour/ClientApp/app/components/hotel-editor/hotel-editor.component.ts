import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Hotel } from '../../models/hotel';
import { Observable } from 'rxjs';
import { HotelesService } from '../../services/hoteles.service';

@Component({
  selector: 'app-hotel-editor',
  templateUrl: './hotel-editor.component.html',
  styleUrls: ['./hotel-editor.component.css']
})

export class HotelEditorComponent implements OnInit {

    public hotel : Hotel;
    private id: number;

    constructor(private route: ActivatedRoute, private hotelesService:HotelesService, private router:Router) {
    }

    save() {
        this.hotelesService.saveHotel(this.hotel).subscribe(result => {
                this.router.navigate(['hoteles']);
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
            this.hotelesService.getHotel(this.id.toString()).subscribe(result => {
                    console.log(result);
                    this.hotel = result;
                },
                error => console.error(error));
        } else {
            this.hotel = new Hotel();
        }
  }

}
