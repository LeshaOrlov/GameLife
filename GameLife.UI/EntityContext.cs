using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    class EntityContext: DbContext
    {
        public EntityContext() : base("DefaultConnection")
        { }

        public DbSet<Log> Logs { get; set; }
        public DbSet<GameSave> GameSaves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
