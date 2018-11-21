namespace Test_1708091406.Data
{
    using System.Data.Entity;
    using Test_1708091406.Models;

    public class ModelContext : DbContext
    {
        // Your context has been configured to use a 'ModelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Test_1708091406.Data.ModelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelContext' 
        // connection string in the application configuration file.
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<SuperParent> SuperParents { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Child_1> Childrent_1 { get; set; }
        public DbSet<Child_2> Childrent_2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperParent>()
                .Map<Parent>(m => m.Requires("Discriminator").HasValue("Parent"))
                .Map<Child_1>(m => m.Requires("Discriminator").HasValue("Child_2"))
                .Map<Child_2>(m => m.Requires("Discriminator").HasValue("Child_3"));

            base.OnModelCreating(modelBuilder);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}