//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class LockedLanguageException : Xeption
	{
		public LockedLanguageException(Exception innerException)
			: base(message: "Language is locked, please try again.", innerException)
		{ }
	}
}
