import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { TokenModel } from './token-model';
import { UserProfile } from './user-profile';
import { LoginModel } from './login-model';
import { Environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = "auth"

  constructor(private httpClient: HttpClient) { }
  userProfile = new BehaviorSubject<UserProfile | null>(null);
  jwtService: JwtHelperService = new JwtHelperService();

  public userLogin(payload: LoginModel) {
    return this.httpClient.post<any>(`${Environment.apiUrl}/${this.url}/login-user`, payload)
      .pipe(
        map((data) => {
          var token = data as TokenModel;
          localStorage.setItem('token', JSON.stringify(token.data));
          var userInfo = this.jwtService.decodeToken(token.data.token) as UserProfile;
          this.userProfile.next(userInfo);

          return true;
        }),
        catchError((error) => {
          console.log(error);
          return of(false);
        })
      );
  }
}
