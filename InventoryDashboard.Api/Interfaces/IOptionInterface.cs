using InventoryDashboard.Api.Models;

namespace InventoryDashboard.Api.Interfaces
{
    public interface IOptionInterface
    {
        ICollection<Option> GetOptions();
        Option GetOption(int id);
        bool OptionExists(int id);
        bool CreateOption(Option option);
        bool UpdateOption(Option option);
        bool DeleteOption(Option option);
        bool Save();
    }
}
