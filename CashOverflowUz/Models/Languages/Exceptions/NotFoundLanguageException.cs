//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class NotFoundLanguageException : Xeption
	{
		public NotFoundLanguageException(Guid languageId)
			: base(message: $"Couldn't find language with id:{languageId}")
		{ }
	}
}
