﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Polyanka_1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PolyankaEntities : DbContext
    {
        public PolyankaEntities()
            : base("name=PolyankaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Order_> Order_ { get; set; }
        public virtual DbSet<Order_point> Order_point { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Products_in_storehouse> Products_in_storehouse { get; set; }
        public virtual DbSet<Storehouse> Storehouse { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<images> images { get; set; }
    }
}
