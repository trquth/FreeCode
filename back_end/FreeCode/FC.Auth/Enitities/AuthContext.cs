using System.Data.Entity;

namespace FC.Auth.Enitities
{
    public class AuthContext : DbContext
    {
        public AuthContext() : base("AuthContext")
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}