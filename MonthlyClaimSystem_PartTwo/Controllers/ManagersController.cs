using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonthlyClaimSystem_PartTwo.Data;
using MonthlyClaimSystem_PartTwo.Models;

namespace MonthlyClaimSystem_PartTwo.Controllers
{
    public class ManagersController : Controller
    {
        private readonly MonthlyClaimSystem_PartTwoContext _context;

        public ManagersController(MonthlyClaimSystem_PartTwoContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var monthlyClaimSystem_PartTwoContext = _context.Manager.Include(m => m.Lecturer);
            return View(await monthlyClaimSystem_PartTwoContext.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .Include(m => m.Lecturer)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,ClaimId")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", manager.ClaimId);
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", manager.ClaimId);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,ClaimId")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", manager.ClaimId);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .Include(m => m.Lecturer)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Manager.FindAsync(id);
            if (manager != null)
            {
                _context.Manager.Remove(manager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Manager.Any(e => e.ManagerId == id);
        }
    }
}
