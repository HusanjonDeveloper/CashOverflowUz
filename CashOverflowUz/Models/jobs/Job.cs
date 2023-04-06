//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;

namespace CashOverflowUz.Models.job
{
	public class Job
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public Level Level { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
