//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class LanguageDependencyException : Xeption
	{
		public LanguageDependencyException(Xeption innerException)
			: base(message: "Language dependency exception occurred, contact support", innerException)
		{ }
	}
}
