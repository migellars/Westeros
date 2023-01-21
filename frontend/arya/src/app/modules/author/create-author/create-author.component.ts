import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorService } from 'src/app/shared/author/author.service';

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.css']
})
export class CreateAuthorComponent implements OnInit {
  authorForm: FormGroup = new FormGroup({})

  constructor(private fb: FormBuilder, private authorService: AuthorService, private router: Router) { }

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
      zipCode: new FormControl(null)
    });
  }
  submitForm() {
    const data = this.authorForm.value
    console.log(data)
    this.authorService.createAuthor(data).subscribe((res) => this.router.navigateByUrl("/author/dashboard"))
  }
}
