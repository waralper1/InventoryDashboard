using InventoryDashboard.Api.Data;
using InventoryDashboard.Api.Interfaces;
using InventoryDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryDashboard.Api.Repository
{
    public class VariantRepository : IVariantInterface
    {
        private readonly DataContext _context;
        public VariantRepository(DataContext context)
        {
            _context = context;
        }

        

        public ICollection<Variant> GetVariants()
        {
            return _context.Variants.OrderBy(p => p.VariantId).ToList();
        }
        public Variant GetVariant(int id)
        {
            return _context.Variants.Where(p => p.VariantId == id).FirstOrDefault();
        }

        public bool VariantExists(int id)
        {
            return _context.Variants.Any(p => p.VariantId == id);
        }
        public bool CreateVariant(int productId, int optionId, Variant variant)
        {
            _context.Add(variant);
            return Save();
        }
        public bool UpdateVariant(int variantId, Variant variant)
        {
            _context.Update(variant);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteVariant(Variant variant)
        {
            _context.Remove(variant);
            return Save();
        }
    }
}

