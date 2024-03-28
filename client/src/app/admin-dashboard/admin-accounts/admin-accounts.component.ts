import {Component, OnInit} from '@angular/core';
import {AdminService} from "../../_services/admin.service";
import {ToastrService} from "ngx-toastr";
import {AdminModel} from "../../_models/adminModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-admin-accounts',
  templateUrl: './admin-accounts.component.html',
  styleUrls: ['./admin-accounts.component.css']
})
export class AdminAccountsComponent implements OnInit {
  adminList?: AdminModel[];
  adminInfoForm = new FormGroup({
    id: new FormControl({value: '', disabled: true}, []),
    username: new FormControl({value: '', disabled: true}, []),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.minLength(5)]),
    confirmPassword: new FormControl('', [Validators.minLength(5)])
  })
  registerForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(5)])
  });
  isDone = false;
  deleteAdminInfo?: AdminModel;
  isDeleted = false;

  constructor(private adminService: AdminService, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.getAdminList()
  }

  isSameLength() {
    return this.registerForm.get('password')?.value.length !== this.registerForm.get('confirmPassword')?.value.length
  }

  registerCheckPass() {
    return this.registerForm.get('confirmPassword')?.touched &&
      this.registerForm.get('confirmPassword')?.value !== this.registerForm.get('password')?.value! &&
      this.registerForm.get('confirmPassword')?.value.length > 0 && this.registerForm.get('confirmPassword')?.dirty && this.registerForm.get('password')?.dirty
  }

  getAdminList() {
    this.adminService.getAll().subscribe(res => {
      if (res) {
        this.adminList = res;
      }
    })
  }

  checkPass() {
    return this.adminInfoForm.get('confirmPassword')?.touched &&
      this.adminInfoForm.get('confirmPassword')?.value !== this.adminInfoForm.get('password')?.value! &&
      this.adminInfoForm.get('confirmPassword')?.value.length > 0 && this.adminInfoForm.get('confirmPassword')?.dirty && this.adminInfoForm.get('password')?.dirty
  }

  isPasswordChanges() {
    return this.adminInfoForm.get('password')?.value?.length > 0 && this.adminInfoForm.get('confirmPassword')?.value?.length === 0;
  }

  setAdmin(admin: AdminModel) {
    this.adminInfoForm.reset();
    this.adminInfoForm.get('id')?.setValue(admin.id)
    this.adminInfoForm.get('username')?.setValue(admin.userName)
    this.adminInfoForm.get('email')?.setValue(admin.email)

  }

  updateAdmin() {
    let a = {
      'id': this.adminInfoForm.get('id')?.value,
      'email': this.adminInfoForm.get('email')?.value,
      'password': this.adminInfoForm.get('password')?.value
    }
    this.adminService.updateAdmin(a).subscribe(res => {
      if (res) {
        this.getAdminList();
        this.toast.success("Admin account Updated Successfully")
        this.isDone = true;
      }
    })
  }

  addAdmin() {
    this.adminService.addAdmin({
      'UserName': this.registerForm.get('userName')?.value,
      'Password': this.registerForm.get('password')?.value,
      'Email': this.registerForm.get('email')?.value
    }).subscribe(res => {
      if (res) {
        this.toast.success("Account Created Successfully")
        this.getAdminList()
        this.registerForm.reset();
      }
    })
  }

  deleteAdmin() {
    this.adminService.deleteAdmin(this.deleteAdminInfo?.id!).subscribe(res => {
      if (res) {
        this.toast.success("Account has been deleted")
        this.deleteAdminInfo = undefined;
        this.getAdminList();
        this.isDeleted = true;
      } else {
        this.toast.error("Something went wrong! Please try again")
      }

    })
  }
}
