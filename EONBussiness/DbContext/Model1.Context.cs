﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EONBussiness.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EONBusinessEntities : DbContext
    {
        public EONBusinessEntities()
            : base("name=EONBusinessEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbBanner> tbBanners { get; set; }
        public virtual DbSet<tbdoitac> tbdoitacs { get; set; }
        public virtual DbSet<tbduan> tbduans { get; set; }
        public virtual DbSet<tbGioiThieu> tbGioiThieux { get; set; }
        public virtual DbSet<tbkhachhang> tbkhachhangs { get; set; }
        public virtual DbSet<tbLienHe> tbLienHes { get; set; }
        public virtual DbSet<tbsanpham> tbsanphams { get; set; }
        public virtual DbSet<tbSetup> tbSetups { get; set; }
        public virtual DbSet<tbtuyendung> tbtuyendungs { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<tbdanhmuctt> tbdanhmuctts { get; set; }
        public virtual DbSet<tbdanhmucsp> tbdanhmucsps { get; set; }
        public virtual DbSet<tbtintuc> tbtintucs { get; set; }
    }
}
