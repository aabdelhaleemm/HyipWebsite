
import {WithdrawModel} from "../withdrawModel";

export class WithdrawsPaginatedList {
  items?: WithdrawModel[];
  pageIndex: number = 1;
  totalPages: number = 1;
  totalCount: number = 1;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;
}
