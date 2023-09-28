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
    public class FacultySectionSemesterCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultySectionSemesterCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FacultySectionSemesterCourses
        public async Task<IActionResult> Index()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _context.FacultySectionSemesterCourses != null ?
                          View(await _context.FacultySectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).ThenInclude(x => x.PreReqs).Where(x => x.SemCourses.SemId == semester.Id && x.loginID == identifier.Value).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FacultySectionSemesterCourses'  is null.");
        }

        public async Task<IActionResult> OfficerIndex()
        {
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _context.FacultySectionSemesterCourses != null ?
                          View(await _context.FacultySectionSemesterCourses.Include(x => x.Instructor).Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).ThenInclude(x => x.PreReqs).Where(x => x.SemCourses.SemId == semester.Id && x.SectionId == null).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FacultySectionSemesterCourses'  is null.");
        }

        public async Task<IActionResult> AssignSections(int? id)
        {
            var faculty = await _context.FacultySectionSemesterCourses.FirstOrDefaultAsync(x => x.Id== id);
            
            var allSections = await _context.SectionSemesterCourses.Include(x => x.SemCourses).Where(x => x.SemesterCourseId == faculty.SemesterCourseId).Select(x => new {x.Id, x.Name}).ToListAsync();
            Console.WriteLine(allSections);
            ViewBag.ListOfSections = allSections;
            return View(faculty);
        }

        [HttpGet("[controller]/AssignSection/{idT}/{facultyId}")]
        public async Task<IActionResult> AssignSection(int idT, int facultyId)
        {
            Console.WriteLine(idT);
            if (_context.FacultySectionSemesterCourses == null)
            {
                return NotFound();
            }

            var faculty = await _context.FacultySectionSemesterCourses.FirstOrDefaultAsync(x => x.Id == facultyId);
            faculty.SectionId = idT;
            _context.FacultySectionSemesterCourses.Update(faculty);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);


            return RedirectToAction("OfficerIndex");
        }


        public async Task<IActionResult> ViewStudents()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SectionSemesterCourse).ThenInclude(x => x.StudentSectionSemesterCourses).ThenInclude(x => x.Student).Where(x => x.loginID == identifier.Value && x.SectionId != null).ToListAsync();
            var sections = faculty.Select(x => new { Id = x.SectionId, Name = x.SectionId + " " + x.SectionSemesterCourse?.Name });

            ViewBag.ListOfSections = sections;
            return View(faculty.FirstOrDefault());
        }

        public async Task<IActionResult> ViewStudents1(int id)
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SectionSemesterCourse).ThenInclude(x => x.StudentSectionSemesterCourses).ThenInclude(x => x.Student).Where(x => x.loginID == identifier.Value && x.SectionId != null).ToListAsync();
            var sections = faculty.Select(x => new { Id = x.SectionId, Name = x.SectionId + " " + x.SectionSemesterCourse?.Name });

            ViewBag.ListOfSections = sections;
            return View("ViewStudents", faculty.FirstOrDefault(x => x.SectionId == id));
        }

        public async Task<IActionResult> ViewStudentsOfficer()
        {
            //var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SectionSemesterCourse).ThenInclude(x => x.StudentSectionSemesterCourses).ThenInclude(x => x.Student).Where(x => x.SectionId != null).ToListAsync();
            var sections = faculty.Select(x => new { Id = x.SectionId, Name = x.SectionId + " " + x.SectionSemesterCourse?.Name });

            ViewBag.ListOfSections = sections;
            return View(faculty.FirstOrDefault());
        }

        public async Task<IActionResult> ViewStudentsOfficer1(int id)
        {
            //var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SectionSemesterCourse).ThenInclude(x => x.StudentSectionSemesterCourses).ThenInclude(x => x.Student).Where(x => x.SectionId != null).ToListAsync();
            var sections = faculty.Select(x => new { Id = x.SectionId, Name = x.SectionId + " " + x.SectionSemesterCourse?.Name });

            ViewBag.ListOfSections = sections;
            return View("ViewStudentsOfficer", faculty.FirstOrDefault(x => x.SectionId == id));
        }

        public async Task<IActionResult> ViewAllocatedCourses(int id)
        {
            //var allCourses = await _context.SectionSemesterCourses.Include(x => x.FacultSection).ThenInclude(x => x.Instructor).ToListAsync();
            var allCourses = await _context.SemesterCourses.Include(x => x.SectionSemesterCourses).ThenInclude(x => x.FacultSection).ThenInclude(x => x.Instructor).Include(x => x.Course).Include(x => x.semester).ToListAsync();
            return View(allCourses);
        }

            public async Task<IActionResult> SetWeightage()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var faculty = await _context.FacultySectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.Course).Include(x => x.SectionSemesterCourse).Where(x => x.loginID == identifier.Value && x.SectionId != null).ToListAsync();
            ViewBag.ListOfSections = faculty;
            return View(faculty);
        }

        public async Task<IActionResult> EditWeightage(int id)
        {
            var faculty = await _context.FacultySectionSemesterCourses.FirstOrDefaultAsync(x => x.Id == id);
            return View(faculty);
        }

        public async Task<IActionResult> EditWeightage1(FacultySectionSemesterCourses facultySectionSemesterCourses)
        {

            _context.FacultySectionSemesterCourses.Update(facultySectionSemesterCourses);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction("Faculty", "Home");

        }

        public async Task<IActionResult> EditMarks(int id)
        {
            var student = await _context.StudentSectionSemesterCourses.FirstOrDefaultAsync(x => x.Id == id);
            return View(student);
        }

        public async Task<IActionResult> EditMarks1(StudentSectionSemesterCourses studentSectionSemesterCourses)
        {

            _context.StudentSectionSemesterCourses.Update(studentSectionSemesterCourses);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction("ViewStudents", "FacultySectionSemesterCourses");

        }


        // GET: FacultySectionSemesterCourses/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.FacultySectionSemesterCourses == null)
        //    {
        //        return NotFound();
        //    }

        //    var facultySectionSemesterCourses = await _context.FacultySectionSemesterCourses
        //        .Include(f => f.SectionSemesterCourse)
        //        .Include(f => f.SemCourses)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (facultySectionSemesterCourses == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(facultySectionSemesterCourses);
        //}

        // GET: FacultySectionSemesterCourses/Create
        public async Task<IActionResult> Create()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            var facultyCourses = await _context.FacultySectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).Where(x => x.loginID == identifier.Value && x.SemCourses.SemId == semester.Id).ToListAsync();
            var selectedCourses = facultyCourses.Select(x => new { courseID = x.SemCourses.SCourseId, courseName = x.SemCourses.Course.courseName });
            var allCourses = await _context.SemesterCourses.Where(x => x.SemId == semester.Id).Select(x => new { courseID = x.Course.courseID, courseName = x.Course.courseName }).ToListAsync();
            ViewBag.ListOfSemesterCourses = allCourses.Except(selectedCourses).ToList();
            return View();
        }

        // POST: FacultySectionSemesterCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SemesterCourseId,loginID,SectionId")] FacultySectionSemesterCourses facultySectionSemesterCourses)
        {
            if (facultySectionSemesterCourses.SemesterCourseId != null && facultySectionSemesterCourses.SemesterCourseId != 0)
            {

                var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
                //var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
                var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();  
                var semesterCourse = await _context.SemesterCourses.FirstOrDefaultAsync(x => x.SemId == semester.Id && x.SCourseId == facultySectionSemesterCourses.SemesterCourseId);
                facultySectionSemesterCourses.SemesterCourseId = semesterCourse.Id;
                if ((await ValidateCourseLimit(identifier, facultySectionSemesterCourses.SemesterCourseId)))
                {
                    var studsemesterCourse = new FacultySectionSemesterCourses { SemesterCourseId = facultySectionSemesterCourses.SemesterCourseId, loginID = identifier.Value};
                    //if (await _context.StudentSemesterCourses.AnyAsync(x => x.SemCourses. == Sem))
                    _context.Add(studsemesterCourse);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return RedirectToAction(nameof(Index));
                }
                else
                    ViewBag.Msg = "This course is already added.";
            }
            return View();
        }

        private async Task<bool> ValidateCourseLimit(Claim? identifier, int? semesterCourseId)
        {
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            var semesterCourses = _context.SemesterCourses.Where(x => x.SemId == semester.Id);
            var courses = await _context.FacultySectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.Course).Where(x => x.loginID == identifier.Value && x.SemCourses.SemId == semester.Id).ToListAsync();
            var count = courses.Count();
            //var creditHours = courses.Sum(x => x.SemCourses.Course.creditHours);
            Console.Write(count);
            if (count < 3)
                return true;
            else
                return false;
        }



        // GET: FacultySectionSemesterCourses/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.FacultySectionSemesterCourses == null)
        //    {
        //        return NotFound();
        //    }

        //    var facultySectionSemesterCourses = await _context.FacultySectionSemesterCourses.FindAsync(id);
        //    if (facultySectionSemesterCourses == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["SectionId"] = new SelectList(_context.SectionSemesterCourses, "Id", "Id", facultySectionSemesterCourses.SectionId);
        //    ViewData["SemesterCourseId"] = new SelectList(_context.SemesterCourses, "Id", "Id", facultySectionSemesterCourses.SemesterCourseId);
        //    return View(facultySectionSemesterCourses);
        //}

        //// POST: FacultySectionSemesterCourses/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,SemesterCourseId,loginID,SectionId")] FacultySectionSemesterCourses facultySectionSemesterCourses)
        //{
        //    if (id != facultySectionSemesterCourses.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(facultySectionSemesterCourses);
        //            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FacultySectionSemesterCoursesExists(facultySectionSemesterCourses.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SectionId"] = new SelectList(_context.SectionSemesterCourses, "Id", "Id", facultySectionSemesterCourses.SectionId);
        //    ViewData["SemesterCourseId"] = new SelectList(_context.SemesterCourses, "Id", "Id", facultySectionSemesterCourses.SemesterCourseId);
        //    return View(facultySectionSemesterCourses);
        //}

        // GET: FacultySectionSemesterCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacultySectionSemesterCourses == null)
            {
                return NotFound();
            }

            var facultySectionSemesterCourses = await _context.FacultySectionSemesterCourses
                .Include(f => f.SectionSemesterCourse)
                .Include(f => f.SemCourses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultySectionSemesterCourses == null)
            {
                return NotFound();
            }

            return View(facultySectionSemesterCourses);
        }

        // POST: FacultySectionSemesterCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacultySectionSemesterCourses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FacultySectionSemesterCourses'  is null.");
            }
            var facultySectionSemesterCourses = await _context.FacultySectionSemesterCourses.FindAsync(id);
            if (facultySectionSemesterCourses != null)
            {
                _context.FacultySectionSemesterCourses.Remove(facultySectionSemesterCourses);
            }
            
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));
        }

        private bool FacultySectionSemesterCoursesExists(int id)
        {
          return (_context.FacultySectionSemesterCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
