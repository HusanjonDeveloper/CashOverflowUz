//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class NotFoundJobException : Xeption
	{
		public NotFoundJobException(Guid jobId)
			: base(message: $"Couldn't find job with id: {jobId}.")
		{ }
	}
}
