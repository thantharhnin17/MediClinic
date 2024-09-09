using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediClinic.Data;
using Microsoft.AspNetCore.Authorization;

namespace MediClinic.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ClinicContext _context;

        public AppointmentsController(ClinicContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Admin")]
        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Appointments.Include(a => a.DoctorVisit).Include(a => a.Patient);
            var appointments = await clinicContext.ToListAsync();
            return View("~/Views/Admin/Appointments/Index.cshtml", appointments);
        }


        [Authorize(Roles = "Admin")]
        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.DoctorVisit)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Appointments/Details.cshtml", appointment);
        }

        [Authorize]
        // GET: Appointments/Create
        public IActionResult Create()
        {
            var doctorVisits = from visit in _context.DoctorVisits
                               join doctor in _context.Doctors
                               on visit.DoctorID equals doctor.DoctorID
                               select new
                               {
                                   visit.VisitID,
                                   DoctorName = doctor.FullName
                               };

            ViewData["VisitID"] = new SelectList(doctorVisits, "VisitID", "DoctorName");
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName");
            return View("~/Views/Admin/Appointments/Create.cshtml");
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,PatientID,VisitID,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var doctorVisits = from visit in _context.DoctorVisits
                               join doctor in _context.Doctors
                               on visit.DoctorID equals doctor.DoctorID
                               select new
                               {
                                   visit.VisitID,
                                   DoctorName = doctor.FullName
                               };

            ViewData["VisitID"] = new SelectList(doctorVisits, "VisitID", "DoctorName");
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", appointment.PatientID);
            return View("~/Views/Admin/Appointments/Create.cshtml", appointment);
        }


        [Authorize(Roles = "Admin")]
        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var doctorVisits = from visit in _context.DoctorVisits
                               join doctor in _context.Doctors
                               on visit.DoctorID equals doctor.DoctorID
                               select new
                               {
                                   visit.VisitID,
                                   DoctorName = doctor.FullName
                               };

            ViewData["VisitID"] = new SelectList(doctorVisits, "VisitID", "DoctorName");
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", appointment.PatientID);
            return View("~/Views/Admin/Appointments/Edit.cshtml", appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentID,PatientID,VisitID,AppointmentDate")] Appointment appointment)
        {
            if (id != appointment.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.PatientID))
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
            var doctorVisits = from visit in _context.DoctorVisits
                               join doctor in _context.Doctors
                               on visit.DoctorID equals doctor.DoctorID
                               select new
                               {
                                   visit.VisitID,
                                   DoctorName = doctor.FullName
                               };

            ViewData["VisitID"] = new SelectList(doctorVisits, "VisitID", "DoctorName");
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", appointment.PatientID);
            return View("~/Views/Admin/Appointments/Edit.cshtml", appointment);
        }


        [Authorize(Roles = "Admin")]
        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.DoctorVisit)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Appointments/Delete.cshtml", appointment);
        }


        [Authorize(Roles = "Admin")]
        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.PatientID == id);
        }
    }
}
