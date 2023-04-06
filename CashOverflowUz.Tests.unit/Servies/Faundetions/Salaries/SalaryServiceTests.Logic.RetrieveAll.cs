// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashOverflowUz.Models.Salaries;
using FluentAssertions;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Salaries
{
	public partial class SalaryServiceTests
	{
		[Fact]
		public void ShouldRetrieveAllSalaries()
		{
			//given
			IQueryable<Salary> randomSalaries = CreateRandomSalaries();
			IQueryable<Salary> storageSalaries = randomSalaries;
			IQueryable<Salary> expectedSalaries = storageSalaries;

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllSalaries()).Returns(expectedSalaries);

			//when
			IQueryable<Salary> actualSalaries = this.salaryService.RetrieveAllSalaries();

			//then
			actualSalaries.Should().BeEquivalentTo(expectedSalaries);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllSalaries(), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
