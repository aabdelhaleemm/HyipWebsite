import {Component, OnInit} from '@angular/core';
import {MoneyTransferCardModel} from "../../_models/moneyTransferCardModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DepositService} from "../../_services/deposit.service";
import {ToastrService} from "ngx-toastr";
import {PaymentMethodService} from "../../_services/payment-method.service";

@Component({
  selector: 'app-user-deposit',
  templateUrl: './user-deposit.component.html',
  styleUrls: ['./user-deposit.component.css']
})
export class UserDepositComponent implements OnInit {
  depositCards?: MoneyTransferCardModel[];
  depositForm = new FormGroup({
    file: new FormControl('', []),
    fileSource: new FormControl('', [Validators.required]),
    depositAmount: new FormControl('', [Validators.required]),
    operationsId: new FormControl('', [Validators.required]),
    walletId: new FormControl('', [Validators.required]),
  });
  currentCard = '';
  cardCommission = 0;
  amountWithCommission?: number;
  sendToWalletId?: string;
  isOther = false;

  constructor(private depositService: DepositService, private toast: ToastrService, private paymentService: PaymentMethodService) {
  }

  ngOnInit(): void {
    this.getDepositMethods()
  }

  getDepositMethods() {
    this.paymentService.getDepositMethods().subscribe(res => {
      if (res) {
        this.depositCards = res;
      }
    }, error => error)
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.depositForm.patchValue({
        fileSource: file
      });
    }
  }

  requestDeposit() {
    const formData = new FormData();
    formData.append('File', this.depositForm.get('fileSource')?.value);
    formData.append('amount', this.depositForm.get('depositAmount')?.value.toString());
    formData.append('UserWalletId', this.depositForm.get('walletId')?.value);
    formData.append('DepositMethod', this.currentCard);
    formData.append('OperationId', this.depositForm.get('operationsId')?.value);
    this.depositService.requestDeposit(formData).subscribe(res => {
      if (res) {
        this.depositForm.reset();
        this.amountWithCommission = undefined;
        this.toast.success("Deposit request has been submitted")
      }
    })
  }

  depositClickHandler(method: string, commission: number, walletId: string) {
    this.depositForm.get('depositAmount')?.setValue('')
    this.isOther = false;
    this.currentCard = method;
    this.cardCommission = commission;
    this.amountWithCommission = undefined;
    this.sendToWalletId = walletId;
    if (walletId === null) {
      this.sendToWalletId = "Please contact us on telegram"
      this.isOther = true;
    }
  }

  copyToClipboard() {
    this.toast.success("Copied")
    // @ts-ignore
    navigator.clipboard.writeText(this.sendToWalletId).then().catch(e => console.error(e));


  }

  calculateCommission() {
    if (this.depositForm.get('depositAmount')?.value !== undefined) {
      if (this.cardCommission === 20) {
        this.amountWithCommission = +(this.depositForm.get('depositAmount')?.value) + 20
      } else {
        this.amountWithCommission = +(this.depositForm.get('depositAmount')?.value * (this.cardCommission / 100)) + +this.depositForm.get('depositAmount')?.value
      }
    }

  }

}
