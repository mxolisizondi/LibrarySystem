﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySystem.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LibraryDBEntities : DbContext
    {
        public LibraryDBEntities()
            : base("name=LibraryDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookCategories> BookCategories { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BookTable> BookTable { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Designations> Designations { get; set; }
        public virtual DbSet<Fines> Fines { get; set; }
        public virtual DbSet<IssueBooks> IssueBooks { get; set; }
        public virtual DbSet<ReturnBooks> ReturnBooks { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<UserPrivileges> UserPrivileges { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}