using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string? search = "", int page = 1, int pageSize = 3)
        {
            var usersQuery = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                usersQuery = usersQuery.Where(u =>
                    u.FullName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search) ||
                    u.PhoneNumber.ToLower().Contains(search));
            }

            int totalUsers = await usersQuery.CountAsync();

            var users = await usersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { totalUsers, page, pageSize, users });
        }


        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
