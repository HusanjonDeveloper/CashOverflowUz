//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using CashOverflowUz.Models.Languages;
using Microsoft.EntityFrameworkCore;

namespace CashOverflowUz.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Language> Languages { get; set; }  
    }
}
