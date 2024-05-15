
using MusicStream;
using MusicStreamService.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicStreamService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ThrowdownyourtearsContext _context;

        public AlbumController(ThrowdownyourtearsContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllAlbums")]
        public async Task<ActionResult<List<AlbumDTO>>> GetAlbums()
        {
            List<AlbumDTO> albums = _context.Albums.ToList().Select(s => new AlbumDTO
            {
                Id = s.Id,
                Name = s.Name,
                Date = s.Date,
                Duration = s.Duration,
                Creatorid = s.Creatorid,
                Imagesource = s.Imagesource,

            }).ToList();
            return albums;
        }

        [HttpGet("GetPerformerAlbums")]
        public async Task<ActionResult<List<AlbumDTO>>> GetPerformerAlbums(int creatorid)
        {
            List<AlbumDTO> albums = _context.Albums.Where(s => s.Creatorid == creatorid)
                .Select(s => new AlbumDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    Duration = s.Duration,
                    Creatorid = creatorid,
                    Imagesource = s.Imagesource,
                }).ToList();

            return albums;
        }

        [HttpGet("GetAlbum")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var s = _context.Albums.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();

            }
            return Ok(new AlbumDTO
            {
                Id = s.Id,
                Name = s.Name,
                Date = s.Date,
                Duration = s.Duration,
                Creatorid = s.Creatorid,
                Imagesource = s.Imagesource,
            });
        }

        [HttpPost("AddAlbum")]

        public async Task<ActionResult<AlbumDTO>> AddAlbum(AlbumDTO createAlbum)
        {

            Album album = _context.Albums.FirstOrDefault(a => a.Name == createAlbum.Name && a.Creatorid == createAlbum.Creatorid);

            if (album != null)
            {
                return BadRequest("Данный альбом уже добавлен");
            }
            else
            {
                var newalbum = new Album
                {
                    Id = createAlbum.Id,
                    Name = createAlbum.Name,
                    Date = DateTime.Now,
                    Duration = createAlbum.Duration,
                    Creatorid = createAlbum.Creatorid,
                    Imagesource = createAlbum.Imagesource,
                };
                _context.Albums.Add(newalbum);
                await _context.SaveChangesAsync();
                createAlbum.Id = newalbum.Id;
                return CreatedAtAction(nameof(GetAlbum), new { id = newalbum.Id }, createAlbum);
            }

        }

        [HttpDelete("DeleteAlbum")]
        public IActionResult DeleteAlbum(int id)
        {
            Album album = _context.Albums.Find(id);

            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("UpdateAlbum")]
        public async Task<IActionResult> UpdateAlbum(int id, AlbumDTO albumDTO)
        {
            if (id != albumDTO.Id)
            {
                return BadRequest();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            // Обновляем данные товара на основе полученных данных
            album.Id = albumDTO.Id;
            album.Name = albumDTO.Name;
            album.Date = albumDTO.Date;
            album.Duration = albumDTO.Duration;
            album.Creatorid = albumDTO.Creatorid;
            album.Imagesource = albumDTO.Imagesource;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обработка ошибки параллельного доступа или других конкурентных изменений данных
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ошибка при обновлении товара. Попробуйте еще раз." });
            }
        }
    }
}
