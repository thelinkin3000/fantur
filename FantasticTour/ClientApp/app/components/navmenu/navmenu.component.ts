import { Component, OnInit, Inject } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Observable, BehaviorSubject } from 'rxjs';


@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent implements OnInit{
    username: string;
    logged: Observable<boolean>;

    jwtHelper: JwtHelperService = new JwtHelperService();
    isAdmin: Observable<boolean>;
    isUser: Observable<boolean>;

    constructor(private router: Router, private userService:UserService) {}

    ngOnInit() {
        this.isAdmin = this.userService.isAdmin.asObservable();
        this.isUser = this.userService.isUser.asObservable();
        let token: string = localStorage.getItem("userToken");
        this.logged = this.userService.isLogged.asObservable();
        if (token) {
            if (!this.jwtHelper.isTokenExpired(token)) {
                this.username = this.jwtHelper.decodeToken(token).sub;
                this.userService.isLogged.next(true);
                if (this.userService.isInRole(token, "admin"))
                    this.userService.isAdmin.next(true);
                if (this.userService.isInRole(token, "user"))
                    this.userService.isUser.next(true);
            } else {
                localStorage.removeItem("userToken");
                this.userService.isLogged.next(false);
                this.userService.isAdmin.next(false);
                this.userService.isUser.next(false);
            }
        }
    }

    logout() {
        localStorage.removeItem("userToken");
        this.userService.isLogged.next(false);
        this.userService.isAdmin.next(false);
        this.userService.isUser.next(false);
        this.router.navigateByUrl("/");
    }



}
