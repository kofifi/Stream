﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Stream.Helpers;

namespace Stream.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), new { searchQuery });
        }

        public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var libraries = await _libraryService.GetAllAsync(searchQuery, pageNumber, pageSize);
            var totalLibraries = await _libraryService.GetTotalCountAsync(searchQuery);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalLibraries"] = totalLibraries;

            return View(libraries);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create()
        {
            ViewData["Users"] = await _libraryService.GetUsersSelectListAsync();
            ViewData["Games"] = await _libraryService.GetGamesSelectListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(Library library)
        {
            if (ModelState.IsValid)
            {
                await _libraryService.AddAsync(library);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = await _libraryService.GetUsersSelectListAsync(library.UserId);
            ViewData["Games"] = await _libraryService.GetGamesSelectListAsync(library.GameId);
            return View(library);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>());
            return View(library);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, Library library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Status = new SelectList(Enum.GetValues(typeof(Status)).Cast<Status>());
                return View(library);
            }

            await _libraryService.UpdateAsync(library);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var library = await _libraryService.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _libraryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}