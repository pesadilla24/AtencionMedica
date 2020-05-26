using Microsoft.EntityFrameworkCore.Migrations;

namespace AtencionMedica_v1.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultorio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMedico = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTriage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTriage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    Sexo = table.Column<string>(nullable: true),
                    CodigoADN = table.Column<string>(nullable: true),
                    Diagnostico = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    TipoTriageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paciente_TipoTriage_TipoTriageId",
                        column: x => x.TipoTriageId,
                        principalTable: "TipoTriage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atencion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultorioId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atencion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atencion_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atencion_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_ConsultorioId",
                table: "Atencion",
                column: "ConsultorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_PacienteId",
                table: "Atencion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_TipoTriageId",
                table: "Paciente",
                column: "TipoTriageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atencion");

            migrationBuilder.DropTable(
                name: "Consultorio");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "TipoTriage");
        }
    }
}
