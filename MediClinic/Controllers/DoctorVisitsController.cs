using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediClinic.Data;

namespace MediClinic.Controllers
{
    public class DoctorVisitsController : Controller
    {
        private readonly ClinicContext _context;

        public DoctorVisitsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: DoctorVisits
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.DoctorVisits.Include(d => d.Doctor);
            var doctorVisits = await clinicContext.ToListAsync();
            return View("~/Views/Admin/DoctorVisits/Index.cshtml", doctorVisits);
        }

        // GET: DoctorVisits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorVisit = await _context.DoctorVisits
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctorVisit == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/DoctorVisits/Details.cshtml", doctorVisit);
        }

        // GET: DoctorVisits/Create
        public IActionResult Create()
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FullName");
            return View("~/Views/Admin/DoctorVisits/Create.cshtml");
        }

        // POST: DoctorVisits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitID,DoctorID,VisitDate,StartTime,EndTime")] DoctorVisit doctorVisit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorVisit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FullName", doctorVisit.DoctorID);
            return View("~/Views/Admin/DoctorVisits/Create.cshtml", doctorVisit);
        }

        // GET: DoctorVisits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorVisit = await _context.DoctorVisits.FindAsync(id);
            if (doctorVisit == null)
            {
                return NotFound();
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FullName", doctorVisit.DoctorID);
            return View("~/Views/Admin/DoctorVisits/Edit.cshtml", doctorVisit);
        }

        // POST: DoctorVisits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitID,DoctorID,VisitDate,StartTime,EndTime")] DoctorVisit doctorVisit)
        {
            if (id != doctorVisit.DoctorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorVisit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorVisitExists(doctorVisit.DoctorID))
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
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FullName", doctorVisit.DoctorID);
            return View("~/Views/Admin/DoctorVisits/Edit.cshtml", doctorVisit);
        }

        // GET: DoctorVisits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorVisit = await _context.DoctorVisits
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctorVisit == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/DoctorVisits/Delete.cshtml", doctorVisit);
        }

        // POST: DoctorVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorVisit = await _context.DoctorVisits.FindAsync(id);
            if (doctorVisit != null)
            {
                _context.DoctorVisits.Remove(doctorVisit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorVisitExists(int id)
        {
            return _context.DoctorVisits.Any(e => e.DoctorID == id);
        }
    }
}
