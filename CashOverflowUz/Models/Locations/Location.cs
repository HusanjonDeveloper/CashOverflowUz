//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;

namespace CashOverflowUz.Models.Locations
{
    public class Location
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }



    }
}
