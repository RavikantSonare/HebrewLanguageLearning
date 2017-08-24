﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HebrewLanguageLearning_Admin.DBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HLLDBContext : DbContext
    {
        public HLLDBContext()
            : base("name=HLLDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HLL_Grammer> HLL_Grammer { get; set; }
        public virtual DbSet<HLL_Topics> HLL_Topics { get; set; }
        public virtual DbSet<HLL_Vocabulary> HLL_Vocabulary { get; set; }
        public virtual DbSet<HLL_Application> HLL_Application { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<HLL_TagAttachment> HLL_TagAttachment { get; set; }
        public virtual DbSet<HLL_StudentsInfo> HLL_StudentsInfo { get; set; }
        public virtual DbSet<HLL_DictionaryEntries> HLL_DictionaryEntries { get; set; }
        public virtual DbSet<HLL_Sound> HLL_Sound { get; set; }
        public virtual DbSet<HLL_Definition> HLL_Definition { get; set; }
        public virtual DbSet<HLL_Pictures> HLL_Pictures { get; set; }
        public virtual DbSet<HLL_Example> HLL_Example { get; set; }
        public virtual DbSet<HLL_SemanticDomain> HLL_SemanticDomain { get; set; }
    
        public virtual ObjectResult<get_row_id> get_row_id(string t_name, ObjectParameter row_id)
        {
            var t_nameParameter = t_name != null ?
                new ObjectParameter("t_name", t_name) :
                new ObjectParameter("t_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<get_row_id>("get_row_id", t_nameParameter, row_id);
        }
    }
}
