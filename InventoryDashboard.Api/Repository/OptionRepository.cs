using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryDashboard.Api.Repository
{
    public class OptionRepository : IOptionInterface
    { 
        private readonly DataContext _context;
        public OptionRepository(DataContext context)
        {
            _context = context;
        }



        public ICollection<Option> GetOptions()
        {
            return _context.Options.OrderBy(p => p.OptionId).ToList();
        }
        public Option GetOption(int id)
        {
            return _context.Options.Where(p => p.OptionId == id).FirstOrDefault();
        }

        public bool OptionExists(int id)
        {
            return _context.Options.Any(p => p.OptionId == id);
        }
    }
}
