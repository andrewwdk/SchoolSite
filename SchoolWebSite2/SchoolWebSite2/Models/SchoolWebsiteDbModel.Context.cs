﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolWebSite2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SchoolWebsiteDatabaseEntities : DbContext
    {
        public SchoolWebsiteDatabaseEntities()
            : base("name=SchoolWebsiteDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bell> Bell { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
