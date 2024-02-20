using WpfApp1.MVVM.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.MVVM.Model;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.IO;
using WpfApp1.MVVM.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

        }
    }


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=DESKTOP-NFDA02C;Database=Messages;User Id=postgres;Password=1111;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactModel>().HasKey(u => u.UserId);
            modelBuilder.Entity<MessageModel>().HasKey(u => u.MessageId);
            modelBuilder.Entity<StickerModel>().HasKey(u => u.StickerId);
            //modelBuilder.Entity<MessageModel>()
            //    .HasOne<ContactModel>()
            //    .WithMany()
            //    .HasForeignKey(m => m.SenderId)
            //    .IsRequired();

            //modelBuilder.Entity<MessageModel>()
            //    .HasOne<ContactModel>()
            //    .WithMany()
            //    .HasForeignKey(m => m.ReceiverId)
            //    .IsRequired();

        }
        public DbSet<ContactModel> dbContacts { get; set; }
        public DbSet<MessageModel> dbMessages { get; set; }
        public DbSet<StickerModel> dbStickers { get; set; }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Server=DESKTOP-NFDA02C;Database=Messages;User Id=postgres;Password=1111;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }


}
