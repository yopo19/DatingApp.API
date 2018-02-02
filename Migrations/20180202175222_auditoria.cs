using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatingApp.API.Migrations
{
    public partial class auditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Accion = table.Column<string>(nullable: true),
                    ColumnasMod = table.Column<string>(nullable: true),
                    DataNueva = table.Column<string>(nullable: true),
                    DataOriginal = table.Column<string>(nullable: true),
                    FechaRevision = table.Column<DateTime>(nullable: false),
                    NombreTabla = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");
        }
    }
}
