import { Component, OnInit } from '@angular/core';
import {UserModel} from "../../_models/UserModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  userName: string | null = '';
  user?: UserModel;
  userInfoForm = new FormGroup({
    id: new FormControl({value: '', disabled: true}, []),
    username: new FormControl({value: '', disabled: true}, []),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.minLength(5)]),
    confirmPassword: new FormControl('', [Validators.minLength(5)])
  })


  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private toast: ToastrService) {
  }

  isPasswordChanges() {
    return this.userInfoForm.get('password')?.value.length > 0 && this.userInfoForm.get('confirmPassword')?.value.length === 0;
  }

  checkPass() {
    return this.userInfoForm.get('confirmPassword')?.touched &&
      this.userInfoForm.get('confirmPassword')?.value !== this.userInfoForm.get('password')?.value! &&
      this.userInfoForm.get('confirmPassword')?.value.length > 0 && this.userInfoForm.get('confirmPassword')?.dirty && this.userInfoForm.get('password')?.dirty
  }

  updateUser() {
    let a = {
      'id': this.userInfoForm.get('id')?.value,
      'email': this.userInfoForm.get('email')?.value,
      'password': this.userInfoForm.get('password')?.value
    }
    this.userService.updateUser(a).subscribe(res => {
      if (res) {
        this.toast.success("User Updated Successfully")
      }
    })
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.userName = res.get("username")
      this.getUser()
    })
  }

  search() {
    this.router.navigate([], {queryParams: {username: this.userName}})
  }

  getUser() {
    this.user = undefined
    if (this.userName === '' || this.userName === null) {
      return;
    }
    this.userService.getUserByUserName(this.userName).subscribe(res => {
      if (res) {
        this.userInfoForm.setValue({
          'id': res.id,
          'username': res.userName,
          'email': res.email,
          'password': '',
          'confirmPassword': ''
        })
        this.user = res;
      }
    })
  }

}
