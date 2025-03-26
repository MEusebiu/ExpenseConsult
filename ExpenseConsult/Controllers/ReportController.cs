using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Claims;

namespace ExpenseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ILogger<ReportController> _logger;
        public ReportController(ICategoryService categoryService, ILogger<ReportController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;

            QuestPDF.Settings.License = LicenseType.Community;
        }

        [HttpGet("generate-pdf")]
        public async Task<IActionResult> GenerateExpenseReport()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var groupedExpenses = await _categoryService.GetReportInformation(userId);

            var pdfBytes = GeneratePdf(groupedExpenses);

            _logger.LogInformation("Successfully generated pdf file");

            return File(pdfBytes, "application/pdf", "ExpenseReport.pdf");
        }

        private byte[] GeneratePdf(Dictionary<string, List<Expense>> groupedExpenses)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);

                    page.Header()
                        .AlignCenter()
                        .PaddingBottom(30)
                        .Text("Expenses")
                        .Bold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        foreach (var (categoryId, expenses) in groupedExpenses)
                        {
                            col.Item().Text(categoryId)
                                .Bold()
                                .FontSize(14)
                                .FontColor(Colors.Black);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50);  
                                    columns.RelativeColumn(2);  
                                    columns.RelativeColumn(1);  
                                    columns.RelativeColumn(1);  
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Description").Bold();
                                    header.Cell().Text("Amount").Bold();
                                    header.Cell().Text("Date").Bold();
                                });

                                var index = 1;
                                foreach (var expense in expenses)
                                {
                                    table.Cell().Text(index++.ToString());
                                    table.Cell().Text(expense.Description);
                                    table.Cell().Text($"{expense.Amount}");
                                    table.Cell().Text(expense.CreatedDate.ToString("dd.MM.yy"));
                                }
                            });

                            col.Item().PaddingBottom(20);
                        }
                    });

                    page.Footer()
                        .AlignRight()
                        .Text($"Generated at {DateTime.UtcNow:ddd dd-MM-yyyy HH:mm}")
                        .FontSize(12);
                });
            }).GeneratePdf();

            return document;
        }
    }
}
