import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  passwordForm = new FormGroup({
    currentPassword: new FormControl('', [Validators.required, Validators.minLength(5)]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(5)])
  })

  constructor(private userService: UserService, private toast: ToastrService) {
  }

  ngOnInit(): void {
  }

  isSameLength() {
    return this.passwordForm.get('password')?.value.length !== this.passwordForm.get('confirmPassword')?.value.length
  }

  checkPass() {
    return this.passwordForm.get('confirmPassword')?.touched &&
      this.passwordForm.get('confirmPassword')?.value !== this.passwordForm.get('password')?.value! &&
      this.passwordForm.get('confirmPassword')?.value.length > 0 && this.passwordForm.get('confirmPassword')?.dirty && this.passwordForm.get('password')?.dirty
  }

  changePassword() {
    this.userService.changePassword({
      'CurrentPassword': this.passwordForm.get('currentPassword')?.value,
      'NewPassword': this.passwordForm.get('password')?.value
    }).subscribe(res => {
      if (res) {
        this.toast.success("Password changed successfully")
        this.passwordForm.reset();
      } else {
        this.toast.error("Something went wrong! please try again")
      }
    })
  }

}
