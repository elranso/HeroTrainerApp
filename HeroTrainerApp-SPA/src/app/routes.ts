import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { TrainerComponent } from './trainer/Trainer.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'trainers', canActivate: [AuthGuard], component: TrainerComponent },

  { path: '**', redirectTo: '', pathMatch: 'full' },
];
