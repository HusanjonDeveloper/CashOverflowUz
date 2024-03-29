// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Linq;
using CashOverflowUz.Models.Languages;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CashOverflowUz.Tests.unit.Servies.Faundetions.Languages
{
	public partial class LanguageServiceTests
	{
		[Fact]
		public void ShouldRetrieveAllLanguages()
		{
			// given
			IQueryable<Language> randomLanguages = CreateRandomLanguages();
			IQueryable<Language> storageLanguages = randomLanguages;
			IQueryable<Language> expectedLanguages = storageLanguages.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectAllLanguages())
					.Returns((Delegate)storageLanguages);

			// when
			IQueryable<Language> actualLanguages =
				this.languageService.RetrieveAllLanguages();

			// then
			actualLanguages.Should().BeEquivalentTo(expectedLanguages);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectAllLanguages(),
					Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
