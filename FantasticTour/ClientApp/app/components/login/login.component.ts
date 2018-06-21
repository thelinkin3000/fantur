import { Component, OnInit } from '@angular/core';
import { LoginVm } from '../../models/LoginVm';
import { UserService } from '../../services/user.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    private loginVm: LoginVm;
    constructor(private userService: UserService, private modalService: NgbModal, private router: Router) { }

    ngOnInit() {
        this.loginVm = new LoginVm();
    }

    login() {
        console.log(this.loginVm);
        this.userService.login(this.loginVm).subscribe(result => {
                if (result.valid) {
                    localStorage.setItem("userToken", result.message);
                    this.userService.isLogged.next(true);
                    if (this.userService.isInRole(result.message, "admin"))
                        this.userService.isAdmin.next(true);
                    if (this.userService.isInRole(result.message, "user"))
                        this.userService.isUser.next(true);
                    this.router.navigateByUrl("/");
                } else {
                    let mess: string = "El nombre de usuario y/o la contraseña no son correctos.";
                    console.log(mess);
                    this.displayError(mess, "Whoops");
                }
            },
            error => {
                console.error(error);
                this.displayError(error.toString(), "Error!");
            });
    }

    displayError(content: string, title: string) {
        console.log(content);
        const modalReference = this.modalService.open(ErrorModalComponent);
        modalReference.componentInstance.errorMessage = content;
        modalReference.componentInstance.title = title;
    }
}
