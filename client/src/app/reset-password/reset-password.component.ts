import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../_services/user.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  passwordForm = new FormGroup({
    password: new FormControl('', [Validators.required, Validators.minLength(4)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(4)])
  })
  token?: string | null = '';
  email?: string | null = '';
  isValid: boolean = false;
  isReset: boolean = false;

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      // @ts-ignore
      this.token = encodeURIComponent(res.get('token'));
      this.email = res.get('email');
      this.validateToken();
    })
  }

  validateToken() {
    if (this.token === null || this.email === null) {
      this.isValid = false;
      return;
    }
    // @ts-ignore
    this.userService.validateResetToken(this.token, this.email).subscribe(res => {
      if (res) {
        this.isValid = true;
      }
    })
  }

  isSameLength() {
    return this.passwordForm.get('password')?.value.length !== this.passwordForm.get('confirmPassword')?.value.length
  }

  checkPass() {
    return this.passwordForm.get('confirmPassword')?.touched &&
      this.passwordForm.get('confirmPassword')?.value !== this.passwordForm.get('password')?.value! &&
      this.passwordForm.get('confirmPassword')?.value.length > 0 && this.passwordForm.get('confirmPassword')?.dirty && this.passwordForm.get('password')?.dirty
  }

  resetPassword() {
    this.userService.resetPassword({
      'token': this.token,
      'password': this.passwordForm.get('password')?.value,
      'email': this.email
    }).subscribe(res => {
      if (res) {
        this.toast.success("Password has been changed successfully")
        this.toast.info("please login to continue")
        this.router.navigate(['/login']);
      } else {
        this.toast.error("Something went Wrong! please contact the admin")
      }
    })
  }


}
