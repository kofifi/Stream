using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stream.Services.Interfaces;
using Stream.ViewModels.Dto;
using Stream.ViewModels.ViewModels;

namespace Stream.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        var users = await _userService.SearchUserAsync(searchQuery, pageNumber, pageSize);
        var totalUsers = await _userService.GetTotalCountAsync(searchQuery);

        var viewModel = new UserViewModel
        {
            Users = _mapper.Map<IEnumerable<UserDto>>(users),
            SearchQuery = searchQuery,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalUsers = totalUsers ?? 0
        };

        if (IsAjaxRequest())
        {
            return PartialView("_UserTablePartial", viewModel.Users);
        }

        return View(viewModel);
    }

    public bool IsAjaxRequest()
    {
        return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    [HttpPost]
    public IActionResult ResetSearch()
    {
        return RedirectToAction(nameof(Index), new { searchQuery = string.Empty });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return View(userDto);
        }

        var user = _mapper.Map<Stream.Models.User>(userDto);
        await _userService.AddAsync(user);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userDto = _mapper.Map<UserDto>(user);
        return View(userDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserDto userDto)
    {
        if (id != userDto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(userDto);
        }

        var user = _mapper.Map<Stream.Models.User>(userDto);
        await _userService.UpdateAsync(user);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userDto = _mapper.Map<UserDto>(user);
        return View(userDto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}