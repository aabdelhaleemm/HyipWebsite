import {InvestmentModel} from "../InvestmentModel";

export class InvestmentPaginatedList {
  items?: InvestmentModel[]
  pageIndex: number = 1;
  totalPages: number = 1;
  totalCount: number = 1;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;
}
