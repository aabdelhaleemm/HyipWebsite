import {TransactionModel} from "../transactionModel";

export class TransactionsPaginatedList {
  items?: TransactionModel[];
  pageIndex: number = 1;
  totalPages: number = 1;
  totalCount: number = 1;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;
}
