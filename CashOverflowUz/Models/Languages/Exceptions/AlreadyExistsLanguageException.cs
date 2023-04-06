//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class AlreadyExistsLanguageException : Xeption
	{
		public AlreadyExistsLanguageException(Exception innerException)
			: base(message: "Already exists exception", innerException)
		{ }
	}
}
