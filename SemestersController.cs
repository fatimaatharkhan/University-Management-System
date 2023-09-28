using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flex.Data;
using Flex.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace Flex.Controllers
{
    public class SemestersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemestersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Semesters
        public async Task<IActionResult> Index()
        {
            return _context.Semester != null ?
                        View(await _context.Semester.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Semester'  is null.");
        }

        // GET: Semesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Semester == null)
            {
                return NotFound();
            }

            var semester = await _context.Semester
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // GET: Semesters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Semesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,startDate,endDate")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semester);
                await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                return RedirectToAction(nameof(Index));
            }
            return View(semester);
        }

        // GET: Semesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Semester == null)
            {
                return NotFound();
            }

            var semester = await _context.Semester.Include(x => x.SemesterCourses).ThenInclude(x => x.Course).ThenInclude(x => x.PreReqs).FirstOrDefaultAsync(x => x.Id == id);
            var semesterCourses = _context.SemesterCourses.Include(x => x.Course).Where(x => x.SemId == id).Select(x => new { courseID = x.SCourseId, courseName = x.Course.courseName}).ToList(); 
            var courses = _context.Course.Select(X => new { X.courseID, X.courseName }).ToList();
            ViewBag.Semester = courses.Except(semesterCourses).ToList(); //.ConvertAll(a => new SelectListItem { Text = a.courseName, Value = a.courseID.ToString(), Selected = false });
            if (semester == null)
            {
                return NotFound();
            }
            return View(semester);
        }

        //[Route("/[controller]/AddCourse/{id}/{semId}")]
        [HttpGet("[controller]/AddCourse/{idT}/{semId}")]
        public async Task<IActionResult> AddCourse(int idT, int semId)
        {
            Console.WriteLine(idT);
            if (_context.Semester == null)
            {
                return NotFound();
            }

            var semesterCourse = new SemesterCourses { SemId = semId, SCourseId = idT };
            if (await _context.SemesterCourses.AnyAsync(x => x.SemId == semId && x.SCourseId == idT))
                ViewBag.Msg = "This course is already added.";

            await _context.SemesterCourses.AddAsync(semesterCourse);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);


            return RedirectToAction("Edit", new { id = semId});
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,startDate,endDate")] Semester semester)
        {
            if (id != semester.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semester);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterExists(semester.Id))
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
            return View(semester);
        }

        // GET: Semesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Semester == null)
            {
                return NotFound();
            }

            var semester = await _context.Semester
                .FirstOrDefaultAsync(m => m.Id == id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Semester == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Semester'  is null.");
            }
            var semester = await _context.Semester.FindAsync(id);
            if (semester != null)
            {
                _context.Semester.Remove(semester);
            }
            
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));
        }

        private bool SemesterExists(int id)
        {
          return (_context.Semester?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
