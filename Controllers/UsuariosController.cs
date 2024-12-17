using IncapacidadesWeb.Data.Models;
using IncapacidadesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using ClosedXML.Excel;
using System.IO;
using System.Threading.Tasks;

namespace IncapacidadesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        // Inyección del servicio de Usuario
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            try
            {
                // Crear el usuario utilizando el servicio
                var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuario);

                // Respuesta exitosa con el usuario creado
                return CreatedAtAction(nameof(CrearUsuario), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);  // Mensaje si falta algún dato requerido
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);  // Error al intentar guardar el usuario
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        // GET: api/Usuarios/generatepdf
        [HttpGet("generatepdf")]
        public IActionResult GenerateDisabilityReport()
        {
            try 
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    PdfDocument document = new PdfDocument();
                    Random random = new Random();
                    
                    // Comprehensive Data Pools
                    string[] firstNames = { 
                        "Juan", "Carlos", "María", "Ana", "Luis", "Pedro", "José", "Elena", 
                        "Diego", "Laura", "Andrea", "Manuel", "Sofía", "Javier", "Carmen", 
                        "Miguel", "Isabel", "Roberto", "Alejandro", "Patricia", "Daniel", 
                        "Martín", "Lucía", "Fernando", "Carolina", "David", "Gabriela"
                    };

                    string[] departments = { 
                        "Recursos Humanos", "Contabilidad", "Ventas", "Marketing", 
                        "Desarrollo de Software", "Soporte Técnico", "Logística", 
                        "Producción", "Investigación y Desarrollo", "Atención al Cliente", 
                        "Compras", "Control de Calidad", "Legales", "Administración", 
                        "Relaciones Públicas", "Finanzas", "Auditoría", "Innovación", 
                        "Operaciones", "Proyectos Especiales"
                    };
                    
                    string[] lastNames = { 
                        "Pérez", "González", "Rodríguez", "Martínez", "López", "Sánchez", 
                        "Ramírez", "Torres", "Díaz", "Ruiz", "Hernández", "Morales", "Jiménez", 
                        "Castillo", "Gómez", "Álvarez", "Rivera", "Fernández", "Navarro", 
                        "Méndez", "Castro", "Ortiz", "Muñoz"
                    };
                    
                    string[] diagnoses = { 
                        "Lumbalgia Crónica", "Tendinitis Severa", "Estrés Laboral Avanzado", 
                        "Recuperación Postquirúrgica", "Síndrome de Túnel Carpiano", 
                        "Lesión Muscular Compleja", "Fatiga Crónica", "Burnout", 
                        "Depresión Ocupacional", "Hernias Discales", "Problemas Respiratorios", 
                        "Trastorno de Ansiedad", "Migraña Crónica", "Epicondilitis", 
                        "Cervicalgia", "Síndrome del Manguito Rotador", "Lesión Deportiva"
                    };

                    // Sophisticated Fonts
                    XFont titleFont = new XFont("Helvetica", 24, XFontStyle.Bold);
                    XFont subtitleFont = new XFont("Helvetica", 16, XFontStyle.Regular);
                    XFont headerFont = new XFont("Helvetica", 18, XFontStyle.Bold);
                    XFont contentFont = new XFont("Helvetica", 12, XFontStyle.Regular);
                    XFont smallFont = new XFont("Helvetica", 10, XFontStyle.Regular);

                    // Comprehensive Cover Page
                    PdfPage coverPage = document.AddPage();
                    XGraphics gfxCover = XGraphics.FromPdfPage(coverPage);
                    
                    gfxCover.DrawRectangle(XPens.Navy, XBrushes.White, new XRect(50, 50, coverPage.Width - 100, coverPage.Height - 100));
                    gfxCover.DrawString("INFORME DETALLADO", titleFont, XBrushes.Navy, new XPoint(150, 250));
                    gfxCover.DrawString("DE INCAPACIDADES LABORALES", titleFont, XBrushes.Navy, new XPoint(100, 300));
                    gfxCover.DrawString("Periodo: Enero - Diciembre 2024", subtitleFont, XBrushes.Black, new XPoint(180, 400));
                    gfxCover.DrawString("Departamento de Recursos Humanos", subtitleFont, XBrushes.Gray, new XPoint(200, 600));

                    // Detailed Statistics Page
                    PdfPage statsPage = document.AddPage();
                    XGraphics gfxStats = XGraphics.FromPdfPage(statsPage);

                    gfxStats.DrawString("ANÁLISIS ESTADÍSTICO EJECUTIVO", headerFont, XBrushes.DarkBlue, new XPoint(100, 50));

                    int totalCases = 54;
                    int totalDaysOff = 0;
                    Dictionary<string, int> departmentCases = new Dictionary<string, int>();
                    Dictionary<string, int> diagnosisCases = new Dictionary<string, int>();

                    // Pre-calculate statistics for visualization
                    var casesList = new List<(string Name, string Department, string Diagnosis, int DaysOff, bool IsCritical)>();

                    for (int i = 0; i < totalCases; i++)
                    {
                        string name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
                        string department = departments[random.Next(departments.Length)];
                        string diagnosis = diagnoses[random.Next(diagnoses.Length)];
                        int daysOff = random.Next(5, 45);
                        bool isCritical = random.Next(100) < 15;

                        // Track statistics
                        totalDaysOff += daysOff;
                        departmentCases[department] = departmentCases.ContainsKey(department) ? departmentCases[department] + 1 : 1;
                        diagnosisCases[diagnosis] = diagnosisCases.ContainsKey(diagnosis) ? diagnosisCases[diagnosis] + 1 : 1;

                        casesList.Add((name, department, diagnosis, daysOff, isCritical));
                    }

                    // Visualization of statistics
                    string[] statisticsDetails = {
                        $"Total Casos de Incapacidad: {totalCases}",
                        $"Total Días de Reposo: {totalDaysOff}",
                        $"Promedio Días por Incapacidad: {(totalDaysOff / (double)totalCases):F2}",
                        $"Departamento más Afectado: {departmentCases.OrderByDescending(x => x.Value).First().Key}"
                    };

                    for (int i = 0; i < statisticsDetails.Length; i++)
                    {
                        gfxStats.DrawString(statisticsDetails[i], contentFont, XBrushes.Black, 
                            new XPoint(100, 150 + (i * 50)));
                    }

                    // Top Departments Chart (Simplified)
                    var topDepartments = departmentCases.OrderByDescending(x => x.Value).Take(5);
                    for (int i = 0; i < topDepartments.Count(); i++)
                    {
                        var dept = topDepartments.ElementAt(i);
                        gfxStats.DrawString($"{dept.Key}: {dept.Value} casos", smallFont, XBrushes.DarkBlue, 
                            new XPoint(100, 350 + (i * 30)));
                        
                        XRect barRect = new XRect(300, 345 + (i * 30), dept.Value * 5, 20);
                        gfxStats.DrawRectangle(XPens.Navy, XBrushes.LightBlue, barRect);
                    }

                    // Multiple Pages for Case Details
                    for (int pageNum = 1; pageNum <= 10; pageNum++)
                    {
                        PdfPage casesPage = document.AddPage();
                        XGraphics gfxCases = XGraphics.FromPdfPage(casesPage);

                        gfxCases.DrawString($"DETALLE DE CASOS - PÁGINA {pageNum}", headerFont, XBrushes.DarkBlue, new XPoint(150, 30));

                        int casesOnPage = 5;
                        int startIndex = (pageNum - 1) * casesOnPage;
                        
                        for (int i = 0; i < casesOnPage && startIndex + i < casesList.Count; i++)
                        {
                            var currentCase = casesList[startIndex + i];
                            int verticalPosition = 100 + (i * 100);

                            XBrush caseColor = currentCase.IsCritical ? XBrushes.LightSalmon : XBrushes.LightGray;
                            gfxCases.DrawRectangle(XPens.Black, caseColor, 
                                new XRect(50, verticalPosition - 20, casesPage.Width - 100, 80));

                            gfxCases.DrawString($"Empleado: {currentCase.Name}", contentFont, XBrushes.Black, 
                                new XPoint(70, verticalPosition));
                            gfxCases.DrawString($"Departamento: {currentCase.Department}", smallFont, XBrushes.Black, 
                                new XPoint(70, verticalPosition + 25));
                            gfxCases.DrawString($"Diagnóstico: {currentCase.Diagnosis}", smallFont, XBrushes.Black, 
                                new XPoint(70, verticalPosition + 40));
                            gfxCases.DrawString(
                                $"Días de Incapacidad: {currentCase.DaysOff} | " +
                                $"Estado: {(currentCase.IsCritical ? "CRÍTICO" : "ESTÁNDAR")}", 
                                smallFont, 
                                currentCase.IsCritical ? XBrushes.Red : XBrushes.DarkGreen, 
                                new XPoint(70, verticalPosition + 55)
                            );
                        }
                    }

                    document.Save(stream, false);
                    return File(stream.ToArray(), "application/pdf", "Informe_Incapacidades_2024.pdf");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF: {ex.Message}");
            }
        }
        [HttpGet("generateexcel")]
        public IActionResult GenerateDisabilityReportExcel()
        {
            try 
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Informe de Incapacidades");
                    Random random = new Random();
                    
                    // Data Pools
                    string[] firstNames = { 
                        "Juan", "Carlos", "María", "Ana", "Luis", "Pedro", "José", "Elena", 
                        "Diego", "Laura", "Andrea", "Manuel", "Sofía", "Javier", "Carmen", 
                        "Miguel", "Isabel", "Roberto", "Alejandro", "Patricia", "Daniel", 
                        "Martín", "Lucía", "Fernando", "Carolina", "David", "Gabriela"
                    };

                    string[] departments = { 
                        "Recursos Humanos", "Contabilidad", "Ventas", "Marketing", 
                        "Desarrollo de Software", "Soporte Técnico", "Logística", 
                        "Producción", "Investigación y Desarrollo", "Atención al Cliente", 
                        "Compras", "Control de Calidad", "Legales", "Administración", 
                        "Relaciones Públicas", "Finanzas", "Auditoría", "Innovación", 
                        "Operaciones", "Proyectos Especiales"
                    };
                    
                    string[] lastNames = { 
                        "Pérez", "González", "Rodríguez", "Martínez", "López", "Sánchez", 
                        "Ramírez", "Torres", "Díaz", "Ruiz", "Hernández", "Morales", "Jiménez", 
                        "Castillo", "Gómez", "Álvarez", "Rivera", "Fernández", "Navarro", 
                        "Méndez", "Castro", "Ortiz", "Muñoz"
                    };
                    
                    string[] diagnoses = { 
                        "Lumbalgia Crónica", "Tendinitis Severa", "Estrés Laboral Avanzado", 
                        "Recuperación Postquirúrgica", "Síndrome de Túnel Carpiano", 
                        "Lesión Muscular Compleja", "Fatiga Crónica", "Burnout", 
                        "Depresión Ocupacional", "Hernias Discales", "Problemas Respiratorios", 
                        "Trastorno de Ansiedad", "Migraña Crónica", "Epicondilitis", 
                        "Cervicalgia", "Síndrome del Manguito Rotador", "Lesión Deportiva"
                    };

                    // Headers
                    worksheet.Cell(1, 1).Value = "Empleado";
                    worksheet.Cell(1, 2).Value = "Departamento";
                    worksheet.Cell(1, 3).Value = "Diagnóstico";
                    worksheet.Cell(1, 4).Value = "Días de Incapacidad";
                    worksheet.Cell(1, 5).Value = "Estado";

                    // Style headers
                    var headerRange = worksheet.Range(1, 1, 1, 5);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Generate data
                    int totalCases = 54;
                    int totalDaysOff = 0;
                    var departmentCases = new Dictionary<string, int>();
                    var diagnosisCases = new Dictionary<string, int>();

                    for (int i = 0; i < totalCases; i++)
                    {
                        string name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
                        string department = departments[random.Next(departments.Length)];
                        string diagnosis = diagnoses[random.Next(diagnoses.Length)];
                        int daysOff = random.Next(5, 45);
                        bool isCritical = random.Next(100) < 15;

                        // Track statistics
                        totalDaysOff += daysOff;
                        departmentCases[department] = departmentCases.ContainsKey(department) ? departmentCases[department] + 1 : 1;
                        diagnosisCases[diagnosis] = diagnosisCases.ContainsKey(diagnosis) ? diagnosisCases[diagnosis] + 1 : 1;

                        // Write to Excel
                        worksheet.Cell(i + 2, 1).Value = name;
                        worksheet.Cell(i + 2, 2).Value = department;
                        worksheet.Cell(i + 2, 3).Value = diagnosis;
                        worksheet.Cell(i + 2, 4).Value = daysOff;
                        
                        var stateCell = worksheet.Cell(i + 2, 5);
                        stateCell.Value = isCritical ? "CRÍTICO" : "ESTÁNDAR";
                        stateCell.Style.Font.FontColor = isCritical ? XLColor.Red : XLColor.DarkGreen;
                    }

                    // Add summary sheet
                    var summarySheet = workbook.Worksheets.Add("Resumen");
                    summarySheet.Cell(1, 1).Value = "Total Casos de Incapacidad";
                    summarySheet.Cell(1, 2).Value = totalCases;
                    summarySheet.Cell(2, 1).Value = "Total Días de Reposo";
                    summarySheet.Cell(2, 2).Value = totalDaysOff;
                    summarySheet.Cell(3, 1).Value = "Promedio Días por Incapacidad";
                    summarySheet.Cell(3, 2).Value = Math.Round(totalDaysOff / (double)totalCases, 2);

                    // Top Departments
                    summarySheet.Cell(5, 1).Value = "Top 5 Departamentos";
                    summarySheet.Cell(5, 1).Style.Font.Bold = true;
                    var topDepartments = departmentCases.OrderByDescending(x => x.Value).Take(5);
                    int rowIndex = 6;
                    foreach (var dept in topDepartments)
                    {
                        summarySheet.Cell(rowIndex, 1).Value = dept.Key;
                        summarySheet.Cell(rowIndex, 2).Value = dept.Value;
                        rowIndex++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();
                    summarySheet.Columns().AdjustToContents();

                    // Save to memory stream
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Informe_Incapacidades_2024.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando Excel: {ex.Message}");
            }
        }
    }
}
