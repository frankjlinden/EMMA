using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CT.DDS.EMMA.Sync.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "SmtpConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConfigName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    UseAuthentication = table.Column<bool>(nullable: false),
                    SecureSocketOptions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobConfig",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SysStart = table.Column<DateTime>(nullable: false),
                    SysEnd = table.Column<DateTime>(nullable: false),
                    SysUser = table.Column<string>(nullable: true),
                    SysUserNext = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(maxLength: 255, nullable: true),
                    ViewName = table.Column<string>(maxLength: 150, nullable: true),
                    SenderAddress = table.Column<string>(maxLength: 255, nullable: true),
                    SenderName = table.Column<string>(maxLength: 50, nullable: true),
                    BodyTemplate = table.Column<string>(maxLength: 8000, nullable: true),
                    SubjectTemplate = table.Column<string>(maxLength: 150, nullable: true),
                    DataRateDays = table.Column<int>(nullable: false),
                    MessageResendLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Execution",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    SysUser = table.Column<string>(nullable: true),
                    NewSendAttempts = table.Column<int>(nullable: false),
                    NewSendFails = table.Column<int>(nullable: false),
                    ResendAttempts = table.Column<int>(nullable: false),
                    ResendFails = table.Column<int>(nullable: false),
                    Sender = table.Column<string>(maxLength: 150, nullable: true),
                    JobConfigId = table.Column<int>(nullable: false),
                    ResultText = table.Column<string>(nullable: true),
                    SmtpConfigId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Execution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Execution_JobConfig_JobConfigId",
                        column: x => x.JobConfigId,
                        principalSchema: "dbo",
                        principalTable: "JobConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Execution_SmtpConfig_SmtpConfigId",
                        column: x => x.SmtpConfigId,
                        principalTable: "SmtpConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SysStart = table.Column<DateTime>(nullable: false),
                    SysEnd = table.Column<DateTime>(nullable: false),
                    SysUser = table.Column<string>(nullable: true),
                    SysUserNext = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    Cc = table.Column<string>(nullable: true),
                    Bcc = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    AttemptCount = table.Column<int>(nullable: false),
                    JobConfigId = table.Column<int>(nullable: false),
                    ExecutionId = table.Column<int>(nullable: false),
                    ErrorText = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Execution_ExecutionId",
                        column: x => x.ExecutionId,
                        principalSchema: "dbo",
                        principalTable: "Execution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_JobConfig_JobConfigId",
                        column: x => x.JobConfigId,
                        principalSchema: "dbo",
                        principalTable: "JobConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Execution_JobConfigId",
                schema: "dbo",
                table: "Execution",
                column: "JobConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Execution_SmtpConfigId",
                schema: "dbo",
                table: "Execution",
                column: "SmtpConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ExecutionId",
                schema: "dbo",
                table: "Message",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_JobConfigId",
                schema: "dbo",
                table: "Message",
                column: "JobConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Execution",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobConfig",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SmtpConfig");
        }
    }
}
