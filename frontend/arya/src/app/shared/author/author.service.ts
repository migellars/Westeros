import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Author } from '../models/author/author';


@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  private url = "author"
  constructor(private http: HttpClient) { }

  public getAuthors(): Observable<any> {
    return this.http.get<any>(`${Environment.apiUrl}/${this.url}/all-author-profile`)
  }

  public getAuthorsById(id: string): Observable<any> {
    return this.http.get<any>(`${Environment.apiUrl}/${this.url}/author-profile?id=${id}`)
  }

  public editAuthor(id: string, author: Author): Observable<any> {
    return this.http.put<any>(`${Environment.apiUrl}/${this.url}/edit-author-profile?id=${id}`, author)
  }

  public createAuthor(author: Author): Observable<Author[]> {
    return this.http.post<Author[]>(`${Environment.apiUrl}/${this.url}/create-author-profile`, author)
  }

  public removeAuthor(id: string): Observable<any> {
    return this.http.delete<any>(`${Environment.apiUrl}/${this.url}/delete-author-profile?id=${id}`)
  }
}
