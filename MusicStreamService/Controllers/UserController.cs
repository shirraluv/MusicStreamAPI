using MusicStream;
using MusicStreamService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MusicStreamService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ThrowdownyourtearsContext _context;

        public UserController(ThrowdownyourtearsContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            List<UserDTO> users = _context.Users.ToList().Select(s => new UserDTO
            {
                Id = s.Id,
                Nick = s.Nick,
                Password = s.Password,
                Regdate = s.Regdate,
                Imagesource = s.Imagesource
            }).ToList();
            return users;
        }
        [HttpGet("GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var s = _context.Users.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new UserDTO
            {
                Id = s.Id,
                Nick = s.Nick,
                Password = s.Password,
                Regdate = s.Regdate,
                Imagesource = s.Imagesource
            });
        }

        [HttpGet("GetUserByName")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string Name)
        {
            var s = _context.Users.FirstOrDefault(s => s.Nick == Name);
            if (s == null)
            {
                return NotFound();

            }
            return Ok(new UserDTO
            {
                Id = s.Id,
                Nick = Name,
                Password = s.Password,
                Regdate = s.Regdate,
                Imagesource = s.Imagesource
            });
        }

        [HttpPost("UserLogin")]
        public ActionResult<UserDTO> UserLogin(LoginUser loginUser)
        {

            User user = _context.Users.FirstOrDefault(a => a.Nick == loginUser.Nick && a.Password == loginUser.Password);
            if (user != null)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Nick = user.Nick,
                    Password = user.Password,
                    Regdate = user.Regdate,
                    Imagesource = user.Imagesource
                };
            }
            else
            {
                return BadRequest("нЕПРАВИЛЬНЫЙ лОгин или пароль");
            }

        }
        [HttpPost("UserRegistration")]
        public ActionResult<User> Registration(User user)
        {
            if (_context.Users.Any(u => u.Nick == user.Nick))
            {
                return BadRequest("Пользователь с таким никнеймом уже зарегистрирован");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Пользователь зарегистрирован");
        }
    }
}