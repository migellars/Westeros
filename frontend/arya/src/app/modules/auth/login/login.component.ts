import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/shared/auth/auth.service';
import { UserProfile } from 'src/app/shared/auth/user-profile';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  authForm: FormGroup = new FormGroup({})

  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) { }
  userProfile = new BehaviorSubject<UserProfile | null>(null);
  jwtService: JwtHelperService = new JwtHelperService();


  ngOnInit(): void {
    this.initForm()
  }

  initForm() {
    this.authForm = this.fb.group({
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
      confirmpassword: new FormControl(null, [Validators.required]),

    });
  }
  submitForm() {
    const data = this.authForm.value
    this.authService.userLogin(data).subscribe((res) => {
      if (res) {
        this.router.navigate(['/']);
      }
    })
  }
}