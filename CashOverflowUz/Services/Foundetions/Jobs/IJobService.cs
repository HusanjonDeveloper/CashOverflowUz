//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System.Threading.Tasks;
using CashOverflowUz.Models.job;

namespace CashOverflowUz.Services.Foundetions.Jobs
{
    public interface IJobService
    {
        ValueTask<Job>AddJobAsync(Job job);
    }
}
