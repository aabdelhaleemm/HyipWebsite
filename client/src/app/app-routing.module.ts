import {NgModule} from '@angular/core';
import {ExtraOptions, RouterModule, Routes} from "@angular/router";
import {HomeComponent} from "./home/home.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {LoginComponent} from "./login/login.component";
import {UserDashboardComponent} from "./user-dashboard/user-dashboard.component";
import {UserOverviewComponent} from "./user-dashboard/user-overview/user-overview.component";
import {UserWithdrawComponent} from "./user-dashboard/user-withdraw/user-withdraw.component";
import {UserDepositComponent} from "./user-dashboard/user-deposit/user-deposit.component";
import {UserTransactionComponent} from "./user-dashboard/user-transaction/user-transaction.component";
import {UserInvestmentComponent} from "./user-dashboard/user-investment/user-investment.component";
import {UserDepositHistoryComponent} from "./user-dashboard/user-deposit-history/user-deposit-history.component";
import {UserWithdrawHistoryComponent} from "./user-dashboard/user-withdraw-history/user-withdraw-history.component";
import {AdminDashboardComponent} from "./admin-dashboard/admin-dashboard.component";
import {AdminOverviewComponent} from "./admin-dashboard/admin-overview/admin-overview.component";
import {AdminLoginComponent} from "./admin-dashboard/admin-login/admin-login.component";
import {UserAuthGuard} from "./_guards/user-auth.service";
import {AdminAuthGuard} from "./_guards/admin-auth.guard";
import {AdminWithdrawComponent} from "./admin-dashboard/admin-withdraw/admin-withdraw.component";
import {AdminDepositComponent} from "./admin-dashboard/admin-deposit/admin-deposit.component";
import {AdminInvestmentComponent} from "./admin-dashboard/admin-investment/admin-investment.component";
import {AdminTransactions} from "./admin-dashboard/admin-transactions/admin-transactions.component";
import {AdminUsersComponent} from "./admin-dashboard/admin-users/admin-users.component";
import {ForgetPasswordComponent} from "./forget-password/forget-password.component";
import {ResetPasswordComponent} from "./reset-password/reset-password.component";
import {ChangePasswordComponent} from "./user-dashboard/change-password/change-password.component";
import {ReferencesComponent} from "./user-dashboard/references/references.component";
import {RegisterComponent} from "./register/register.component";
import {SentTransfersComponent} from "./user-dashboard/sent-transfers/sent-transfers.component";
import {ReceivedTransfersComponent} from "./user-dashboard/recevied-transfers/received-transfers.component";
import {SendNewTransferComponent} from "./user-dashboard/send-new-transfer/send-new-transfer.component";
import {UserPlansComponent} from "./user-dashboard/user-plans/user-plans.component";
import {AdminAccountsComponent} from "./admin-dashboard/admin-accounts/admin-accounts.component";
import {AdminPlansComponent} from "./admin-dashboard/admin-plans/admin-plans.component";
import {AdminChangePasswordComponent} from "./admin-dashboard/admin-change-password/admin-change-password.component";
import {ContactUsComponent} from "./contact-us/contact-us.component";
import {UserDetailsComponent} from "./admin-dashboard/user-details/user-details.component";
import {ActivationComponent} from "./admin-dashboard/activation/activation.component";


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'forget-password', component: ForgetPasswordComponent},
  {path: 'reset', component: ResetPasswordComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'contact', component: ContactUsComponent},
  {path: 'dashboard', redirectTo: 'dashboard/overview', pathMatch: 'full'},
  {
    path: 'dashboard', component: UserDashboardComponent, canActivate: [UserAuthGuard], children: [
      {path: 'overview', component: UserOverviewComponent},
      {path: 'withdraw', component: UserWithdrawComponent},
      {path: 'deposit', component: UserDepositComponent},
      {path: 'deposit/history', component: UserDepositHistoryComponent},
      {path: 'withdraw/history', component: UserWithdrawHistoryComponent},
      {path: 'transaction', component: UserTransactionComponent},
      {path: 'investment', component: UserInvestmentComponent},
      {path: 'investment/plans', component: UserPlansComponent},
      {path: 'change-password', component: ChangePasswordComponent},
      {path: 'referral', component: ReferencesComponent},
      {path: 'sent-transfers', component: SentTransfersComponent},
      {path: 'received-transfers', component: ReceivedTransfersComponent},
      {path: 'send-transfer', component: SendNewTransferComponent},
    ]
  },
  {path: 'admin/login', component: AdminLoginComponent},
  {
    path: 'admin', component: AdminDashboardComponent, canActivate: [AdminAuthGuard], children: [
      {path: 'overview', component: AdminOverviewComponent},
      {path: 'withdraws', component: AdminWithdrawComponent},
      {path: 'deposits', component: AdminDepositComponent},
      {path: 'investments', component: AdminInvestmentComponent},
      {path: 'transactions', component: AdminTransactions},
      {path: 'users', component: AdminUsersComponent},
      {path: 'user-details', component: UserDetailsComponent},
      {path: 'plans', component: AdminPlansComponent},
      {path: 'activation', component: ActivationComponent},
      {path: 'change-password', component: AdminChangePasswordComponent},
      {path: 'admin-accounts', component: AdminAccountsComponent},
    ]
  },
  {path: 'not-found', component: NotFoundComponent},
  {path: '**', component: NotFoundComponent},
];
const routerOptions: ExtraOptions = {
  useHash: false,
  anchorScrolling: 'enabled',
  scrollPositionRestoration: 'top'
};

@NgModule({
  imports: [RouterModule.forRoot(routes, routerOptions)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
