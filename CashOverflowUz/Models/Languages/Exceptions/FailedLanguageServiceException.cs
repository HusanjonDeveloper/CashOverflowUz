//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Languages.Exceptions
{
	public class FailedLanguageServiceException : Xeption
	{
		public FailedLanguageServiceException(Exception innerException)
			: base(message: "Failed language service error occured, please contact support", innerException)
		{ }
	}
}
