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
        public bool CreateDiscount(Discount discount)
        {
            // Notes to self
            //Change Tracker
            //Tracker states: add, updating, modifying
            //connected - disconnected
            //Entity.State.Added 
            //
            _context.Add(discount);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
