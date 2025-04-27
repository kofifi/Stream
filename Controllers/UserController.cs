using Microsoft.AspNetCore.Mvc;
using Stream.Models;
using Stream.Services.Interfaces;
using System.Threading.Tasks;

namespace Stream.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index(string searchQuery)
    {
        var users = await _userService.GetAllAsync(searchQuery);
        ViewData["SearchQuery"] = searchQuery;

        if (IsAjaxRequest())
        {
            return PartialView("_UserTablePartial", users);
        }

        return View(users);
    }

    public bool IsAjaxRequest()
    {
        return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    public IActionResult ResetSearch()
    {
        return RedirectToAction(nameof(Index), new { searchQuery = string.Empty });
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        await _userService.AddAsync(user);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        await _userService.UpdateAsync(user);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Search()
    {
        return View();
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
}