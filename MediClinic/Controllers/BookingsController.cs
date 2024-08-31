﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediClinic.Data;

namespace MediClinic.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ClinicContext _context;

        public BookingsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Bookings.Include(b => b.DoctorVisit).Include(b => b.Patient);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.DoctorVisit)
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["VisitID"] = new SelectList(_context.DoctorVisits, "DoctorID", "DoctorID");
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,PatientID,VisitID,BookingDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VisitID"] = new SelectList(_context.DoctorVisits, "DoctorID", "DoctorID", booking.VisitID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", booking.PatientID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["VisitID"] = new SelectList(_context.DoctorVisits, "DoctorID", "DoctorID", booking.VisitID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", booking.PatientID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,PatientID,VisitID,BookingDate")] Booking booking)
        {
            if (id != booking.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.PatientID))
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
            ViewData["VisitID"] = new SelectList(_context.DoctorVisits, "DoctorID", "DoctorID", booking.VisitID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", booking.PatientID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.DoctorVisit)
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.PatientID == id);
        }
    }
}
