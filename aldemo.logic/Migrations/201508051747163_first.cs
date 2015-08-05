namespace aldemo.logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "aldemo.Lines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "aldemo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "aldemo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ProjectId = c.Int(nullable: false),
                        LineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("aldemo.Lines", t => t.LineId, cascadeDelete: true)
                .ForeignKey("aldemo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.LineId);
            
            CreateTable(
                "aldemo.ProjectLines",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        LineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.LineId })
                .ForeignKey("aldemo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("aldemo.Lines", t => t.LineId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.LineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("aldemo.Status", "ProjectId", "aldemo.Projects");
            DropForeignKey("aldemo.Status", "LineId", "aldemo.Lines");
            DropForeignKey("aldemo.ProjectLines", "LineId", "aldemo.Lines");
            DropForeignKey("aldemo.ProjectLines", "ProjectId", "aldemo.Projects");
            DropIndex("aldemo.ProjectLines", new[] { "LineId" });
            DropIndex("aldemo.ProjectLines", new[] { "ProjectId" });
            DropIndex("aldemo.Status", new[] { "LineId" });
            DropIndex("aldemo.Status", new[] { "ProjectId" });
            DropTable("aldemo.ProjectLines");
            DropTable("aldemo.Status");
            DropTable("aldemo.Projects");
            DropTable("aldemo.Lines");
        }
    }
}
