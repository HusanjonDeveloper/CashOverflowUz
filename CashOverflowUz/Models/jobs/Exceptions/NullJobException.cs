//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class NullJobException : Xeption
	{
		public NullJobException()
			: base(message: "Job is null.")
		{ }
	}
}
