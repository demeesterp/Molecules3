﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Molecules.Core.Data.Migrations
{
    public partial class AddBasissetCodeToCalcOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasissetCode",
                schema: "moleculesapp",
                table: "CalcOrderItem",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasissetCode",
                schema: "moleculesapp",
                table: "CalcOrderItem");
        }
    }
}

