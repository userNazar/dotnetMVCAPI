using RecipeDB.Models;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeDB.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
