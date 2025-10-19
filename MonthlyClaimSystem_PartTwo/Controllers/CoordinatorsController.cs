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
    public class CoordinatorsController : Controller
    {
        private readonly MonthlyClaimSystem_PartTwoContext _context;

        public CoordinatorsController(MonthlyClaimSystem_PartTwoContext context)
        {
            _context = context;
        }

        // GET: Coordinators
        public async Task<IActionResult> Index()
        {
            var monthlyClaimSystem_PartTwoContext = _context.Coordinator.Include(c => c.Lecturer);
            return View(await monthlyClaimSystem_PartTwoContext.ToListAsync());
        }

        // GET: Coordinators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await _context.Coordinator
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.CoordinatorId == id);
            if (coordinator == null)
            {
                return NotFound();
            }

            return View(coordinator);
        }

        // GET: Coordinators/Create
        public IActionResult Create()
        {
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId");
            return View();
        }

        // POST: Coordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoordinatorId,ClaimId")] Coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", coordinator.ClaimId);
            return View(coordinator);
        }

        // GET: Coordinators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await _context.Coordinator.FindAsync(id);
            if (coordinator == null)
            {
                return NotFound();
            }
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", coordinator.ClaimId);
            return View(coordinator);
        }

        // POST: Coordinators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoordinatorId,ClaimId")] Coordinator coordinator)
        {
            if (id != coordinator.CoordinatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinatorExists(coordinator.CoordinatorId))
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
            ViewData["ClaimId"] = new SelectList(_context.Lecturer, "ClaimId", "ClaimId", coordinator.ClaimId);
            return View(coordinator);
        }

        // GET: Coordinators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await _context.Coordinator
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.CoordinatorId == id);
            if (coordinator == null)
            {
                return NotFound();
            }

            return View(coordinator);
        }

        // POST: Coordinators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinator = await _context.Coordinator.FindAsync(id);
            if (coordinator != null)
            {
                _context.Coordinator.Remove(coordinator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinatorExists(int id)
        {
            return _context.Coordinator.Any(e => e.CoordinatorId == id);
        }
    }
}
