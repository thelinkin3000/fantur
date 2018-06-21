import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-email-confirm',
  templateUrl: './email-confirm.component.html',
  styleUrls: ['./email-confirm.component.css']
})
export class EmailConfirmComponent implements OnInit {
    private userId: string;
    private token:string;

    constructor(private route: ActivatedRoute, private router: Router, private userService:UserService, private modalService:NgbModal) { }

    ngOnInit() {
        this.route.queryParams
            .subscribe(params => {
                console.log(params);
                if (params.userId != null) {
                    this.userId = params.userId;
                }
                if (params.token != null) {
                    this.token = params.token;
                }
            });

        if (this.userId != null && this.token != null) {
            this.userService.confirmEmail(this.userId, this.token).subscribe(result => {
                    if (result.valid) {
                        this.router.navigate(['login']);
                    } else {
                        this.displayError("No pudimos encontrar tu mail entre los mails a verificar. Por favor, intentá mas tarde.","Whoops!");
                    }
                },
                error => console.error(error));
        }
    }
    displayError(content: string, title: string) {
        console.log(content);
        const modalReference = this.modalService.open(ErrorModalComponent);
        modalReference.componentInstance.errorMessage = content;
        modalReference.componentInstance.title = title;
    }

}
