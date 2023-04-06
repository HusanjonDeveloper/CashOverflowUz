//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;

namespace CashOverflowUz.Models.Reviews
{
	public class Review
	{
		public Guid Id { get; set; }
		public string CompanyName { get; set; }
		public int Stars { get; set; }
		public string Thoughts { get; set; }
	}
}
