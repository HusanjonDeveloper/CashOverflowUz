//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class LockedJobException : Xeption
	{
		public LockedJobException(Exception innerException)
			: base(message: "Job is locked, please try again.", innerException)
		{ }
	}
}
