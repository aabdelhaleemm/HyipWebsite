import {Component, OnInit} from '@angular/core';
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {TransferService} from "../../_services/transfer.service";

@Component({
  selector: 'app-send-new-transfer',
  templateUrl: './send-new-transfer.component.html',
  styleUrls: ['./send-new-transfer.component.css']
})
export class SendNewTransferComponent implements OnInit {
  userBalance = 0;
  sendAmount?: number;
  recipientUserName?: string;
  code?: string;
  isSent = false;

  constructor(private userService: UserService, private toast: ToastrService, private transferService: TransferService) {
  }

  ngOnInit(): void {
    this.userService.getUserBalance().subscribe(res => {
      this.userBalance = res;
    })
  }

  isValid() {
    return this.sendAmount === undefined || this.sendAmount! < 1 || this.recipientUserName === undefined || this.recipientUserName.length! < 1

  }

  requestCode() {
    this.isSent = false;
    this.transferService.requestCode(this.recipientUserName!).subscribe(res => {
      if (res) {
        this.toast.success("Email has been sent")
      } else {
        this.toast.error("Something went wrong!, Please try again")
      }
    })
  }

  sendMoney() {
    this.transferService.newTransfer({
      "Code": this.code,
      "RecipientUserName": this.recipientUserName,
      "Amount": this.sendAmount
    }).subscribe(res => {
      if (res) {
        this.toast.success("Transfer done successfully")
        this.userBalance -= this.sendAmount!;
        this.sendAmount = undefined;
        this.recipientUserName = undefined;
        this.code = undefined;
        this.isSent = true;
      }
    })
  }
}
