import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {RouterModule, Routes} from "@angular/router";
import {NotFoundComponent} from "../not-found/not-found.component";
import {LoginComponent} from "../login/login.component";
import {TestComponent} from "../test/test.component";
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {TestsComponent} from "../tests/tests.component";
import {CreatorComponent} from "../creator/creator.component";
import {ProfileComponent} from "../profile/profile.component";
import {TestAnswersComponent} from "../test-answers/test-answers.component";
import {TestAnswerComponent} from "../test-answer/test-answer.component";
import {TokenService} from "../services/tokens/token.service";

const appRoutes: Routes = [
  {path: '', redirectTo: "tests", pathMatch: "full"},
  {path: 'test/:id', component: TestComponent},
  {path: 'login', component: LoginComponent},
  {path: 'tests', component: TestsComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'test-answers/:id', component: TestAnswersComponent},
  {path: 'test-answer/:testId/:username', component: TestAnswerComponent},
  {path: 'creator', component: CreatorComponent},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  declarations: [
    AppComponent, NotFoundComponent, LoginComponent,
    TestComponent, TestsComponent, CreatorComponent,
    ProfileComponent, TestAnswersComponent, TestAnswerComponent
  ],
  imports: [
    BrowserModule, RouterModule.forRoot(appRoutes), FormsModule, HttpClientModule
  ],
  providers: [TokenService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
