//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class LanguageServiceException : Xeption
	{
		public LanguageServiceException(Xeption innerException)
			: base(message: "Language service error occurred, contact support.", innerException)
		{ }
	}
}
