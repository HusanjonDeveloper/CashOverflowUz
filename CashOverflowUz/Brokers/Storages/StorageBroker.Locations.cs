//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using Microsoft.EntityFrameworkCore;

namespace CashOverflowUz.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Location> Locations { get; set; }
        public async ValueTask<Location> InsertLocationAysnc(Location location) =>
             await InsertAsync(location);

    }
}
