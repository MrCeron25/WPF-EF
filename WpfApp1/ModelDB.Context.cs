﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class course_work_WPF_EFEntities1 : DbContext
    {
        public course_work_WPF_EFEntities1()
            : base("name=course_work_WPF_EFEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<airplane> airplane { get; set; }
        public virtual DbSet<cities> cities { get; set; }
        public virtual DbSet<country> country { get; set; }
        public virtual DbSet<flights> flights { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<system> system { get; set; }
        public virtual DbSet<tickets> tickets { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}
