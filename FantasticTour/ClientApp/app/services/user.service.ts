import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserRegisterVm } from "../models/UserRegisterVm";
import { RequestResultVm } from "../models/RequestResultVm";
import { EmailConfirmVm } from "../models/EmailConfirmVm";
import { LoginVm } from '../models/LoginVm';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class UserService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };
    private jwtService: JwtHelperService = new JwtHelperService();
    url : string = '/api/Users/Register';
    loginUrl: string = '/api/Users/Login';
    confirmUrl: string = '/api/Users/ConfirmMail';
    isLogged: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    isUser: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor(private httpClient: HttpClient) { }


    register(uservm: UserRegisterVm): Observable<Object> {
      return this.httpClient.post(this.url, uservm, this.httpOptions);
    }

    login(loginVm: LoginVm): Observable<RequestResultVm> {
        return this.httpClient.post<RequestResultVm>(this.loginUrl, loginVm, this.httpOptions);
    }
    
    isInRole(token: string, role: string): boolean {
        debugger;
        
        let decodedToken = this.jwtService.decodeToken(token);
        console.log(JSON.stringify(decodedToken));
        if (!decodedToken)
            return false;
        if (!decodedToken.roles)
            return false;
        let roles: string[] = decodedToken.roles;
        console.log(JSON.stringify(roles));
        if (roles.length === 0)
            return false;
        console.log(roles.indexOf(role));
        if (roles.indexOf(role) <0)
            return false;
        console.log("IS IN ROLE!");
        return true;
    }

    confirmEmail(userId: string, token: string): Observable<RequestResultVm>{
        let confirmVm: EmailConfirmVm = new EmailConfirmVm();
        confirmVm.token = token;
        confirmVm.userId = userId;
        return this.httpClient.post<RequestResultVm>(this.confirmUrl, confirmVm, this.httpOptions);
    }

}
