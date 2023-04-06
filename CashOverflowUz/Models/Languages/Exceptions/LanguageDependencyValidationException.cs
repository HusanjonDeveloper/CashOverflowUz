//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class LanguageDependencyValidationException : Xeption
	{
		public LanguageDependencyValidationException(Xeption innerException)
			: base(message: "Language dependency validation exception occurred, fix the errors and try again.",
				  innerException)
		{ }
	}
}
}
