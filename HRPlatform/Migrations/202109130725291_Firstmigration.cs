namespace HRPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        DateOfBirth = c.String(),
                        ContactNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillCandidates",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Candidate_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Candidate_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillCandidates", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.SkillCandidates", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillCandidates", new[] { "Candidate_Id" });
            DropIndex("dbo.SkillCandidates", new[] { "Skill_Id" });
            DropTable("dbo.SkillCandidates");
            DropTable("dbo.Skills");
            DropTable("dbo.Candidates");
        }
    }
}
