import {Component, OnInit} from '@angular/core';
import {AuthService} from "../_services/auth.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  screenWidth = window.innerWidth;

  constructor(public authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
