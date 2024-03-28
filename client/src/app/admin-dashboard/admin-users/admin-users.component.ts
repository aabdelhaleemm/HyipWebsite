import {Component, OnInit} from '@angular/core';
import {UserService} from "../../_services/user.service";
import {UserPaginatedList} from "../../_models/paginatedList/userPaginatedList";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {
  usersList: UserPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, items: [], pageIndex: 1, totalCount: 0, totalPages: 0
  }
  page?: string;

  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.page = res.get("page")!
      this.getUsers();
    })

  }

  getUsers() {
    if (parseInt(this.page!) < 1) {
      this.page = '1';
    }
    console.log(this.page)
    this.userService.getAllUsers(this.page!).subscribe(res => {
      if (res) {
        this.usersList = res;
      }
    })
  }
}
