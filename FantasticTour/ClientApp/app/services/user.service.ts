import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { UserRegisterVm } from "../models/UserRegisterVm"

@Injectable()
export class UserService {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
        })
    };

    url : string = '/api/User/Register';

  constructor(private httpClient:HttpClient) { }

  register(uservm: UserRegisterVm) : Observable<Object> {
      return this.httpClient.post(this.url, uservm, this.httpOptions);
  }
}
