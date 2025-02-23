using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinanceManagmentApp.Infrastructure
{
    public class FinanceManagmentAppContextFactory : IDesignTimeDbContextFactory<FinanceManagmentAppContext>
    {
        public FinanceManagmentAppContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<FinanceManagmentAppContextFactory>().Build();

            var options = new DbContextOptionsBuilder<FinanceManagmentAppContext>()
                .UseSqlServer(config["DbConnectionString"])
                .Options;

            return new FinanceManagmentAppContext(options);
        }
    }
}
