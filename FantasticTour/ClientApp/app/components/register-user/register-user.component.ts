import { Component, OnInit } from '@angular/core';
import { UserRegisterVm } from "../../models/UserRegisterVm";
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})

export class RegisterUserComponent implements OnInit {

    public user: UserRegisterVm;
  constructor(private userService:UserService) { }


  ngOnInit() {
      this.user = new UserRegisterVm();
  }

  save() {
      this.userService.register(this.user).subscribe(result => {
              console.log(result);
          },
          error => {
              console.error(error);
          });
  }

}
