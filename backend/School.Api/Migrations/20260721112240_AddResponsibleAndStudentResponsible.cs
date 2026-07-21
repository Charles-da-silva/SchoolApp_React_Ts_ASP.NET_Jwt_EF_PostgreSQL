using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddResponsibleAndStudentResponsible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "responsibles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    cpf = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    profession = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deactivated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_responsibles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student_responsibles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    responsible_id = table.Column<Guid>(type: "uuid", nullable: false),
                    relationship_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    can_pick_up_child = table.Column<bool>(type: "boolean", nullable: false),
                    is_finance_contact = table.Column<bool>(type: "boolean", nullable: false),
                    is_legal_responsable = table.Column<bool>(type: "boolean", nullable: false),
                    observation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_responsibles", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_responsibles_responsibles_responsible_id",
                        column: x => x.responsible_id,
                        principalTable: "responsibles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_student_responsibles_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_student_responsibles_responsible_id",
                table: "student_responsibles",
                column: "responsible_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_responsibles_student_id_responsible_id",
                table: "student_responsibles",
                columns: new[] { "student_id", "responsible_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student_responsibles");

            migrationBuilder.DropTable(
                name: "responsibles");
        }
    }
}
