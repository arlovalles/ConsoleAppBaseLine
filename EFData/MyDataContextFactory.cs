using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFData
{
    internal class MyDataContextFactory : IDesignTimeDbContextFactory<MyDataContext>
    {
            public MyDataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyDataContext>();
                optionsBuilder.UseSqlServer("Data Source=ARLODELLOPTI705;Initial Catalog=ArloEnterprise;Integrated Security=True;TrustServerCertificate=true;");

                return new MyDataContext(optionsBuilder.Options);
            }
    }
}
