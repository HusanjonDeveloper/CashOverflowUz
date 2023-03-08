//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Threading.Tasks;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.job;

namespace CashOverflowUz.Services.Foundetions.Jobs
{
    public class JobService:IJobService
    {
        private IStorageBroker storageBroker;

        public JobService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;

        public async ValueTask<Job> AddJobAsync(Job job)=>
            await this.storageBroker.InsertJobAsync(job);
    
    }
}
