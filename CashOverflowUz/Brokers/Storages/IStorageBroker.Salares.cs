using System.Threading.Tasks;
using CashOverflowUz.Models.Salaries;

namespace CashOverflowUz.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Salary> InsertSalaryAsync(Salary salary);
	}
}
