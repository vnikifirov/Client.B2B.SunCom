namespace Test_1708150901.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Test_1708150901.Models;

    public class MyDbContext : DbContext
    {        
        public MyDbContext()
            : base("name=MyDataBase")
        {
        }

        public virtual DbSet<BaseClass> Models { get; set; }
    }    
}