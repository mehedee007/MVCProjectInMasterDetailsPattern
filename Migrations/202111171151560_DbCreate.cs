namespace MVCProjectInMasterDetailsPattern.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        CourseDuration = c.String(),
                        CourseFee = c.Int(),
                        //FeeWithVat = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CourseID);
            Sql("ALTER TABLE Courses ADD FeeWithVat AS (CourseFee + (CourseFee * .15))");

            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeID = c.Int(nullable: false, identity: true),
                        TraineeName = c.String(),
                        Gender = c.String(),
                        DOB = c.DateTime(nullable: false),
                        TraineeEmail = c.String(),
                        TraineeContact = c.String(),
                        CourseID = c.Int(nullable: false),
                        TraineeImage = c.String(),
                    })
                .PrimaryKey(t => t.TraineeID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.TraineeModuleDescriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        TraineeID = c.Int(nullable: false),
                        ExternalMark = c.Int(nullable: false),
                        ExternalDate = c.DateTime(nullable: false),
                        EvidenceMark = c.Int(nullable: false),
                        EvidenceDate = c.DateTime(nullable: false),
                        IsPass = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trainees", t => t.TraineeID, cascadeDelete: true)
                .Index(t => t.TraineeID);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        TrainerID = c.Int(nullable: false, identity: true),
                        TrainerName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        TrainerContact = c.String(),
                        TrainerEmail = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TrainerImage = c.String(),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainerID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.TrainerExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Designation = c.String(),
                        TrainerID = c.Int(nullable: false),
                        Institution = c.String(),
                        ExperienceInYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trainers", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.TrainerID);
            
            CreateTable(
                "dbo.TSPs",
                c => new
                    {
                        TspID = c.Int(nullable: false, identity: true),
                        TspName = c.String(),
                        TspAddress = c.String(),
                        TspContact = c.String(),
                        TspEmail = c.String(),
                        TrainerID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TspID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: false)
                .ForeignKey("dbo.Trainers", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.TrainerID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TSPs", "TrainerID", "dbo.Trainers");
            DropForeignKey("dbo.TSPs", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.TrainerExperiences", "TrainerID", "dbo.Trainers");
            DropForeignKey("dbo.Trainers", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.TraineeModuleDescriptions", "TraineeID", "dbo.Trainees");
            DropForeignKey("dbo.Trainees", "CourseID", "dbo.Courses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TSPs", new[] { "CourseID" });
            DropIndex("dbo.TSPs", new[] { "TrainerID" });
            DropIndex("dbo.TrainerExperiences", new[] { "TrainerID" });
            DropIndex("dbo.Trainers", new[] { "CourseID" });
            DropIndex("dbo.TraineeModuleDescriptions", new[] { "TraineeID" });
            DropIndex("dbo.Trainees", new[] { "CourseID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TSPs");
            DropTable("dbo.TrainerExperiences");
            DropTable("dbo.Trainers");
            DropTable("dbo.TraineeModuleDescriptions");
            DropTable("dbo.Trainees");
            DropTable("dbo.Courses");
        }
    }
}
