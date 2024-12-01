using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CludeTestApi.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TipoDocumento = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EspecialidadeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissionais_Especialidades_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nome", "TipoDocumento" },
                values: new object[,]
                {
                    { 1, "Cardiologia", "CRM" },
                    { 2, "Ortopedia", "CRM" },
                    { 3, "Pediatria", "CRM" },
                    { 4, "Dermatologia", "CRM" },
                    { 5, "Ginecologia", "CRM" },
                    { 6, "Neurologia", "CRM" },
                    { 7, "Psiquiatria", "CRM" },
                    { 8, "Anestesiologia", "CRM" },
                    { 9, "Radiologia", "CRM" },
                    { 10, "Endocrinologia", "CRM" },
                    { 11, "Nutricionista Clínico", "CRN" },
                    { 12, "Nutrição Esportiva", "CRN" },
                    { 13, "Nutrição Oncológica", "CRN" },
                    { 14, "Fisioterapia Ortopédica", "CREFITO" },
                    { 15, "Fisioterapia Neurológica", "CREFITO" },
                    { 16, "Fisioterapia Respiratória", "CREFITO" },
                    { 17, "Ortodontia", "CRO" },
                    { 18, "Implantodontia", "CRO" },
                    { 19, "Endodontia", "CRO" },
                    { 20, "Enfermagem Geral", "COREN" },
                    { 21, "Enfermagem Obstétrica", "COREN" },
                    { 22, "Psicologia Clínica", "CRP" },
                    { 23, "Psicologia Organizacional", "CRP" },
                    { 24, "Neuropsicologia", "CRP" },
                    { 25, "Farmácia Clínica", "CRF" },
                    { 26, "Farmácia Hospitalar", "CRF" },
                    { 27, "Análises Clínicas", "CRF" },
                    { 28, "Personal Trainer", "CREF" },
                    { 29, "Preparação Física", "CREF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_EspecialidadeId",
                table: "Profissionais",
                column: "EspecialidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "Especialidades");
        }
    }
}
