import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { AuthorService } from 'src/app/shared/author/author.service';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.css']
})
export class EditAuthorComponent implements OnInit {
  authorForm: FormGroup = new FormGroup({})

  constructor(private fb: FormBuilder, private authorService: AuthorService, private router: Router, private route: ActivatedRoute) {
    this.route.paramMap.subscribe((params: ParamMap) => {
      const authorId = params.get("id");
      if (authorId) this.getAuthorById(authorId);
    });

  }

  ngOnInit(): void {
    this.initForm()
  }

  initForm() {
    this.authorForm = this.fb.group({
      firstName: new FormControl(null, [Validators.required]),
      lastName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required]),
      phone: new FormControl(null, [Validators.required]),
      city: new FormControl(null, [Validators.required]),
      street: new FormControl(null, [Validators.required]),
      country: new FormControl(null, [Validators.required]),
      state: new FormControl(null, [Validators.required]),
      localGovt: new FormControl(null),
      zipCode: new FormControl(null),
      id: new FormControl(null)
    });
  }
  submitForm() {
    const data = this.authorForm.value
    this.authorService.editAuthor(data.id, data).subscribe((res) => this.router.navigateByUrl("/author/dashboard"))
  }

  getAuthorById(id: string) {
    this.authorService.getAuthorsById(id).subscribe((res) => {

      const author = res?.data
      Object.keys(author?.address).forEach(key => {
        author[key] = author?.address[key]
      });
      this.authorForm.patchValue(author);
    })
  }
}
