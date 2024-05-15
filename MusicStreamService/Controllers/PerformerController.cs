using MusicStream;
using MusicStreamService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MusicStreamService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        private readonly ThrowdownyourtearsContext _context;

        public PerformerController(ThrowdownyourtearsContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllPerformers")]
        public async Task<ActionResult<List<PerformerDTO>>> GetPerformers()
        {
            List<PerformerDTO> performers = _context.Performers.ToList().Select(s => new PerformerDTO
            {
                Id = s.Id,
                Nick = s.Nick,
                Password = s.Password,
                Regdate = s.Regdate,
                Auditions = s.Auditions,
                Imagesource = s.Imagesource
            }).ToList();
            return performers;
        }
        [HttpGet("GetPerformer")]
        public async Task<ActionResult<PerformerDTO>> GetPerformer(int id)
        {
            var s = _context.Performers.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new PerformerDTO
            {
                Id = s.Id,
                Nick = s.Nick,
                Password = s.Password,
                Regdate = s.Regdate,
                Auditions = s.Auditions,
                Imagesource = s.Imagesource
            });
        }

        [HttpGet("GetPerformerByName")]
        public async Task<ActionResult<PerformerDTO>> GetPerformerByName(string Name)
        {
            var s = _context.Performers.FirstOrDefault(s => s.Nick == Name);
            if (s == null)
            {
                return NotFound();

            }
            return Ok(new PerformerDTO
            {
                Id = s.Id,
                Nick = Name,
                Password = s.Password,
                Regdate = s.Regdate,
                Auditions = s.Auditions,
                Imagesource = s.Imagesource
            });
        }

        [HttpPost("PerformerLogin")]
        public ActionResult<PerformerDTO> PerformerLogin(LoginPerformer loginPerformer)
        {

            Performer performer = _context.Performers.FirstOrDefault(a => a.Nick == loginPerformer.Nick && a.Password == loginPerformer.Password);
            if (performer != null)
            {
                return new PerformerDTO
                {
                    Id = performer.Id,
                    Nick = performer.Nick,
                    Password = performer.Password,
                    Regdate = performer.Regdate,
                    Auditions = performer.Auditions,
                    Imagesource = performer.Imagesource
                };
            }
            else
            {
                return BadRequest("нЕПРАВИЛЬНЫЙ лОгин или пароль");
            }

        }
        [HttpPost("PerformerRegistration")]
        public ActionResult<Performer> Registration(Performer performer)
        {
            if (_context.Performers.Any(u => u.Nick == performer.Nick))
            {
                return BadRequest("Исполнитель с таким никнеймом уже зарегистрирован");
            }

            _context.Performers.Add(performer);
            _context.SaveChanges();

            return Ok("Исполнитель зарегистрирован");
        }
    }
}
