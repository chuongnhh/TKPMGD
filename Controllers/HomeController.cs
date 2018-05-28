using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TKPMGD_NET.CodeHelper;
using TKPMGD_NET.Data;
using TKPMGD_NET.Models;

namespace TKPMGD_NET.Controllers {

    [Authorize]
    public class HomeController : Controller {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index() {
            var models = await _context.TestCases.Include(x => x.Sentences).Where(x=>x.UserId== _userManager.GetUserId(HttpContext.User)).ToListAsync();
            return View(models);
        }

        public async Task<IActionResult> Sentences(long id) {

            if (id == 0) {
                var test = await _context.TestCases.Include(x => x.Sentences).Where(x => x.UserId == _userManager.GetUserId(HttpContext.User)).LastOrDefaultAsync();
                if (test != null && !test.Sentences.Any(x => !string.IsNullOrEmpty(x.Content))) {
                    return RedirectToAction(nameof(HomeController.Sentences), "Home", new { test.Id });
                } else {
                    test = new TestCase();
                    test.UserId = _userManager.GetUserId(HttpContext.User);
                    _context.Entry(test).State = EntityState.Added;
                    for (int i = 0; i < 5; i++) {
                        var sentence = new Sentence();
                        sentence.UserId = _userManager.GetUserId(HttpContext.User);
                        _context.Entry(sentence).State = EntityState.Added;
                        test.Sentences.Add(sentence);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(HomeController.Sentences), "Home", new { test.Id });
                }
            }
            var model = await _context.TestCases.Include(x => x.Sentences).SingleOrDefaultAsync(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sentences(long Id, IFormFile InputFile) {
            var test = await _context.TestCases.Include(x => x.Sentences).SingleOrDefaultAsync(x => x.Id == Id);
            if (test == null) {
                return NotFound();
            }

            string controller = "upload";
            string fileName = "";
            String line = "";
            String Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            if (InputFile != null && InputFile.Length != 0) {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", controller, Todaysdate);
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }

                fileName = GuidComb.GenerateComb() + Path.GetExtension(InputFile.FileName);
                var filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await InputFile.CopyToAsync(stream);
                }
                using (var sr = new StreamReader(filePath)) {
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            if (line != null) {
                int i = 0;

                foreach (var item in line.Trim().Split("\n")) {
                    if (i > 5) break;
                    test.Sentences[i].UserId = _userManager.GetUserId(HttpContext.User);
                    test.Sentences[i++].Content = item.Trim();

                }
            }
            test.UserId = _userManager.GetUserId(HttpContext.User);
            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return View(test);
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SaveSentence(int id, string sentence) {
            var model = await _context.Sentences.SingleOrDefaultAsync(x => x.Id == id);
            if (model != null) {

                model.Content = sentence;
                await _context.SaveChangesAsync();
                return Json(new { success = 2 });
            }
            return Json(new { success = 0 });
        }

        public async Task<IActionResult> Compare(int id, string sentence,int time) {
            var model = await _context.Sentences.Include(x => x.TestCase).SingleOrDefaultAsync(x => x.Id == id);
            if (model != null) {

                if (!string.IsNullOrEmpty(sentence) && sentence.ToLower() == model.Content.ToLower()) {
                    model.Status = "PASS";
                    model.Time = time;
                    if (model.TestCase != null && model.TestCase.Sentences.Any(x => string.IsNullOrEmpty(x.Status)) == false) {
                        model.TestCase.Status = "DONE";
                        model.AddedDate = DateTime.Now;
                    }
                    await _context.SaveChangesAsync();
                    return Json(new { success = 1 });
                }
                model.Status = "FAIL";
                model.Time = time;
                if (model.TestCase != null && model.TestCase.Sentences.Any(x => string.IsNullOrEmpty(x.Status)) == false) {
                    model.AddedDate = DateTime.Now;
                    model.TestCase.Status = "DONE";
                }
                await _context.SaveChangesAsync();
                return Json(new { success = 2 });
            }
            return Json(new { success = 0 });
        }
    }
}
