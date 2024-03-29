// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
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
		public async Task ShouldRetrieveLanguageByIdAsync()
		{
			//given
			Guid randomLanguageId = Guid.NewGuid();
			Guid inputLanguageId = randomLanguageId;
			Language randomLanguage = CreateRandomLanguage();
			Language persistedLanguage = randomLanguage;
			Language expectedLanguage = persistedLanguage.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.SelectLanguageByIdAsync(inputLanguageId))
					.ReturnsAsync(persistedLanguage);

			//when
			Language actualLanguage = await this.languageService
				.RetrieveLanguageByIdAsync(inputLanguageId);

			//then
			actualLanguage.Should().BeEquivalentTo(expectedLanguage);

			this.storageBrokerMock.Verify(broker =>
				broker.SelectLanguageByIdAsync(inputLanguageId), Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.dateTimeBrokerMock.VerifyNoOtherCalls();
		}
	}
}
