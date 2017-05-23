using CoachPosition.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoachPosition.Data.Concrete
{
    public class EFContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }
    }
}