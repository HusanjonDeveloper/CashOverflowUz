//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class InvalidLanguageException : Xeption
	{
		public InvalidLanguageException()
			: base(message: "Language is invalid.")
		{ }
	}
}
