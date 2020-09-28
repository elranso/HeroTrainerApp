import { Component, Input, OnInit } from '@angular/core';

import { Hero } from '../models/hero';
import { Trainer } from '../models/trainer';
import { AlertifyService } from '../services/alertify.service';

import { TrainingService } from '../Services/Training.service';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-Trainer',
  templateUrl: './Trainer.component.html',
  styleUrls: ['./Trainer.component.css'],
})
export class TrainerComponent implements OnInit {
  trainer: Trainer;
  heroes: Hero[];
  count = 0;
  ok = true;

  @Input()
  currentPower: number;

  constructor(
    private trainingService: TrainingService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.trainer = JSON.parse(localStorage.getItem('trainer'));
    this.heroes = this.trainer.heroes;
    this.getTrainer(this.trainer.id);
  }
  getTrainer(id: number) {
    this.trainingService.getTrainer(id).subscribe(
      (data: Trainer) => {
        this.trainer = data;
        this.heroes = this.trainer.heroes;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  trainHero(heroGuid: string) {
    if (this.count > 5) {
      this.alertify.error('maximum 5 training per day');
    } else {
      this.count += 1;
      for (let i = 0; i < this.heroes.length; i++) {
        if (heroGuid === this.heroes[i].guidId) {
          this.currentPower = this.heroes[i].currentPower;
          if (this.currentPower >= 100) {
            this.alertify.error('your hero is at maximum power already!');
            break;
          } else {
            this.currentPower += Math.random() * (10 - 1) + 1;
            this.heroes[i].currentPower = this.currentPower;
          }
        }
      }

      console.log(this.currentPower);
    }
  }
}
