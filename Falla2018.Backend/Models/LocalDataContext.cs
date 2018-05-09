namespace Falla2018.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Falla2018.Domain.User> Users { get; set; }
    }
}