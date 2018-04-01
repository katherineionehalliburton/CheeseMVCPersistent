using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CheeseMVC.Migrations
{
    public partial class fixingStepThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheeseMenus_Menu_MenuID",
                table: "CheeseMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheeseMenus_Menus_MenuID",
                table: "CheeseMenus",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheeseMenus_Menus_MenuID",
                table: "CheeseMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheeseMenus_Menu_MenuID",
                table: "CheeseMenus",
                column: "MenuID",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
