using System.Configuration;
using System.Data.Entity;
using Glossory_MayBeFinal_.Models;

namespace Glossory_MayBeFinal_.Services;

public class StoreDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public StoreDbContext() : base(ConfigurationManager.ConnectionStrings["MyStoreDb"].ConnectionString)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
    public DbSet<Product> products { get; set; }


}