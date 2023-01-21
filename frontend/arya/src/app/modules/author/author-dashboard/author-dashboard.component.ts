import { Component } from '@angular/core';
import { AuthorService } from 'src/app/shared/author/author.service';
import { Author } from 'src/app/shared/models/author/author';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.css']
})
export class AuthorDashboardComponent {
  title = "Author";
  authors: Author[] = [];
  authorToEdit?: Author;

  constructor(private authorService: AuthorService) { }

  ngOnInit(): void {
    this.LoadAuthors();
  }

  LoadAuthors() {
    this.authorService.getAuthors().subscribe((res) => (this.authors = res.data.items));
  }
  updateAuthorList(authors: Author[]) {
    this.authors = authors
  }
  initNewAuthor(): void {
    this.authorToEdit = new Author();
  }

  editAuthor(author: Author) {
    this.authorToEdit = author
  }

  deleteAuthor(id: string) {
    this.authorService.removeAuthor(id).subscribe((res) => (this.LoadAuthors()))
  }
}