namespace Domain.Common
{
    public interface IInvestmentsProfitService
    {
        void ScheduleProfitEarning(int investmentId, int nextProfitEarningDurationInDays);

    }
}