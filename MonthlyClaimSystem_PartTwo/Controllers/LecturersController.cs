using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MonthlyClaimSystem_PartTwo.Data;
using MonthlyClaimSystem_PartTwo.Models;
using MonthlyClaimSystem_PartTwo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyClaimSystem_PartTwo.Controllers
{
    public class LecturersController : Controller
    {
        public readonly MonthlyClaimSystem_PartTwoContext _context;
        public readonly IWebHostEnvironment _environment;
        public readonly FileEncryptionService _encryptionService;
        public static List<Lecturer> _claims = new List<Lecturer>();

        public LecturersController(MonthlyClaimSystem_PartTwoContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _encryptionService = new FileEncryptionService();
        }

        // GET: Lecturers
        public async Task<IActionResult> Index()
        {
            try
            {

                var books = await _context.Lecturer.ToListAsync();
                return View(books);
            }
            catch (Exception ex)
            {

                TempData["Error"] = "Unable to load books at this time. Please try again later.";
                return View(new List<Lecturer>());
            }
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = _context.Lecturer.Include(l => l.UploadedFiles).FirstOrDefault(l => l.ClaimId == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimId,LecturerRefID,FirstName,LastName,Faculty,ClaimName,Amount,StartDate,EndDate,HoursWorked,HourlyRate,Description,Email,ContactNum")] Lecturer lecturer, List<IFormFile> documents)
        {
            try
            {

                if (documents != null && documents.Count > 0)
                {
                    foreach (var file in documents)
                    {
                        if (file.Length > 0)
                        {
                            var allowedExtensions = new[] { ".pdf", ".docx", ".txt", ".xlsx" };
                            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                            if (!allowedExtensions.Contains(extension))
                            {
                                ViewBag.Error = $"File extension {extension} not allowed";
                                return View(lecturer);
                            }

                            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                            Directory.CreateDirectory(uploadsFolder);

                            var uniqueFileName = Guid.NewGuid().ToString() + ".encrypted";
                            var encryptedFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = file.OpenReadStream())
                            {
                                await _encryptionService.EncryptFileAsync(fileStream, encryptedFilePath);
                            }

                            lecturer.UploadedFiles.Add(new FileModel
                            {
                                FileName = file.FileName,
                                FilePath = "/uploads/" + uniqueFileName,
                                FileSize = file.Length,
                                IsEncrypted = true
                            });

                        }
                    }
                }
                lecturer.SubmittedDate = DateTime.Now;
                lecturer.Status = ClaimStatus.Pending;
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Successfully added new claim!";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                //viewbag is a dictionary
                ViewBag.Error = "Error adding claim. " + ex.Message;
                return View(lecturer);
            }

        }

        public async Task<IActionResult> DownloadDocument(int claimId, int docId)
        {
            try
            {
                var claim = await _context.Lecturer.Include(c => c.UploadedFiles).FirstOrDefaultAsync(m => m.ClaimId == claimId);
                if (claim == null) { return NotFound("Claim not found."); }

                var document = claim.UploadedFiles.FirstOrDefault(doc => doc.Id == docId);
                if (document == null) { return NotFound("Document not found."); }

                var encryptedFilePath = Path.Combine(_environment.WebRootPath, document.FilePath.TrimStart('/'));
                if (!System.IO.File.Exists(encryptedFilePath)) return NotFound("File not found;");

                var decryptedStream = await _encryptionService.DecryptFileAsync(encryptedFilePath);

                var contentType = Path.GetExtension(document.FileName).ToLower()
                    switch
                {
                    ".pdf" => "application/pdf",
                    ".txt" => "application/txt",
                    ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    _ => "application/octet-stream"
                };

                return File(decryptedStream, contentType, document.FileName);

            }
            catch (Exception ex)
            {
                return BadRequest("Error downloading file: " + ex.Message);
            }
        }


    
    public static int GetPendingCount() => _claims.Count(b => b.Status == ClaimStatus.Pending);

        public static int GetApprovedCount() => _claims.Count(b => b.Status == ClaimStatus.Approved);

        public static int GetDeclinedCount() => _claims.Count(b => b.Status == ClaimStatus.Declined);

        public static int GetVerifiedCount() => _claims.Count(b => b.Status == ClaimStatus.Verified);
    }
}