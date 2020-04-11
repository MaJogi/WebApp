using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Appeal;
using WebApp.Infra.Common;

namespace WebApp.Infra
{
    public class SupportAppDbContext : BaseSupportAppDbContext<SupportAppDbContext>
    {
        public SupportAppDbContext() { }
        public SupportAppDbContext(DbContextOptions<SupportAppDbContext> options) : base(options) { }
        public DbSet<AppealData> Appeals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }

        public static void InitializeTables(ModelBuilder builder)
        {
            createAppealTable(builder);
        }

        public static void createAppealTable(ModelBuilder builder)
        {
            const string table = "Appeal";
            createPrimaryKey<AppealData>(builder, table, a => new {a.Id});
            // Foreign key's can be added here
        }
    }
}
