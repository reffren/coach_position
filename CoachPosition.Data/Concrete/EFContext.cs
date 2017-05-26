using CoachPosition.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoachPosition.Data.Concrete
{
    public class EFContext : IdentityDbContext
    {
        public EFContext() : base("EFContext") { }
        public DbSet<Train> Trains { get; set; }
    }
}