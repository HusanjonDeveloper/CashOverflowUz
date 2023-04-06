//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using CashOverflowUz.Models.job;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CashOverflowUz.Services.Foundetions.Jobs
{
    public interface IJobService
    {
        ValueTask<Job> AddJobAsync(Job job);
        IQueryable<Job> RetrieveAllJobs();
        ValueTask<Job> RetrieveJobByIdAsync(Guid jobId);
        ValueTask<Job> ModifyJobAsync(Job job);
        ValueTask<Job> RemoveJobByIdAsync(Guid jobId);
    }
}
