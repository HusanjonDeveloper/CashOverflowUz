//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class FailedLanguageStorageException : Xeption
	{
		public FailedLanguageStorageException(Exception innerException)
			: base(message: "Failed language storage exception occurred, contact support", innerException)
		{ }
	}
}
