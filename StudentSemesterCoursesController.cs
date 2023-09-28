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
using static System.Collections.Specialized.BitVector32;

namespace Flex.Controllers
{
    public class StudentSemesterCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentSemesterCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentSemesterCourses
        public async Task<IActionResult> Index()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _context.StudentSectionSemesterCourses != null ? 
                          View(await _context.StudentSectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).ThenInclude(x => x.PreReqs).Where(x => x.SemCourses.SemId == semester.Id && x.loginID == identifier.Value).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StudentSectionSemesterCourses'  is null.");
        }



        public async Task<IActionResult> OfficerIndex()
        {
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            return _context.StudentSectionSemesterCourses != null ?
                          View(await _context.StudentSectionSemesterCourses.Include(x => x.Student).Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).ThenInclude(x => x.PreReqs).Where(x => x.SemCourses.SemId == semester.Id).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StudentSectionSemesterCourses'  is null.");
        }

        public async Task<IActionResult> OfficerIndex2(int? id)
        {
            SectionSemesterCourses section = null;
            var studentSemCourse = await _context.StudentSectionSemesterCourses.FirstOrDefaultAsync(x => x.Id== id);
            if (studentSemCourse == null)
                return RedirectToAction("OfficerIndex");


            section = await _context.SectionSemesterCourses.OrderByDescending(m => m.Id).FirstOrDefaultAsync(x => x.SemesterCourseId == studentSemCourse.SemesterCourseId);
            if (section == null)
            {
                section = new SectionSemesterCourses { SemesterCourseId = (int)studentSemCourse.SemesterCourseId, Name = "A", Description = "A" };
                await _context.SectionSemesterCourses.AddAsync(section);
                await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
               
            }
            else
            {
                var totalStudents = await _context.StudentSectionSemesterCourses.CountAsync(x => x.SectionId == section.Id);
                if (totalStudents >= 2)
                {
                    var sectionsCount = await _context.SectionSemesterCourses.CountAsync(x => x.SemesterCourseId == studentSemCourse.SemesterCourseId);
                    section = new SectionSemesterCourses { SemesterCourseId = (int)studentSemCourse.SemesterCourseId, Name = section.Name + sectionsCount, Description = section.Description + sectionsCount };
                    await _context.SectionSemesterCourses.AddAsync(section);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);

                }

            }

            studentSemCourse.SectionId = section.Id;
            studentSemCourse.isApproved = true;
            _context.StudentSectionSemesterCourses.Update(studentSemCourse);
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction("OfficerIndex");
        }

        public async Task<IActionResult> Attendance(int? id)
        {
            var studentSemCourse = await _context.StudentSectionSemesterCourses.Include(x => x.StudentSectionAttendances).ThenInclude(x => x.sectionAttendance).FirstOrDefaultAsync(x => x.Id == id);
            return View(studentSemCourse);
        }

        public async Task<IActionResult> ViewMarks(int? id)
        {
            var studentSemCourse = await _context.StudentSectionSemesterCourses.FirstOrDefaultAsync(x => x.Id == id);
            return View(studentSemCourse);
        }

            // GET: StudentSemesterCourses/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentSectionSemesterCourses == null)
            {
                return NotFound();
            }

            var studentSemesterCourses = await _context.StudentSectionSemesterCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSemesterCourses == null)
            {
                return NotFound();
            }

            return View(studentSemesterCourses);
        }

        // GET: StudentSemesterCourses/Create
        public async Task<IActionResult> Create()
        {
            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            //var semesterCourses = _context.StudentSemesterCourses.Include(x => x.SemCourses).Where(x => x.SemesterCourseId == ).Select(x => new { courseID = x.SCourseId, courseName = x.Course.courseName }).ToList();
            var studentCourses = await _context.StudentSectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.semester).Include(x => x.SemCourses).ThenInclude(x => x.Course).Where(x => x.loginID == identifier.Value && x.SemCourses.SemId == semester.Id).ToListAsync();
            var registeredCourses = studentCourses.Select(x => new { courseID = x.SemCourses.SCourseId, courseName = x.SemCourses.Course.courseName });
            var allCourses = await _context.SemesterCourses.Where(x => x.SemId == semester.Id).Select(x => new { courseID = x.Course.courseID, courseName = x.Course.courseName}).ToListAsync();
            ViewBag.ListOfSemesterCourses = allCourses.Except(registeredCourses).ToList();
            return View();
        }

        // POST: StudentSemesterCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SemesterCourseId,loginID,isApproved")] StudentSectionSemesterCourses studentSemesterCourses)
        {
            if (studentSemesterCourses.SemesterCourseId != null && studentSemesterCourses.SemesterCourseId != 0)
            {

                var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
                //var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
                var semester = await _context.Semester.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
                var semesterCourse = await _context.SemesterCourses.FirstOrDefaultAsync(x => x.SemId == semester.Id && x.SCourseId == studentSemesterCourses.SemesterCourseId);
                studentSemesterCourses.SemesterCourseId = semesterCourse.Id;
                if ((await ValidateCourseLimit(identifier, studentSemesterCourses.SemesterCourseId)))
                {
                    var studsemesterCourse = new StudentSectionSemesterCourses { SemesterCourseId = studentSemesterCourses.SemesterCourseId, loginID = identifier.Value, isApproved = false };
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
            var courses = await _context.StudentSectionSemesterCourses.Include(x => x.SemCourses).ThenInclude(x => x.Course).Where(x => x.loginID == identifier.Value && x.SemCourses.SemId == semester.Id).ToListAsync();
            var count = courses.Count();
            var creditHours = courses.Sum(x => x.SemCourses.Course.creditHours);
            Console.Write(count);
            if (creditHours < 17 && count <= 6)
                return true;
            else
                return false;
        }

        // GET: StudentSemesterCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentSectionSemesterCourses == null)
            {
                return NotFound();
            }
            
            var studentSemesterCourses = await _context.StudentSectionSemesterCourses.FindAsync(id);
            if (studentSemesterCourses == null)
            {
                return NotFound();
            }
            return View(studentSemesterCourses);
        }

        // POST: StudentSemesterCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SemesterCourseId,loginID,isApproved")] StudentSectionSemesterCourses studentSemesterCourses)
        {
            if (id != studentSemesterCourses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSemesterCourses);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSemesterCoursesExists(studentSemesterCourses.Id))
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
            return View(studentSemesterCourses);
        }

        // GET: StudentSemesterCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentSectionSemesterCourses == null)
            {
                return NotFound();
            }

            var studentSemesterCourses = await _context.StudentSectionSemesterCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSemesterCourses == null)
            {
                return NotFound();
            }

            return View(studentSemesterCourses);
        }

        // POST: StudentSemesterCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentSectionSemesterCourses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentSemesterCourses'  is null.");
            }
            var studentSemesterCourses = await _context.StudentSectionSemesterCourses.FindAsync(id);
            if (studentSemesterCourses != null)
            {
                _context.StudentSectionSemesterCourses.Remove(studentSemesterCourses);
            }
            
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSemesterCoursesExists(int id)
        {
          return (_context.StudentSectionSemesterCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
