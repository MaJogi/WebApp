using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Request;
using WebApp.Infra.Common;

namespace WebApp.Infra
{
    public class RequestDbContext : BaseSupportAppDbContext<RequestDbContext>
    {
        public RequestDbContext() { }
        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options) { }
        public DbSet<RequestData> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }

        public static void InitializeTables(ModelBuilder builder)
        {
            createRequestTable(builder);
        }

        public static void createRequestTable(ModelBuilder builder)
        {
            const string table = "Request";
            createPrimaryKey<RequestData>(builder, table, a => new {a.Id});
            // Foreign key's can be added here
        }
    }
}
