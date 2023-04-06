//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class AlreadyExistsJobException : Xeption
	{
		public AlreadyExistsJobException(Exception innerException)
			: base(message: "Job already exists.", innerException)
		{ }
	}
}
