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
    public class PharmacyBillingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacyBillingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PharmacyBillings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PharmacyBillings.Include(p => p.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PharmacyBillings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PharmacyBillings == null)
            {
                return NotFound();
            }

            var pharmacyBilling = await _context.PharmacyBillings
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacyBilling == null)
            {
                return NotFound();
            }

            return View(pharmacyBilling);
        }

        // GET: PharmacyBillings/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: PharmacyBillings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BillingDate,Amount,PatientId")] PharmacyBilling pharmacyBilling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacyBilling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", pharmacyBilling.PatientId);
            return View(pharmacyBilling);
        }

        // GET: PharmacyBillings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PharmacyBillings == null)
            {
                return NotFound();
            }

            var pharmacyBilling = await _context.PharmacyBillings.FindAsync(id);
            if (pharmacyBilling == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", pharmacyBilling.PatientId);
            return View(pharmacyBilling);
        }

        // POST: PharmacyBillings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BillingDate,Amount,PatientId")] PharmacyBilling pharmacyBilling)
        {
            if (id != pharmacyBilling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacyBilling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyBillingExists(pharmacyBilling.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", pharmacyBilling.PatientId);
            return View(pharmacyBilling);
        }

        // GET: PharmacyBillings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PharmacyBillings == null)
            {
                return NotFound();
            }

            var pharmacyBilling = await _context.PharmacyBillings
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacyBilling == null)
            {
                return NotFound();
            }

            return View(pharmacyBilling);
        }

        // POST: PharmacyBillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PharmacyBillings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PharmacyBillings'  is null.");
            }
            var pharmacyBilling = await _context.PharmacyBillings.FindAsync(id);
            if (pharmacyBilling != null)
            {
                _context.PharmacyBillings.Remove(pharmacyBilling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyBillingExists(int id)
        {
          return _context.PharmacyBillings.Any(e => e.Id == id);
        }
    }
}
