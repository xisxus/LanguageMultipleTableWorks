using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LanguageInstall.Data.Migrations
{
    /// <inheritdoc />
    public partial class seeddata1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanguageLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MainTables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainTableID = table.Column<int>(type: "int", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslatedText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Translation_MainTables_MainTableID",
                        column: x => x.MainTableID,
                        principalTable: "MainTables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LanguageLists",
                columns: new[] { "ID", "LanguageCode", "LanguageName" },
                values: new object[,]
                {
                    { 1, "en", "English" },
                    { 2, "es", "Spanish" },
                    { 3, "fr", "French" },
                    { 4, "de", "German" },
                    { 5, "zh", "Chinese" },
                    { 6, "ja", "Japanese" },
                    { 7, "ko", "Korean" },
                    { 8, "ar", "Arabic" },
                    { 9, "hi", "Hindi" },
                    { 10, "ru", "Russian" },
                    { 11, "pt", "Portuguese" },
                    { 12, "it", "Italian" },
                    { 13, "nl", "Dutch" },
                    { 14, "sv", "Swedish" },
                    { 15, "no", "Norwegian" },
                    { 16, "da", "Danish" },
                    { 17, "fi", "Finnish" },
                    { 18, "pl", "Polish" },
                    { 19, "tr", "Turkish" },
                    { 20, "th", "Thai" },
                    { 21, "vi", "Vietnamese" },
                    { 22, "ms", "Malay" },
                    { 23, "id", "Indonesian" },
                    { 24, "el", "Greek" },
                    { 25, "he", "Hebrew" },
                    { 26, "ur", "Urdu" },
                    { 27, "bn", "Bengali" },
                    { 28, "ta", "Tamil" },
                    { 29, "te", "Telugu" },
                    { 30, "fa", "Persian" }
                });

            migrationBuilder.InsertData(
                table: "MainTables",
                columns: new[] { "ID", "EnglishText" },
                values: new object[,]
                {
                    { 1, "First Name" },
                    { 2, "Last Name" },
                    { 3, "Submit" },
                    { 4, "Home" },
                    { 5, "Human Resource Management" },
                    { 6, "Leave Management" },
                    { 7, "Operation" },
                    { 8, "Leave Application Entry" },
                    { 9, "Company" },
                    { 10, "Employee ID" },
                    { 11, "Employee Name" },
                    { 12, "Designation" },
                    { 13, "Department" },
                    { 14, "Immediate Supervisor" },
                    { 15, "Head of Department" },
                    { 16, "Apply Leave Format" },
                    { 17, "Leave Type" },
                    { 18, "From" },
                    { 19, "Day(s)" },
                    { 20, "To" },
                    { 21, "Half Day" },
                    { 22, "First Half" },
                    { 23, "Second Half" },
                    { 24, "File Attachment" },
                    { 25, "Reason" },
                    { 26, "Leave Apply" },
                    { 27, "Select Dropdown Options" },
                    { 28, "--Select--" },
                    { 29, "--Select Apply Leave Format--" },
                    { 30, "Half Day Leave" },
                    { 31, "Full Day Leave" },
                    { 32, "Short Leave" },
                    { 33, "--Select--" },
                    { 34, "Button and Action Text" },
                    { 35, "Leave Apply" },
                    { 36, "Half Day" },
                    { 37, "Full Day" },
                    { 38, "Short Leave" },
                    { 39, "Leave Duration" },
                    { 40, "Select Employee" },
                    { 41, "Select Leave Format" },
                    { 42, "Select Leave Type" },
                    { 43, "Sick Leave" },
                    { 44, "Casual Leave" },
                    { 45, "Select Leave Type" },
                    { 46, "Enter Reason" },
                    { 47, "Cancel" },
                    { 48, "Entry ID" },
                    { 49, "--Select Employee--" },
                    { 50, "Additional Information" }
                });

            migrationBuilder.InsertData(
                table: "Translation",
                columns: new[] { "ID", "LanguageCode", "MainTableID", "TranslatedText" },
                values: new object[,]
                {
                    { 1, "bn", 1, "প্রথম নাম" },
                    { 2, "bn", 2, "শেষ নাম" },
                    { 3, "bn", 3, "জমা দিন" },
                    { 4, "bn", 4, "বাড়ি" },
                    { 5, "bn", 5, "মানবসম্পদ পরিচালনা" },
                    { 6, "bn", 6, "ব্যবস্থাপনা ছেড়ে দিন" },
                    { 7, "bn", 7, "অপারেশন" },
                    { 8, "bn", 8, "অ্যাপ্লিকেশন এন্ট্রি ছেড়ে দিন" },
                    { 9, "bn", 9, "সংস্থা" },
                    { 10, "bn", 10, "কর্মচারী আইডি" },
                    { 11, "bn", 11, "কর্মচারীর নাম" },
                    { 12, "bn", 12, "উপাধি" },
                    { 13, "bn", 13, "বিভাগ" },
                    { 14, "bn", 14, "তাত্ক্ষণিক সুপারভাইজার" },
                    { 15, "bn", 15, "বিভাগের প্রধান" },
                    { 16, "bn", 16, "ছুটি ফর্ম্যাট প্রয়োগ করুন" },
                    { 17, "bn", 17, "ছেড়ে দিন" },
                    { 18, "bn", 18, "থেকে" },
                    { 19, "bn", 19, "দিন (গুলি)" },
                    { 20, "bn", 20, "থেকে" },
                    { 21, "bn", 21, "অর্ধ দিন" },
                    { 22, "bn", 22, "প্রথমার্ধ" },
                    { 23, "bn", 23, "দ্বিতীয়ার্ধ" },
                    { 24, "bn", 24, "ফাইল সংযুক্তি" },
                    { 25, "bn", 25, "কারণ" },
                    { 26, "bn", 26, "আবেদন করুন" },
                    { 27, "bn", 27, "ড্রপডাউন বিকল্পগুলি নির্বাচন করুন" },
                    { 28, "bn", 28, "-নির্বাচন-" },
                    { 29, "bn", 29, "-নির্বাচন করুন ছুটির ফর্ম্যাট প্রয়োগ করুন-" },
                    { 30, "bn", 30, "অর্ধ দিনের ছুটি" },
                    { 31, "bn", 31, "পুরো দিন ছুটি" },
                    { 32, "bn", 32, "সংক্ষিপ্ত ছুটি" },
                    { 33, "bn", 33, "-নির্বাচন-" },
                    { 34, "bn", 34, "বোতাম এবং ক্রিয়া পাঠ্য" },
                    { 35, "bn", 35, "আবেদন করুন" },
                    { 36, "bn", 36, "অর্ধ দিন" },
                    { 37, "bn", 37, "পুরো দিন" },
                    { 38, "bn", 38, "সংক্ষিপ্ত ছুটি" },
                    { 39, "bn", 39, "সময়কাল" },
                    { 40, "bn", 40, "কর্মচারী নির্বাচন করুন" },
                    { 41, "bn", 41, "ছুটি ফর্ম্যাট নির্বাচন করুন" },
                    { 42, "bn", 42, "ছুটি প্রকার নির্বাচন করুন" },
                    { 43, "bn", 43, "অসুস্থ ছুটি" },
                    { 44, "bn", 44, "নৈমিত্তিক ছুটি" },
                    { 45, "bn", 45, "ছুটি প্রকার নির্বাচন করুন" },
                    { 46, "bn", 46, "কারণ লিখুন" },
                    { 47, "bn", 47, "বাতিল" },
                    { 48, "bn", 48, "এন্ট্রি আইডি" },
                    { 49, "bn", 49, "-- কর্মচারী নির্বাচন করুন --" },
                    { 50, "bn", 50, "অতিরিক্ত তথ্য" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translation_MainTableID",
                table: "Translation",
                column: "MainTableID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageLists");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MainTables");
        }
    }
}
