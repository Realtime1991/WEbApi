using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service1.Models
{
    public class PlaneGeoLocationContext : DbContext
    {
        public PlaneGeoLocationContext(DbContextOptions<PlaneGeoLocationContext> options)
            : base(options)
        {

        }

        public DbSet<PlaneGeoLocation> PlaneGeoLocationItems { get; set; }

    }
}
