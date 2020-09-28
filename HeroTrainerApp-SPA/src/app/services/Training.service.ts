import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Hero } from "../models/hero";
import { Trainer } from "../models/trainer";

@Injectable({
  providedIn: "root",
})
export class TrainingService {
  baseUrl = "http://localhost:5000/api/";

  constructor(private http: HttpClient) {}

  getTrainer(id: number): Observable<Trainer> {
    return this.http.get<Trainer>(this.baseUrl + "trainers/" + id);
  }
  getHeroes(): Observable<Hero> {
    return this.http.get<Hero>(this.baseUrl + "heroes/");
  }

  updateHero(id: number, hero: Hero) {
    return this.http.put(this.baseUrl + "heroes/" + id, hero);
  }
}
