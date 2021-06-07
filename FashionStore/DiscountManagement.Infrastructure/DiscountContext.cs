using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;


namespace DiscountManagement.Infrastructure.EFCore
{
   public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount>  CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }
    }
}
