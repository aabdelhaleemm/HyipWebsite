import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {OverviewModel} from "../../_models/overviewModel";
import {TransactionStatusParser} from "../../_services/_static/TransactionStatusParser";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {UserService} from "../../_services/user.service";

@Component({
  selector: 'app-user-overview',
  templateUrl: './user-overview.component.html',
  styleUrls: ['./user-overview.component.css']
})
export class UserOverviewComponent implements OnInit {
  overview?: OverviewModel;

  constructor(private userService: UserService, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.userService.getOverview().subscribe(res => {
      if (res === null) {
        this.toast.error("Something wrong! Please contact admin")
        return
      }
      this.overview = res;
    })
  }

  getStatusBgClass(status: string): string {
    return StatusBgService.getStatusBgClass(status)
  }

  getNewStatusName(status: string): string {
    return TransactionStatusParser.getBetterName(status);
  }


}
