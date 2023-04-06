// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CashOverflow Team
// --------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using CashOverflowUz.Models.Languages;

namespace CashOverflow.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Language> InsertLanguageAsync(Language language);
		IQueryable<Language> SelectAllLanguages();
		ValueTask<Language> SelectLanguageByIdAsync(Guid languageId);
		ValueTask<Language> UpdateLanguageAsync(Language language);
		ValueTask<Language> DeleteLanguageAsync(Language language);
	}
}