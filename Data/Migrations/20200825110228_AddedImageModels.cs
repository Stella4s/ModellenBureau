using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModellenBureau.Data.Migrations
{
    public partial class AddedImageModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImagesOnDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    PhotoModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesOnDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesOnDatabase_PhotoModel_PhotoModelId",
                        column: x => x.PhotoModelId,
                        principalTable: "PhotoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagesOnFileSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesOnFileSystem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LogoId",
                table: "Customer",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesOnDatabase_PhotoModelId",
                table: "ImagesOnDatabase",
                column: "PhotoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ImagesOnDatabase_LogoId",
                table: "Customer",
                column: "LogoId",
                principalTable: "ImagesOnDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ImagesOnDatabase_LogoId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "ImagesOnDatabase");

            migrationBuilder.DropTable(
                name: "ImagesOnFileSystem");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LogoId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
