import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";



import { MailingService } from '../../services/mailing.service';
import { Mailing } from '../../models/Mailing';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { RequestResultVm } from "../../models/RequestResultVm";

@Component({
  selector: 'app-send-mailing',
  templateUrl: './send-mailing.component.html',
  styleUrls: ['./send-mailing.component.css']
})
export class SendMailingComponent implements OnInit {

    constructor(private mailingService: MailingService,
        private modalService: NgbModal,
        private router: Router) { }
    
    public mailing:Mailing;

    ngOnInit() {
        this.mailing = new Mailing();
    }

    send() {
        this.mailingService.send(this.mailing)
            .subscribe(result => {
                    if (result.valid) {
                        this.router.navigate(['/']);
                    } else {
                        this.displayError(result.message, "Whoops");
                    }
                },
                error => this.displayError(error, "Whoops"));
    }


    displayError(content: string, title: string) {
        console.log(content);
        const modalReference = this.modalService.open(ErrorModalComponent);
        modalReference.componentInstance.errorMessage = content;
        modalReference.componentInstance.title = title;
    }

}
