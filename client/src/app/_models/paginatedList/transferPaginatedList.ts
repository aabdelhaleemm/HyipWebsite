import {TransferModel} from "../transferModel";

export class TransferPaginatedList {
  items?: TransferModel[]
  pageIndex: number = 1;
  totalPages: number = 1;
  totalCount: number = 1;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;
}
