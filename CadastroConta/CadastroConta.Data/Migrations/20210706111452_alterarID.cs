﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroConta.Data.Migrations
{
    public partial class alterarID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DiasEmAtraso = table.Column<int>(nullable: false),
                    ValorOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorCorrigido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    PagamentoRealizado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar(100)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "varchar(100)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
