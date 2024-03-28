import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(5)])
  });
  reference?: string | null;

  constructor(private route: ActivatedRoute, private userService: UserService, private toast: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.reference = res.get('reference');
    })
  }

  isSameLength() {
    return this.registerForm.get('password')?.value.length !== this.registerForm.get('confirmPassword')?.value.length
  }

  checkPass() {
    return this.registerForm.get('confirmPassword')?.touched &&
      this.registerForm.get('confirmPassword')?.value !== this.registerForm.get('password')?.value! &&
      this.registerForm.get('confirmPassword')?.value.length > 0 && this.registerForm.get('confirmPassword')?.dirty && this.registerForm.get('password')?.dirty
  }

  register() {
    this.userService.register({
      'UserName': this.registerForm.get('userName')?.value,
      'Password': this.registerForm.get('password')?.value,
      'ReferenceUserName': this.reference,
      'Email': this.registerForm.get('email')?.value
    }).subscribe(res => {
      if (res) {
        this.toast.success("Account created successfully")
        this.router.navigateByUrl('/login');
      }
    })


  }


}
