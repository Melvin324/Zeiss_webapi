using System;
using Microsoft.EntityFrameworkCore;
using Zeiss_webapi.Models;

namespace Zeiss_webapi.Providers {

    public class MyDbContext : DbContext {
        public DbSet<MsgEntity> Msg { get; set; }

        protected string Path { get; set; }

        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"DataSource=app.db;Cache=Shared");
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
