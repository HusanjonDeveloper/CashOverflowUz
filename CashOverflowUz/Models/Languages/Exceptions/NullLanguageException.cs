//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class NullLanguageException : Xeption
	{
		public NullLanguageException()
			: base(message: "Language is null")
		{ }
	}
}
