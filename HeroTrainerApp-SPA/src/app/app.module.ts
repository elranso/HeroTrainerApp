import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { TrainerComponent } from './trainer/Trainer.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

import { AlertifyService } from './services/alertify.service';
import { TrainingService } from './services/Training.service';
import { AuthService } from './services/auth.service';

import { appRoutes } from './routes';
import { AuthGuard } from './guards/auth.guard';
import { TrainerResolver } from './resolvers/trainer.resolver';
@NgModule({
  declarations: [
    AppComponent,
    TrainerComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule,
    RouterModule.forRoot(appRoutes),
   
  ],
  providers: [
    TrainingService,
    AuthService,
    AlertifyService,
    AuthGuard,
    TrainerResolver,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
