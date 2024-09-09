using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediClinic.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace MediClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorVisitsController : Controller
    {
        private readonly ClinicContext _context;
        private readonly ILogger<DoctorVisitsController> _logger;

        public DoctorVisitsController(ClinicContext context, ILogger<DoctorVisitsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: DoctorVisits
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.DoctorVisits.Include(d => d.Doctor);
            return View("~/Views/Admin/DoctorVisits/Index.cshtml", await clinicContext.ToListAsync());
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
            PopulateDoctorsDropDownList();
            return View("~/Views/Admin/DoctorVisits/Create.cshtml");
        }

        // POST: DoctorVisits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,VisitDate,StartTime,EndTime")] DoctorVisit doctorVisit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorVisit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                    _logger.LogError(error.ErrorMessage);
            }

            PopulateDoctorsDropDownList(doctorVisit.DoctorID);
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
            PopulateDoctorsDropDownList(doctorVisit.DoctorID);
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
            PopulateDoctorsDropDownList(doctorVisit.DoctorID);
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

        private void PopulateDoctorsDropDownList(object selectedDoctor = null)
        {
            var doctorsQuery = from d in _context.Doctors
                               orderby d.FullName
                               select d;
            ViewBag.DoctorID = new SelectList(doctorsQuery.AsNoTracking(), "DoctorID", "FullName", selectedDoctor);
        }
    }
}
