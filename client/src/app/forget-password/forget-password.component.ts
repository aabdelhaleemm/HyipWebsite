import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../_services/user.service";

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {
  resetForm = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required])
  })
  isDone?: boolean = false;

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
  }

  requestResetPassword() {
    this.userService.requestResetPassword(this.resetForm.get('email')?.value).subscribe(() => {
      this.isDone = true;
    })
  }

}
