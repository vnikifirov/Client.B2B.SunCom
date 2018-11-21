using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1708091406.Data
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuperParent",
                s => new
                {
                    SuperClass_ID = s.Int(nullable: false, identity: true),
                    SuperParentText = s.String(),

                    Child_1Tex = s.String(),
                    ParentText = s.String(),
                    Child_2Text = s.String(),

                    Discriminator = s.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.SuperClass_ID);

        }

        public override void Down()
        {
            DropTable("dbo.SuperParent");
        }
    }
}
