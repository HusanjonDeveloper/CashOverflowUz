// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CashOverflow.Brokers.Storages;
using CashOverflowUz.Brokers.DateTimes;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Models.Salaries;
using CashOverflowUz.Services.Foundetions.Salaries;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Salaries
{
	public partial class SalaryServiceTests
	{
		private readonly Mock<IStorageBroker> storageBrokerMock;
		private readonly Mock<ILoggingBroker> loggingBrokerMock;
		private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
		private readonly ISalaryService salaryService;

		public SalaryServiceTests()
		{
			this.storageBrokerMock = new Mock<IStorageBroker>();
			this.loggingBrokerMock = new Mock<ILoggingBroker>();
			this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

			this.salaryService = new SalaryService(
				storageBroker: this.storageBrokerMock.Object,
				loggingBroker: this.loggingBrokerMock.Object,
				dateTimeBroker: this.dateTimeBrokerMock.Object);
		}

		public static TheoryData<int> InvalidMinutes()
		{
			int minutesInFuture = GetRandomNumber();
			int minutesInPast = GetRandomNegativeNumber();

			return new TheoryData<int>
			{
				minutesInFuture,
				minutesInPast
			};
		}

		private SqlException CreateSqlException() =>
		   (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

		private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
			 actualException => actualException.SameExceptionAs(expectedException);

		private DateTimeOffset GetRandomDateTimeOffset() =>
			new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

		private Salary CreateRandomSalary(DateTimeOffset dates) =>
			CreateSalaryFiller(dates).Create();

		private Salary CreateRandomSalary() =>
			CreateSalaryFiller(dates: GetRandomDateTimeOffset()).Create();

		private IQueryable<Salary> CreateRandomSalaries() =>
			CreateSalaryFiller(dates: GetRandomDateTimeOffset())
				.Create(count: GetRandomNumber()).AsQueryable();

		private static int GetRandomNegativeNumber() =>
			-1 * new IntRange(min: 2, max: 9).GetValue();

		private static int GetRandomNumber() =>
			new IntRange(min: 2, max: 9).GetValue();

		private static string GetRandomString() =>
			new MnemonicString().GetValue();

		private Filler<Salary> CreateSalaryFiller(DateTimeOffset dates)
		{
			var filler = new Filler<Salary>();

			filler.Setup()
				.OnType<DateTimeOffset>().Use(dates);

			return filler;
		}
	}
}
