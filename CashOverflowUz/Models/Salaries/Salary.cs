//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;

namespace CashOverflowUz.Models.Salaries
{
    public class Salary
    {
        public Guid id { get; set; }
        public decimal Amount { get; set; }
        public int Experience { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid LocationId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid JobId { get; set; }
    }
}
