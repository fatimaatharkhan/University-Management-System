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
    public class UserprofilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserprofilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Userprofiles
        public async Task<IActionResult> Index()
        {
              return _context.Userprofile != null ? 
                          View(await _context.Userprofile.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Userprofile'  is null.");
        }

        // GET: Userprofiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Userprofile == null)
            {
                return NotFound();
            }

            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
            if (userprofile == null)
            {
                return NotFound();
            }

            return View(userprofile);
        }

        // GET: Userprofiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Userprofiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,loginID,phoneNumber,s_rollNumber,s_section,s_degree,gender,s_batch,DOB,city,country,cnic,address")] Userprofile userprofile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userprofile);
                await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                return RedirectToAction(nameof(Index));
            }
            return View(userprofile);
        }

        // GET: Userprofiles/Edit/5
        public async Task<IActionResult> Edit()
        {
            
            if (_context.Userprofile == null)
            {
                return NotFound();
            }

            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
            //id = userprofile.Id;
            if (userprofile == null)
            {
                return NotFound();
            }
            return View(userprofile);
        }

        // POST: Userprofiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,loginID,phoneNumber,s_rollNumber,s_section,s_degree,gender,s_batch,DOB,city,country,cnic,address")] Userprofile userprofile)
        {
            if (id != userprofile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userprofile);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserprofileExists(userprofile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(userprofile);
        }

        // GET: Userprofiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Userprofile == null)
            {
                return NotFound();
            }

            var userprofile = await _context.Userprofile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userprofile == null)
            {
                return NotFound();
            }

            return View(userprofile);
        }

        // POST: Userprofiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Userprofile == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Userprofile'  is null.");
            }
            var userprofile = await _context.Userprofile.FindAsync(id);
            if (userprofile != null)
            {
                _context.Userprofile.Remove(userprofile);
            }
            
            await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));
        }

        private bool UserprofileExists(int id)
        {
          return (_context.Userprofile?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> StudentDetails(int? id)
        {
            if (id == null || _context.Userprofile == null)
            {
                return NotFound();
            }

            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
            if (userprofile == null)
            {
                return NotFound();
            }

            return View(userprofile);
        }

        // GET: Userprofiles/StudentEdit/5
        public async Task<IActionResult> StudentEdit()
        {

            if (_context.Userprofile == null)
            {
                return NotFound();
            }

            var identifier = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            var userprofile = await _context.Userprofile.FirstOrDefaultAsync(x => x.loginID.ToLower() == identifier.Value.ToString().ToLower());
            //id = userprofile.Id;
            if (userprofile == null)
            {
                return NotFound();
            }
            return View(userprofile);
        }

        // POST: Userprofiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentEdit(int id, [Bind("Id,FirstName,LastName,loginID,phoneNumber,s_rollNumber,s_section,s_degree,gender,s_batch,DOB,city,country,cnic,address")] Userprofile userprofile)
        {
            if (id != userprofile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userprofile);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserprofileExists(userprofile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Student", "Home");
            }
            return View(userprofile);
        }
    }
}
