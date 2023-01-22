import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { TokenModel } from './token-model';
import { UserProfile } from './user-profile';
import { LoginModel } from './login-model';
import { Environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Login } from '../models/auth/login';
import { Register } from '../models/auth/register';
import { Author } from '../models/author/author';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = "auth"

  constructor(private http: HttpClient, private router: Router) { }
  userProfile = new BehaviorSubject<UserProfile | null>(null);
  jwtService: JwtHelperService = new JwtHelperService();
  public isUserAuthenticated: boolean = false;
  private loggedIn = new BehaviorSubject<boolean>(false); // {1}

  get isLoggedIn() {
    let currentUser = localStorage.getItem('token');
    if (!currentUser) { }
    this.loggedIn.next(true);
    return this.loggedIn.asObservable();
  }

  public userLogin(payload: LoginModel) {
    return this.http.post<any>(`${Environment.apiUrl}/${this.url}/login-user`, payload)
      .pipe(
        map((data) => {
          var token = data as TokenModel;
          localStorage.setItem('token', JSON.stringify(token.data));
          this.loggedIn.next(true);
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

  logout() {
    localStorage.removeItem("token")
    // this.loggedIn.next(false);
    this.router.navigate(['/']);
    this.reloadPage();
  }

  reloadPage(): void {
    window.location.reload();
  }

  public getUsers(): Observable<any> {
    return this.http.get<any>(`${Environment.apiUrl}/${this.url}/all-user-profile`)
  }

  public loginUser(login: Login): Observable<any> {
    return this.http.post<any>(`${Environment.apiUrl}/${this.url}/login-user`, login)
  }

  public getUserById(id: string): Observable<any> {
    return this.http.get<any>(`${Environment.apiUrl}/${this.url}/user?id=${id}`)
  }

  public editUser(id: string, author: Author): Observable<any> {
    return this.http.put<any>(`${Environment.apiUrl}/${this.url}/edit-user-profile?id=${id}`, author)
  }

  public createUser(author: Register): Observable<Register[]> {
    return this.http.post<Register[]>(`${Environment.apiUrl}/${this.url}/register-user`, author)
  }

  public removeAuthor(id: string): Observable<any> {
    return this.http.delete<any>(`${Environment.apiUrl}/${this.url}/delete-user?id=${id}`)
  }
}
