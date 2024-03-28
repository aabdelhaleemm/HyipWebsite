import {Component, OnInit} from '@angular/core';
import {MoneyTransferCardModel} from "../../_models/moneyTransferCardModel";
import {UserService} from "../../_services/user.service";
import {WithdrawService} from "../../_services/withdraw.service";
import {ToastrService} from "ngx-toastr";


@Component({
  selector: 'app-user-withdraw',
  templateUrl: './user-withdraw.component.html',
  styleUrls: ['./user-withdraw.component.css']
})
export class UserWithdrawComponent implements OnInit {
  withdrawCards: MoneyTransferCardModel[] = [
    {
      id: 1,
      name: "Perfect Money",
      walletId: "U35960081",
      minimum: 10,
      charge: 1,
      logoUrl: "https://res.cloudinary.com/dbhqoj86r/image/upload/v1634932548/pngwing.com_xnyhvg.png"
    },
    {
      id: 2,
      name: "USDT (ERC -20)",
      walletId: "0x93EC394634568D8d32493cF23142C8f7F98391ef",
      minimum: 10,
      charge: 20,
      logoUrl: "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634932790/tether-usdt-logo_bxl3dk.png"
    },
    {
      id: 3,
      name: "USDT (TRC -20)",
      walletId: "TSieWboSBuEvbm9WjZ5dXfZySXFxA3mjuX",
      minimum: 10,
      charge: 1,
      logoUrl: "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634932790/tether-usdt-logo_bxl3dk.png"
    },
    {
      id: 4,
      name: "Other Methods",
      walletId: undefined,
      minimum: 10,
      charge: 0,
      logoUrl: "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634933345/s_u2npdm.png"
    }
  ]

  currentCard = '';
  withdrawAmount?: number;
  userWithdrawAccount = '';
  userBalance?: number;
  isWithdrawActive = false;

  constructor(private userService: UserService, private withdrawService: WithdrawService, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.withdrawService.isWithdrawActive().subscribe(res => {
      // @ts-ignore
      this.isWithdrawActive = res;
    })
    this.userService.getUserBalance().subscribe(res => {
      this.userBalance = res;
    })
  }

  withdrawClickHandler(method: string) {
    this.withdrawAmount = undefined;
    this.userWithdrawAccount = '';
    this.currentCard = method;

  }

  requestWithdraw() {
    this.withdrawService.addWithdrawRequest({
      'Amount': this.withdrawAmount,
      'WithdrawAccount': this.userWithdrawAccount,
      'WithdrawMethod': this.currentCard
    }).subscribe(res => {
      if (res) {
        this.userBalance! -= this.withdrawAmount!;
        this.userWithdrawAccount = '';
        this.withdrawAmount = undefined;
        this.toast.success("Withdrawal request has been submitted")
      }
    })
  }

}
