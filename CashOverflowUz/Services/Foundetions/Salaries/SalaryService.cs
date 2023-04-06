//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflow.Brokers.Storages;
using CashOverflowUz.Brokers.DateTimes;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Models.Salaries;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflowUz.Services.Foundetions.Salaries
{
	public partial class SalaryService : ISalaryService
	{
		private readonly IStorageBroker storageBroker;
		private readonly ILoggingBroker loggingBroker;
		private readonly IDateTimeBroker dateTimeBroker;

		public SalaryService(
			IStorageBroker storageBroker,
			ILoggingBroker loggingBroker,
			IDateTimeBroker dateTimeBroker)

		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
			this.dateTimeBroker = dateTimeBroker;
		}

		public ValueTask<Salary> AddSalaryAsync(Salary salary) =>
		TryCatch(async () =>
		{
			ValidateSalaryOnAdd(salary);

			return await storageBroker.InsertSalaryAsync(salary);
		});

		public IQueryable<Salary> RetrieveAllSalaries() =>
			TryCatch(() => storageBroker.SelectAllSalaries());
	}
}
