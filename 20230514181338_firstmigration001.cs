using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flex.Migrations
{
    public partial class firstmigration001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    courseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    courseDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    creditHours = table.Column<int>(type: "int", nullable: false),
                    preReq_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.courseID);
                    table.ForeignKey(
                        name: "FK_Course_Course_preReq_ID",
                        column: x => x.preReq_ID,
                        principalTable: "Course",
                        principalColumn: "courseID");
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Userprofile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    loginID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    s_rollNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    s_section = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    s_degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    s_batch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cnic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userprofile", x => x.Id);
                    table.UniqueConstraint("AK_Userprofile_loginID", x => x.loginID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemId = table.Column<int>(type: "int", nullable: false),
                    SCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterCourses_Course_SCourseId",
                        column: x => x.SCourseId,
                        principalTable: "Course",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterCourses_Semester_SemId",
                        column: x => x.SemId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionSemesterCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterCourseId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionSemesterCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionSemesterCourses_SemesterCourses_SemesterCourseId",
                        column: x => x.SemesterCourseId,
                        principalTable: "SemesterCourses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FacultySectionSemesterCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterCourseId = table.Column<int>(type: "int", nullable: true),
                    loginID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    assignments = table.Column<int>(type: "int", nullable: true),
                    quizzes = table.Column<int>(type: "int", nullable: true),
                    midTerms = table.Column<int>(type: "int", nullable: true),
                    finals = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultySectionSemesterCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultySectionSemesterCourses_SectionSemesterCourses_SectionId",
                        column: x => x.SectionId,
                        principalTable: "SectionSemesterCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacultySectionSemesterCourses_SemesterCourses_SemesterCourseId",
                        column: x => x.SemesterCourseId,
                        principalTable: "SemesterCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacultySectionSemesterCourses_Userprofile_loginID",
                        column: x => x.loginID,
                        principalTable: "Userprofile",
                        principalColumn: "loginID");
                });

            migrationBuilder.CreateTable(
                name: "SectionAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionAttendance_SectionSemesterCourses_SectionId",
                        column: x => x.SectionId,
                        principalTable: "SectionSemesterCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSectionSemesterCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterCourseId = table.Column<int>(type: "int", nullable: true),
                    loginID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    isApproved = table.Column<bool>(type: "bit", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    assignments = table.Column<int>(type: "int", nullable: true),
                    quizzes = table.Column<int>(type: "int", nullable: true),
                    midTerms = table.Column<int>(type: "int", nullable: true),
                    finals = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSectionSemesterCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSectionSemesterCourses_SectionSemesterCourses_SectionId",
                        column: x => x.SectionId,
                        principalTable: "SectionSemesterCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSectionSemesterCourses_SemesterCourses_SemesterCourseId",
                        column: x => x.SemesterCourseId,
                        principalTable: "SemesterCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSectionSemesterCourses_Userprofile_loginID",
                        column: x => x.loginID,
                        principalTable: "Userprofile",
                        principalColumn: "loginID");
                });

            migrationBuilder.CreateTable(
                name: "StudentSectionAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudSecSemCourseId = table.Column<int>(type: "int", nullable: false),
                    SectionAttendanceId = table.Column<int>(type: "int", nullable: false),
                    present = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSectionAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSectionAttendance_SectionAttendance_SectionAttendanceId",
                        column: x => x.SectionAttendanceId,
                        principalTable: "SectionAttendance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSectionAttendance_StudentSectionSemesterCourses_StudSecSemCourseId",
                        column: x => x.StudSecSemCourseId,
                        principalTable: "StudentSectionSemesterCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { "12bb9f11-6587-4f9a-917b-14fc5e5c42c1", 0, "15398a60-9661-4ea7-a6f4-c6c045949e08", "Salman@gmail.com", true, false, null, "Salman@gmail.com", "Salman@gmail.com", "AQAAAAEAACcQAAAAEDYp7tIh+5VNM4Chx3tDwWxOp24GfsbR+SPYo+Y/M3AMbcEpPfcl1I4LNhRYEEfxvw==", null, false, "ac179198-19cf-4ffb-87ac-ffff816dbba3", false, "Salman@gmail.com", 2 },
                    { "1ed0b7a0-e0bc-4861-a0f0-37c8cdd662a1", 0, "5c2162fa-7d86-4f25-b70e-f3a5e23e825b", "Owais@gmail.com", true, false, null, "Owais@gmail.com", "Owais@gmail.com", "AQAAAAEAACcQAAAAEISSyuA18r3SWpEMjz6/Vb7DTQeiOmnlPpJBsiipgJIeehoGGm2eWMNGerI+gEK/TQ==", null, false, "7448fd00-17a5-468d-a2f4-94f90f1586ee", false, "Owais@gmail.com", 1 },
                    { "630c76c9-999e-4421-9e96-804c6bec4c39", 0, "ee1d2f71-00d7-402f-a085-c8af99b88fe2", "NoorulAin@gmail.com", true, false, null, "NoorulAin@gmail.com", "NoorulAin@gmail.com", "AQAAAAEAACcQAAAAELcgHDrbv0YaYTj7ygNqP6/m2m1iQKpDCtUYNCJQp5Gleb4WyOb94A8C6xphGDSnRQ==", null, false, "666cab24-73d0-4295-aa20-4e556c036844", false, "NoorulAin@gmail.com", 1 },
                    { "90393265-1b3b-4b72-b2b7-f4598cbfec68", 0, "ce2846c8-32e4-404f-84d1-4a18e03979a6", "Amna@gmail.com", true, false, null, "Amna@gmail.com", "Amna@gmail.com", "AQAAAAEAACcQAAAAENjI0l3l34ENS3zH/dEy6yfKYsYQ0GSNKlAFRTvo5kwbeCAazovHVEec90VUhWRfsA==", null, false, "338d3e4f-d34a-42f3-b5e4-51c02f2d3394", false, "Amna@gmail.com", 2 },
                    { "c59157f5-6869-4189-b97f-b45ae8b4ef00", 0, "ef031231-1807-45b4-8d5b-97f8bcd6f3d4", "Fatima@gmail.com", true, false, null, "Fatima@gmail.com", "Fatima@gmail.com", "AQAAAAEAACcQAAAAEC7s8nbs1TlfKw6gsvXwWMJhXqZCRAUaSV231Gb2ZQp45yqvr4YMsdlowWHvBYoTXA==", null, false, "309dafaf-8052-49cd-9c11-78f8f20a3d9d", false, "Fatima@gmail.com", 2 },
                    { "e15bae4a-c92f-428b-be0d-96389ae166ac", 0, "54c38a8b-f254-4976-89d0-3857e612bf7a", "FacultyAdmin@gmail.com", true, false, null, "FacultyAdmin@gmail.com", "FacultyAdmin@gmail.com", "AQAAAAEAACcQAAAAEM6idcO88XXa7z8LCI5Fi6MlSqaM3Hg6oCpMOr9ACXvzsGnt4XKSh9VGi8TYKEbinw==", null, false, "3df998ad-d2fd-4ee3-80f7-d5a7fc493c86", false, "FacultyAdmin@gmail.com", 0 },
                    { "e33a3f37-0026-4775-b6fd-4287d4105c5f", 0, "147089c6-3af9-4284-9cf3-a5d13a1dca34", "Nirmal@gmail.com", true, false, null, "Nirmal@gmail.com", "Nirmal@gmail.com", "AQAAAAEAACcQAAAAEDImlaGNPkLmSV3WqrMwuQPFrcAxA/P1rFXGPVJT+Ywmz8geZavZeIkX/e/VPRPiIQ==", null, false, "1b1eb378-371c-422b-ac5e-ca9c06f07e2b", false, "Nirmal@gmail.com", 1 }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "courseID", "courseCode", "courseDescription", "courseName", "creditHours", "preReq_ID" },
                values: new object[,]
                {
                    { -10, "CS001", "Programming Fundamental", "Programming Fundamental", 3, null },
                    { -9, "CS002", "Object Oriented Programming", "Object Oriented Programming", 3, null },
                    { -8, "CS003", "Operating System", "Operating System", 3, null },
                    { -7, "CS004", "Relational Database Systems", "Relational Database Systems", 3, null },
                    { -6, "CS005", "Design and Algorithm", "Design and Algorithm", 3, null },
                    { -5, "CS006", "Compiler Construction", "Compiler Construction", 3, null },
                    { -4, "CS007", "Computer Architecture", "Computer Architecture", 3, null }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "Id", "Description", "Name", "endDate", "startDate" },
                values: new object[] { -10, "Fall Semester 2023", "Fall Semester 2023", new DateTime(2023, 5, 14, 23, 13, 37, 834, DateTimeKind.Local).AddTicks(7228), new DateTime(2022, 12, 14, 23, 13, 37, 834, DateTimeKind.Local).AddTicks(7193) });

            migrationBuilder.InsertData(
                table: "Userprofile",
                columns: new[] { "Id", "DOB", "FirstName", "LastName", "address", "city", "cnic", "country", "gender", "loginID", "phoneNumber", "s_batch", "s_degree", "s_rollNumber", "s_section" },
                values: new object[,]
                {
                    { -7, null, "Amir", "Khan", "G-11/4", "IBD", "111-111111-1", "PK", null, "e15bae4a-c92f-428b-be0d-96389ae166ac", "0512260915", null, null, null, null },
                    { -6, null, "Noorul", "Ain", "G-11/4", "IBD", "111-111111-1", "PK", null, "630c76c9-999e-4421-9e96-804c6bec4c39", "0512260915", null, null, null, null },
                    { -5, null, "Nirmal", "Chaudhary", "G-11/4", "IBD", "111-111111-1", "PK", null, "e33a3f37-0026-4775-b6fd-4287d4105c5f", "0512260915", null, null, null, null },
                    { -4, null, "Owais", "Idrees", "G-11/4", "IBD", "111-111111-1", "PK", null, "1ed0b7a0-e0bc-4861-a0f0-37c8cdd662a1", "0512260915", null, null, null, null },
                    { -3, null, "Fatima", "Athar Khan", "G-11/4", "IBD", "111-111111-1", "PK", null, "c59157f5-6869-4189-b97f-b45ae8b4ef00", "0512260915", null, null, "21i0385", null },
                    { -2, null, "Amna", "Usman", "G-11/4", "IBD", "111-111111-1", "PK", null, "90393265-1b3b-4b72-b2b7-f4598cbfec68", "0512260915", null, null, "21i2555", null },
                    { -1, null, "Salman", "Jan", "G-11/4", "IBD", "111-111111-1", "PK", null, "12bb9f11-6587-4f9a-917b-14fc5e5c42c1", "0512260915", null, null, "21i2574", null }
                });

            migrationBuilder.InsertData(
                table: "SemesterCourses",
                columns: new[] { "Id", "SCourseId", "SemId" },
                values: new object[,]
                {
                    { -10, -10, -10 },
                    { -9, -9, -10 },
                    { -8, -8, -10 },
                    { -7, -7, -10 },
                    { -6, -6, -10 },
                    { -5, -5, -10 },
                    { -4, -4, -10 }
                });

            migrationBuilder.InsertData(
                table: "FacultySectionSemesterCourses",
                columns: new[] { "Id", "SectionId", "SemesterCourseId", "assignments", "finals", "loginID", "midTerms", "quizzes" },
                values: new object[,]
                {
                    { -12, null, -9, 10, 50, "630c76c9-999e-4421-9e96-804c6bec4c39", 30, 10 },
                    { -11, null, -8, 10, 50, "630c76c9-999e-4421-9e96-804c6bec4c39", 30, 10 },
                    { -10, null, -7, 10, 50, "630c76c9-999e-4421-9e96-804c6bec4c39", 30, 10 },
                    { -9, null, -10, 10, 50, "e33a3f37-0026-4775-b6fd-4287d4105c5f", 30, 10 },
                    { -8, null, -8, 10, 50, "e33a3f37-0026-4775-b6fd-4287d4105c5f", 30, 10 },
                    { -7, null, -7, 10, 50, "e33a3f37-0026-4775-b6fd-4287d4105c5f", 30, 10 },
                    { -6, null, -6, 10, 50, "e33a3f37-0026-4775-b6fd-4287d4105c5f", 30, 10 },
                    { -5, null, -4, 10, 50, "1ed0b7a0-e0bc-4861-a0f0-37c8cdd662a1", 30, 10 },
                    { -4, null, -5, 10, 50, "1ed0b7a0-e0bc-4861-a0f0-37c8cdd662a1", 30, 10 },
                    { -3, null, -6, 10, 50, "1ed0b7a0-e0bc-4861-a0f0-37c8cdd662a1", 30, 10 }
                });

            migrationBuilder.InsertData(
                table: "StudentSectionSemesterCourses",
                columns: new[] { "Id", "SectionId", "SemesterCourseId", "assignments", "finals", "isApproved", "loginID", "midTerms", "quizzes" },
                values: new object[,]
                {
                    { -15, null, -9, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -14, null, -8, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -13, null, -7, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -12, null, -6, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -11, null, -5, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -10, null, -4, null, null, false, "c59157f5-6869-4189-b97f-b45ae8b4ef00", null, null },
                    { -8, null, -9, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null },
                    { -7, null, -8, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null },
                    { -6, null, -7, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null },
                    { -5, null, -6, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null },
                    { -4, null, -5, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null },
                    { -3, null, -4, null, null, false, "90393265-1b3b-4b72-b2b7-f4598cbfec68", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Course_preReq_ID",
                table: "Course",
                column: "preReq_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultySectionSemesterCourses_loginID",
                table: "FacultySectionSemesterCourses",
                column: "loginID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultySectionSemesterCourses_SectionId",
                table: "FacultySectionSemesterCourses",
                column: "SectionId",
                unique: true,
                filter: "[SectionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FacultySectionSemesterCourses_SemesterCourseId",
                table: "FacultySectionSemesterCourses",
                column: "SemesterCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionAttendance_SectionId",
                table: "SectionAttendance",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSemesterCourses_SemesterCourseId",
                table: "SectionSemesterCourses",
                column: "SemesterCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCourses_SCourseId",
                table: "SemesterCourses",
                column: "SCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCourses_SemId",
                table: "SemesterCourses",
                column: "SemId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSectionAttendance_SectionAttendanceId",
                table: "StudentSectionAttendance",
                column: "SectionAttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSectionAttendance_StudSecSemCourseId",
                table: "StudentSectionAttendance",
                column: "StudSecSemCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSectionSemesterCourses_loginID",
                table: "StudentSectionSemesterCourses",
                column: "loginID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSectionSemesterCourses_SectionId",
                table: "StudentSectionSemesterCourses",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSectionSemesterCourses_SemesterCourseId",
                table: "StudentSectionSemesterCourses",
                column: "SemesterCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Userprofile_loginID",
                table: "Userprofile",
                column: "loginID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "FacultySectionSemesterCourses");

            migrationBuilder.DropTable(
                name: "StudentSectionAttendance");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SectionAttendance");

            migrationBuilder.DropTable(
                name: "StudentSectionSemesterCourses");

            migrationBuilder.DropTable(
                name: "SectionSemesterCourses");

            migrationBuilder.DropTable(
                name: "Userprofile");

            migrationBuilder.DropTable(
                name: "SemesterCourses");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Semester");
        }
    }
}
