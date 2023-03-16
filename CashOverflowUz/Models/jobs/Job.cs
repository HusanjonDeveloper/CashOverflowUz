//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;

namespace CashOverflowUz.Models.job
{
    public class Job
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public Level level { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

    }
}
