using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem;
using PatientManagementSystem.Models;

namespace PatientManagementSystem.Controllers
{
    public class LabBillingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabBillingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LabBillings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LabBillings.Include(l => l.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LabBillings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabBillings == null)
            {
                return NotFound();
            }

            var labBilling = await _context.LabBillings
                .Include(l => l.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labBilling == null)
            {
                return NotFound();
            }

            return View(labBilling);
        }

        // GET: LabBillings/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: LabBillings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BillingDate,Amount,PatientId")] LabBilling labBilling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labBilling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", labBilling.PatientId);
            return View(labBilling);
        }

        // GET: LabBillings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabBillings == null)
            {
                return NotFound();
            }

            var labBilling = await _context.LabBillings.FindAsync(id);
            if (labBilling == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", labBilling.PatientId);
            return View(labBilling);
        }

        // POST: LabBillings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BillingDate,Amount,PatientId")] LabBilling labBilling)
        {
            if (id != labBilling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labBilling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabBillingExists(labBilling.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", labBilling.PatientId);
            return View(labBilling);
        }

        // GET: LabBillings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabBillings == null)
            {
                return NotFound();
            }

            var labBilling = await _context.LabBillings
                .Include(l => l.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labBilling == null)
            {
                return NotFound();
            }

            return View(labBilling);
        }

        // POST: LabBillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LabBillings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LabBillings'  is null.");
            }
            var labBilling = await _context.LabBillings.FindAsync(id);
            if (labBilling != null)
            {
                _context.LabBillings.Remove(labBilling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabBillingExists(int id)
        {
          return _context.LabBillings.Any(e => e.Id == id);
        }
    }
}
