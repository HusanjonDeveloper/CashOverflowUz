using System.Threading.Tasks;
using CashOverflowUz.Models.Languages;

namespace CashOverflowUz.Brokers.Storages
{
    public partial interface IStorageBroker
    {
       ValueTask<Language> InsertLanguageAsync(Language language);
    }
}
