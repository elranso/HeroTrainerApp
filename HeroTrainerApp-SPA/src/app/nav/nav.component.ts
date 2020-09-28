import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';
import { Trainer } from '../models/trainer';
import { JsonPipe } from '@angular/common';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}
  model: any = {};
  trainer: Trainer;
  ngOnInit() {}
  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.alertify.success('Logged in successfully');
        this.trainer = JSON.parse(localStorage.getItem('trainer'));
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/trainers']);
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('trainer ');
    this.authService.decodedToken = null;
    this.authService.decodedToken = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }
}
