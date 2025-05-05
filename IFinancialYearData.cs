using RPT.Models;

namespace RPT.Services
{
public interface IFinancialYearDataService
{
    FinancialYearData GetFinancialYearDataById(int profileId);
    bool CreateFinancialYearData(FinancialYearData financialData);
    bool UpdateFinancialYearData(FinancialYearData financialData);
    bool DeleteFinancialYearData(int id);
}
}
