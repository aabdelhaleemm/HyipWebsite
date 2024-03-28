import {Component, OnInit} from '@angular/core';
import {UserReferencesModel} from "../../_models/UserReferencesModel";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-references',
  templateUrl: './references.component.html',
  styleUrls: ['./references.component.css']
})
export class ReferencesComponent implements OnInit {
  userReferences?: UserReferencesModel;
  referenceLink = '';

  constructor(private userService: UserService,private toast:ToastrService) {
  }

  ngOnInit(): void {
    this.userService.getReferences().subscribe(res => {
      if (res) {
        this.userReferences = res;
        this.referenceLink = "www.mindstrade.com/register?reference=" + res.userName;
      }
    })
  }
  copyToClipboard(){
    this.toast.success("Copied")
  }


}
