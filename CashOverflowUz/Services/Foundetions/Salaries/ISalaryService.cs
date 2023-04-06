//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.Salaries;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflowUz.Services.Foundetions.Salaries
{
	public interface ISalaryService
	{
		ValueTask<Salary> AddSalaryAsync(Salary salary);
		IQueryable<Salary> RetrieveAllSalaries();
	}
}
