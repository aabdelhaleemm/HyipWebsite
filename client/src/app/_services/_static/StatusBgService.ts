export class StatusBgService {
  public static getStatusBgClass(status: string): string {
    switch (status) {
      case 'Accepted':
        return "bg-success"
      case 'Pending':
        return "bg-gradient"
      case 'Rejected':
        return "bg-danger"
    }
    return ""
  }

  public static getInvestmentBgStatus(status: string) : string {
    switch (status) {
      case "Running":
        return "bg-info";
      case "Canceled":
        return "bg-danger"
      case "Finished":
        return "bg-success"
    }
    return ""
  }

}
