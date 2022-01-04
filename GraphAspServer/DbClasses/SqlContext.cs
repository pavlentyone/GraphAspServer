using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DbClasses
{
    public class SqlContext : DbContext
    {
        public static string Protocol = "TCP";
        public static string Host = "10.3.0.54";
        public static string Port = "1521";
        public static string Server = "10.3.0.54";
        public static string ServiceName = "bivc.brest.rw";
        public static string UserId = "PR";
        public static string Password = "pr2013";

        // Типы операций
        public  DbSet<Operation> OperationTypes { get; set; }

        // Узлы железной дороги
        public  DbSet<Railway> RailwayNodes { get; set; }

        // Перевозчики
        public  DbSet<Carrier> Carriers { get; set; }

        // Вагоны
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public  DbSet<Wagon> Wagons { get; set; }

        // Поезда
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public  DbSet<Train> Trains { get; set; }

        public  DbSet<Locomotive> Locomotives { get; set; }

        public  DbSet<User> Users { get; set; }

        public  DbSet<Shift> Shifts { get; set; }

        public SqlContext()
        {
            Database.SetCommandTimeout(5);

            ChangeTracker.LazyLoadingEnabled = false;// optionsBuilder.UseLazyLoadingEnabled = false;
        }
        public SqlContext(string Protocol, string Host, string Port, string Server, string ServiceName, string UserId, string Password)
        {
            SqlContext.Protocol = Protocol;
            SqlContext.Host = Host;
            SqlContext.Port = Port;
            SqlContext.Server = Server;
            SqlContext.ServiceName = ServiceName;
            SqlContext.UserId = UserId;
            SqlContext.Password = Password;

            Database.SetCommandTimeout(5);

            ChangeTracker.LazyLoadingEnabled = false;

        }

        public static async Task<SqlContext> GetContextAsync(string Protocol, string Host, string Port, string Server, string ServiceName, string UserId, string Password)
        {
            return await Task.Run(() =>
            {
                return new SqlContext(Protocol, Host, Port, Server, ServiceName, UserId, Password);
            });     
        }

        public static async Task<SqlContext> GetContextAsync()
        {
            return await Task.Run(() =>
            {
                return new SqlContext();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = String.Format(
                "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = {0})(HOST = {1})(PORT = {2})) (CONNECT_DATA = (SERVER = {3})(SERVICE_NAME = {4}))); User Id= {5};Password={6};",
                Protocol, Host, Port, Server, ServiceName, UserId, Password);
            optionsBuilder.UseOracle(connStr);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Operation>();
            builder.Entity<Railway>();
            builder.Entity<Carrier>();
            builder.Entity<Wagon>();
            builder.Entity<Train>();
            builder.Entity<Locomotive>();
            builder.Entity<User>();
            builder.Entity<Shift>().HasNoKey().HasIndex(s=>s.Hid).IsUnique();
        }
    }
}
