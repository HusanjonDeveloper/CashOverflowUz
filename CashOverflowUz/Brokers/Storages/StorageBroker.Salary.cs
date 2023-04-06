
//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Linq;
using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Salaries;
using Microsoft.EntityFrameworkCore;

namespace CashOverflowUz.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Salary> Salaries { get; set; }

		public async ValueTask<Salary> InsertSalaryAsync(Salary salary) =>
			await InsertAsync(salary);

		public IQueryable<Salary> SelectAllSalaries() => SelectAll<Salary>();

		public async ValueTask<Salary> SelectSalaryByIdAsync(Guid salaryId) =>
			 await SelectAsync<Salary>(salaryId);
	}
}
