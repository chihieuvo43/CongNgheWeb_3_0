﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongNgheWeb_3_0.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class E_ClassEntities : DbContext
    {
        public E_ClassEntities()
            : base("name=E_ClassEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_Category> tbl_Category { get; set; }
        public DbSet<tbl_Lession> tbl_Lession { get; set; }
        public DbSet<tbl_Position> tbl_Position { get; set; }
        public DbSet<tbl_User> tbl_User { get; set; }
        public DbSet<tbl_Course> tbl_Course { get; set; }
    }
}