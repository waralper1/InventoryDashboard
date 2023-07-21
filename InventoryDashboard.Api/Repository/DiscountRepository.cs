using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryDashboard.Api.Repository
{
    public class DiscountRepository : IDiscountInterface
    {
        private readonly DataContext _context;
        public DiscountRepository(DataContext context)
        {
            _context = context;
        }



        public ICollection<Discount> GetDiscounts()
        {
            return _context.Discounts.OrderBy(p => p.DiscountId).ToList();
        }
        public Discount GetDiscount(int id)
        {
            return _context.Discounts.Where(p => p.DiscountId == id).FirstOrDefault();
        }

        public bool DiscountExists(int id)
        {
            return _context.Discounts.Any(p => p.DiscountId == id);
        }
    }
}
