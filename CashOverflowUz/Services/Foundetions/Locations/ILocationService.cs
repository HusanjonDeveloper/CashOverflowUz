using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public interface ILocationService
    {
        ValueTask<Location> AddLocationAsyncs(Location location);
    }
}
