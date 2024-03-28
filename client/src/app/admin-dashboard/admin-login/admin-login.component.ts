import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../_services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent implements OnInit {
  loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)])
  });

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn() && !this.authService.isAdmin()) {
      this.router.navigateByUrl('dashboard')
    } else if (this.authService.isAdmin()) {
      this.router.navigateByUrl('admin/overview')
    }
  }

  login() {
    if (this.loginForm.get("password")?.value?.toString()?.length < 5) {
      return;
    }
    this.authService.adminLogin(this.loginForm.value).subscribe(res => {
      if (res) {
        return this.router.navigateByUrl("/admin/overview")
      }
      return;
    })
  }

}
