import {UserModel} from "./UserModel";
import {TransactionModel} from "./transactionModel";

export class UserReferencesModel {
  userName?: string;
  referenceUsers?: UserModel[];
  transactions?: TransactionModel[]
}
