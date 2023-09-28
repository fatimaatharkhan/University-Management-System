using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flex.Data;
using Flex.Models;
using System.Security.Claims;

namespace Flex.Controllers
{
    public class SectionAttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionAttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SectionAttendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SectionAttendance.Include(s => s.sectionInfo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SectionAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SectionAttendance == null)
            {
                return NotFound();
            }

            var sectionAttendance = await _context.SectionAttendance
                .Include(s => s.sectionInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectionAttendance == null)
            {
                return NotFound();
            }

            return View(sectionAttendance);
        }

        // GET: SectionAttendances/Create
        public async Task<IActionResult> Create()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SectionSemesterCourse).Where(x => x.loginID == identifier.Value && x.SectionId != null).ToListAsync();
            var sections = faculty.Select(x => new { Id = x.SectionId, Name = x.SectionId + " " + x.SectionSemesterCourse?.Name });
            ViewBag.ListOfSections = sections;
            return View();
        }

        // POST: SectionAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectionId,Date")] SectionAttendance sectionAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionAttendance);

                var students = await _context.StudentSectionSemesterCourses.Where(x => x.SectionId == sectionAttendance.SectionId).ToListAsync();
                sectionAttendance.Attendances = new List<StudentSectionAttendance>();
                foreach (var student in students)
                {
                    var newStudenAttendance = new StudentSectionAttendance();
                    newStudenAttendance.present = false;
                    newStudenAttendance.StudSecSemCourseId = student.Id;
                    sectionAttendance.Attendances.Add(newStudenAttendance);

                }

                _context.Add(sectionAttendance);
                await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.SectionSemesterCourses, "Id", "Id", sectionAttendance.SectionId);
            return View(sectionAttendance);
        }

        // GET: SectionAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SectionAttendance == null)
            {
                return NotFound();
            }

            var sectionAttendance = await _context.SectionAttendance
                                    .Include(x => x.Attendances)
                                        .ThenInclude(x => x.studentSecSemCourses)
                                        .ThenInclude(x => x.Student)
                                    .Include(x => x.sectionInfo)
                                        .ThenInclude(x => x.SemCourses)
                                        .ThenInclude(x => x.Course)
                                    .FirstOrDefaultAsync(x=> x.Id == id);
            
            if (sectionAttendance == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.SectionSemesterCourses, "Id", "Id", sectionAttendance.SectionId);
            return View(sectionAttendance);
        }

        public async Task<IActionResult> markAttendance(int? id)
        {
            var studentAttendance = await _context.StudentSectionAttendance.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (studentAttendance.present == false)
            {
                studentAttendance.present = true;
            }
            else
            {
                studentAttendance.present = false;
            }
            _context.Update(studentAttendance);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction("Edit", new { id = studentAttendance.SectionAttendanceId });
        }

        // POST: SectionAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionId,Date")] SectionAttendance sectionAttendance)
        {
            if (id != sectionAttendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionAttendance);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionAttendanceExists(sectionAttendance.Id))
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
            ViewData["SectionId"] = new SelectList(_context.SectionSemesterCourses, "Id", "Id", sectionAttendance.SectionId);
            return View(sectionAttendance);
        }

        // GET: SectionAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SectionAttendance == null)
            {
                return NotFound();
            }

            var sectionAttendance = await _context.SectionAttendance
                .Include(s => s.sectionInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectionAttendance == null)
            {
                return NotFound();
            }

            return View(sectionAttendance);
        }

        // POST: SectionAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SectionAttendance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SectionAttendance'  is null.");
            }
            var sectionAttendance = await _context.SectionAttendance.FindAsync(id);
            if (sectionAttendance != null)
            {
                _context.SectionAttendance.Remove(sectionAttendance);
            }
            
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));
        }

        private bool SectionAttendanceExists(int id)
        {
          return (_context.SectionAttendance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
