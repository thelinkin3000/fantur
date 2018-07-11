import { Component, OnInit } from '@angular/core';
import { UserRegisterVm } from "../../models/UserRegisterVm";
import { UserService } from "../../services/user.service";
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})

export class RegisterUserComponent implements OnInit {

    public user: UserRegisterVm;
  constructor(private userService:UserService, private modalService:NgbModal) { }


  ngOnInit() {
      this.user = new UserRegisterVm();
  }

  save() {
      this.userService.register(this.user).subscribe(result => {
              this.displayError(
                  "Excelente! Te mandamos un mail al correo que ingresaste. Por favor, revisalo para poder confirmar tu mail.",
                  "Yahoo!");
          },
          error => {
              console.error(error);
          });
  }

    displayError(content: string, title: string) {
        console.log(content);
        const modalReference = this.modalService.open(ErrorModalComponent);
        modalReference.componentInstance.errorMessage = content;
        modalReference.componentInstance.title = title;
    }
}
