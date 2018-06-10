import { Component, OnInit} from '@angular/core';
import { Hotel } from '../../models/hotel';
import { HotelesService } from '../../services/hoteles.service'

@Component({
  selector: 'app-hoteles',
  templateUrl: './hoteles.component.html',
  styleUrls: ['./hoteles.component.css']
})
export class HotelesComponent implements OnInit {

    hoteles: Hotel[];
    
    constructor(private hotelesService: HotelesService) {
    }

    ngOnInit() {
        this.getHoteles();
    }

    getHoteles() {
        this.hotelesService.getHoteles().subscribe(result => {
            console.log(result);
            this.hoteles = result;
        }, error => console.error(error));
    }

}
