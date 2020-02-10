using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [DbConfigurationType(typeof(DataConfiguration))]
    public class MultitTenantContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }

     public class DataConfiguration :DbConfiguration
    {
        public DataConfiguration()
        {
           SetDatabaseInitializer(new MultitTenantContextIntitializer());
        }

    }

    internal class MultitTenantContextIntitializer :  CreateDatabaseIfNotExists<MultitTenantContext>
    {
        protected override void Seed(MultitTenantContext context)
        {
                 var tenants = new List<Tenant>
                {
                    new Tenant
                    {
                        Name = "SVCC",
                        DomainName = "www.siliconvalley-codecamp.com",
                        Id = 1,
                        Default = true
                    },
                    new Tenant()
                    {
                        Name = "ANGU",
                        DomainName = "angularu.com",
                        Id = 3,
                        Default = false
                    },
                    new Tenant()
                    {
                        Name = "CSSC",
                        DomainName = "codestarssummit.com",
                        Id = 2,
                        Default = false
                    }

                };

                context.Tenants.AddRange(tenants);
                context.SaveChanges();
            
        }
    }
}