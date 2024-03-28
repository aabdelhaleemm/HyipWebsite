import {Component, OnInit} from '@angular/core';
import {AdminService} from "../../_services/admin.service";
import {StateModel} from "../../_models/stateModel";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-activation',
  templateUrl: './activation.component.html',
  styleUrls: ['./activation.component.css']
})
export class ActivationComponent implements OnInit {

  state?: StateModel;

  constructor(private adminService: AdminService, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.adminService.getState().subscribe(res => {
      if (res) {
        this.state = res;
      }
    })
  }

  save() {
    this.adminService.changeState(this.state?.isWithdrawActive, this.state?.isDepositActive).subscribe(res => {
      if (res) {
        this.toast.success("Done")
      } else {
        this.toast.error("Something went wrong");
      }

    })

  }

}
