using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace Application.Chart
{
    public class GetChartQuery : IRequest<Chart>
    {
    }

    public class GetChartQueryHandler : IRequestHandler<GetChartQuery, Chart>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDbConnection _dbConnection;

        public GetChartQueryHandler(IApplicationDbContext applicationDbContext, IDbConnection dbConnection)
        {
            _applicationDbContext = applicationDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<Chart> Handle(GetChartQuery request, CancellationToken cancellationToken)
        {
            var investmentChart = await _applicationDbContext.Investments.AsNoTracking()
                .Where(x => x.StartDate.Year == DateTime.UtcNow.Year)
                .GroupBy(x => new
                {
                    x.Created.Month
                })
                .Select(x => new InvestmentChart()
                    {
                        Name = x.Key.Month.ToString(),
                        Value = x.Sum(k => k.Amount)
                    }
                )
                .ToListAsync(cancellationToken);

            await using var connection = new SqlConnection(_dbConnection.ConnectionString);
            var report = await connection.QuerySingleAsync<DbReport>(
                @"SELECT SUM(TotalDeposit) as TotalDeposit, SUM(TotalWithdraw) as TotalWithdraw, 
                        SUM(TotalInvest) as TotalInvestment,SUM(TotalProfit) as TotalProfit ,COUNT(Id) AS TotalUsers
                        FROM Wallets");
            var pieChart1 = await connection
                .QueryAsync<InvestmentChart>(
                    @"select [Plan] as Name, SUM(Amount) as Value                                        
                                                from Investments                                                                                          
                                                where Status = 'running'                                                                                          
                                                group by [Plan]");
            var pieChart2 = await connection
                .QueryAsync<InvestmentChart>(
                    @"select [Plan] as Name, count(id) as Value                                        
                                                from Investments                                                                                          
                                                where Status ='running'                                                                                         
                                                group by [Plan]");
            return new Chart()
            {
                TotalInvestmentsAmount = pieChart1,
                TotalInvestmentsCount = pieChart2,
                InvestmentChart = investmentChart,
                TotalInvestment = report.TotalInvestment,
                TotalDeposits = report.TotalDeposit,
                TotalWithdraw = report.TotalWithdraw,
                TotalUsers = report.TotalUsers,
                TotalProfit = report.TotalProfit
            };
        }
    }
}