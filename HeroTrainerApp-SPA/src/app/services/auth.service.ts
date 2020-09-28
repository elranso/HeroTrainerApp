import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Trainer } from '../models/trainer';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentTrainer: Trainer;
  constructor(private http: HttpClient) {}
  register(trainer: Trainer) {
    return this.http.post(this.baseUrl + 'register', trainer);
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const trainer = response;
        if (trainer) {
          localStorage.setItem('token', trainer.token);
          localStorage.setItem('trainer', JSON.stringify(trainer.trainer));
          this.decodedToken = this.jwtHelper.decodeToken(trainer.token);
          this.currentTrainer = trainer.trainer;
        }
      })
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
