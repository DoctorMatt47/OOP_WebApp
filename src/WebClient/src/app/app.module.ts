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

const appRoutes: Routes = [
  {path: '', redirectTo: "tests", pathMatch: "full"},
  {path: 'test/:id', component: TestComponent},
  {path: 'login', component: LoginComponent},
  {path: 'tests', component: TestsComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'creator', component: CreatorComponent},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  declarations: [
    AppComponent, NotFoundComponent, LoginComponent,
    TestComponent, TestsComponent, CreatorComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule, RouterModule.forRoot(appRoutes), FormsModule, HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
