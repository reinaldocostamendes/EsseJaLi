using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsseJaLi.Migrations
{
    public partial class ActualizacaoLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCategoria",
                table: "LivrosLeitores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "LivrosLeitores");
        }
    }
}
