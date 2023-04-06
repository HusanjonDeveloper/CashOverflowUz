//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class InvalidJobException : Xeption
	{
		public InvalidJobException()
			: base(message: "Job is invalid.")
		{ }
	}
}
