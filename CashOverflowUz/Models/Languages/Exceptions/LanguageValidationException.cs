//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class LanguageValidationException : Xeption
	{
		public LanguageValidationException(Exception innerException)
			: base(message: "Language validation error occured, fix the errors and try again", innerException)
		{ }
	}
}
