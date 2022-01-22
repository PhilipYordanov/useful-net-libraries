using OfficeOpenXml;
using System.Drawing;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

const string ExcelFilePath = "../../../DEV.BG.xlsx";
FileInfo fileInfo = new(ExcelFilePath);

using var package = new ExcelPackage(fileInfo);

ExcelWorksheet worksheet =
    package.Workbook.Worksheets.Add("DEV.BG");

worksheet.Cells["E1"].Value = "Useful .NET Libaries";
worksheet.Cells["E1"].Style.Fill.SetBackground(Color.Aqua);
worksheet.Cells["E1"].AutoFitColumns();

string[] topics = new[]
{
    "Angle Sharp", "EPPlus", "Polly", "Benchmark DotNet","ASP.NET Core Template",
    "MiniProfiler", "MediatR","FluentValidation", "Honorable Mentions",
};

for (int i = 0; i < topics.Length; i++)
{
    string cell = $"E{i + 2}";
    worksheet.Cells[cell].Value = topics[i];
    worksheet.Cells[cell].Style.Fill.SetBackground(Color.White);
    worksheet.Cells[cell].AutoFitColumns();
}

Image logo = Image.FromFile("cropped-dev.bg-logo.png");
var pic = worksheet.Drawings.AddPicture("Company Logo", logo);
pic.SetSize(180, 150);

package.SaveAs(ExcelFilePath);
