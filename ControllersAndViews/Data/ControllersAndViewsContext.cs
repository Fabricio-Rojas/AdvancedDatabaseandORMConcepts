using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControllersAndViews.Models;

namespace ControllersAndViews.Data
{
    public class ControllersAndViewsContext : DbContext
    {
        public ControllersAndViewsContext (DbContextOptions<ControllersAndViewsContext> options)
            : base(options)
        {
        }

        public DbSet<ControllersAndViews.Models.Course> Course { get; set; } = default!;
        public DbSet<ControllersAndViews.Models.Student> Students { get; set; } = default!;
    }
}
