import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {ButtonModule} from "primeng/button";
import {PasswordModule} from "primeng/password";
import {SliderModule} from "primeng/slider";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {RouterModule} from "@angular/router";
import {HomeComponent} from './home/home.component';
import {NotFoundComponent} from './not-found/not-found.component';
import {AppRoutingModule} from "./app-routing.module";
import {LoginComponent} from './login/login.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ErrorInterceptor} from "./_interceptros/error.interceptor";
import {NgxSpinnerModule} from "ngx-spinner";
import {LoadingInterceptor} from "./_interceptros/loading.interceptor";
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { UserOverviewComponent } from './user-dashboard/user-overview/user-overview.component';
import { UserDepositComponent } from './user-dashboard/user-deposit/user-deposit.component';
import { UserWithdrawComponent } from './user-dashboard/user-withdraw/user-withdraw.component';
import {JwtInterceptor} from "./_interceptros/jwt.interceptor";
import { UserInvestmentComponent } from './user-dashboard/user-investment/user-investment.component';
import { UserTransactionComponent } from './user-dashboard/user-transaction/user-transaction.component';
import { UserProfileComponent } from './user-dashboard/user-profile/user-profile.component';
import { UserDepositHistoryComponent } from './user-dashboard/user-deposit-history/user-deposit-history.component';
import { UserWithdrawHistoryComponent } from './user-dashboard/user-withdraw-history/user-withdraw-history.component';

import {NgxChartsModule} from "@swimlane/ngx-charts";
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminOverviewComponent } from './admin-dashboard/admin-overview/admin-overview.component';
import { AdminLoginComponent } from './admin-dashboard/admin-login/admin-login.component';
import { AdminInvestmentComponent } from './admin-dashboard/admin-investment/admin-investment.component';
import { AdminDepositComponent } from './admin-dashboard/admin-deposit/admin-deposit.component';
import { AdminWithdrawComponent } from './admin-dashboard/admin-withdraw/admin-withdraw.component';
import { AdminTransactions } from './admin-dashboard/admin-transactions/admin-transactions.component';
import { AdminUsersComponent } from './admin-dashboard/admin-users/admin-users.component';
import { FooterComponent } from './footer/footer.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ChangePasswordComponent } from './user-dashboard/change-password/change-password.component';
import { ReferencesComponent } from './user-dashboard/references/references.component';
import { RegisterComponent } from './register/register.component';
import {ClipboardModule} from "ngx-clipboard";
import { SentTransfersComponent } from './user-dashboard/sent-transfers/sent-transfers.component';
import { ReceivedTransfersComponent } from './user-dashboard/recevied-transfers/received-transfers.component';
import { SendNewTransferComponent } from './user-dashboard/send-new-transfer/send-new-transfer.component';
import { UserPlansComponent } from './user-dashboard/user-plans/user-plans.component';
import { AdminAccountsComponent } from './admin-dashboard/admin-accounts/admin-accounts.component';
import { AdminPlansComponent } from './admin-dashboard/admin-plans/admin-plans.component';
import { AdminChangePasswordComponent } from './admin-dashboard/admin-change-password/admin-change-password.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { UserDetailsComponent } from './admin-dashboard/user-details/user-details.component';
import { ActivationComponent } from './admin-dashboard/activation/activation.component';


@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    NotFoundComponent,
    LoginComponent,
    UserDashboardComponent,
    UserOverviewComponent,
    UserDepositComponent,
    UserWithdrawComponent,
    UserInvestmentComponent,
    UserTransactionComponent,
    UserProfileComponent,
    UserDepositHistoryComponent,
    UserWithdrawHistoryComponent,
    AdminDashboardComponent,
    AdminOverviewComponent,
    AdminLoginComponent,
    AdminInvestmentComponent,
    AdminDepositComponent,
    AdminWithdrawComponent,
    AdminTransactions,
    AdminUsersComponent,
    FooterComponent,
    ForgetPasswordComponent,
    ResetPasswordComponent,
    ChangePasswordComponent,
    ReferencesComponent,
    RegisterComponent,
    SentTransfersComponent,
    ReceivedTransfersComponent,
    SendNewTransferComponent,
    UserPlansComponent,
    AdminAccountsComponent,
    AdminPlansComponent,
    AdminChangePasswordComponent,
    ContactUsComponent,
    UserDetailsComponent,
    ActivationComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    PasswordModule,
    SliderModule,
    FormsModule,
    ToastrModule.forRoot({positionClass: 'toast-bottom-right'}),
    RouterModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    NgxChartsModule,
    ClipboardModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
