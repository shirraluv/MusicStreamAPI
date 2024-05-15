using MusicStream;
using MusicStreamService.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MusicStreamService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ThrowdownyourtearsContext _context;

        public TrackController(ThrowdownyourtearsContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllTracks")]
        public async Task<ActionResult<List<TrackDTO>>> GetTracks()
        {
            List<TrackDTO> tracks = _context.Tracks.ToList().Select(s => new TrackDTO
            {
                Id = s.Id,
                Name = s.Name,
                Date = s.Date,
                Duration = s.Duration,
                Filename = s.Filename,
                Auditions = s.Auditions,
                Imagesource = s.Imagesource,
                Albumid = s.Albumid,

            }).ToList();
            return tracks;
        }

        [HttpGet("GetAllAlbumTracks")]
        public async Task<ActionResult<TrackDTO>> GetAllAlbumTracks(List<int> albumids)
        {
            List<TrackDTO> tracks = new List<TrackDTO>();
            foreach (int albumid in albumids)
            {
                List<TrackDTO> albumstracks = _context.Tracks.Where(s => s.Albumid == albumid)
                .Select(s => new TrackDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    Duration = s.Duration,
                    Filename = s.Filename,
                    Auditions = s.Auditions,
                    Imagesource = s.Imagesource,
                    Albumid = albumid,
                }).ToList();
                tracks.AddRange(albumstracks);
            }
            return Ok(tracks);
        }

        [HttpGet("GetAlbumTracks")]
        public async Task<ActionResult<TrackDTO>> GetAlbumTracks(int albumid)
        {
            List<TrackDTO> tracks = _context.Tracks.Where(s => s.Albumid == albumid)
                .Select(s => new TrackDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Date = s.Date,
                    Duration = s.Duration,
                    Filename = s.Filename,
                    Auditions = s.Auditions,
                    Imagesource = s.Imagesource,
                    Albumid = albumid,
                }).ToList();

            return Ok(tracks);
        }

        [HttpGet("GetTrack")]
        public async Task<ActionResult<TrackDTO>> GetTrack(int id)
        {
            var s = _context.Tracks.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();

            }
            return Ok(new TrackDTO
            {
                Id = s.Id,
                Name = s.Name,
                Date = s.Date,
                Duration = s.Duration,
                Filename = s.Filename,
                Auditions = s.Auditions,
                Imagesource = s.Imagesource,
                Albumid = s.Albumid,
            });
        }

        [HttpDelete("DeleteTrack")]
        public IActionResult DeleteTrack(int id)
        {
            Track track = _context.Tracks.Find(id);

            if (track == null)
            {
                return NotFound();
            }

            _context.Tracks.Remove(track);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("DeleteTracksByAlbumId")]
        public IActionResult DeleteTracksByAlbumId(int albumId)
        {
            var tracks = _context.Tracks.Where(t => t.Albumid == albumId).ToList();

            if (tracks == null)
            {
                return NotFound();
            }

            _context.Tracks.RemoveRange(tracks);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("AddTrack")]

        public async Task<ActionResult<TrackDTO>> AddTrack(TrackDTO createTrack)
        {
            Track track = _context.Tracks.FirstOrDefault(a => a.Name == createTrack.Name && a.Albumid == createTrack.Albumid);

            if (track != null)
            {
                return BadRequest("Данный трек уже добавлен");
            }
            else
            {
                var newtrack = new Track
                {
                    Id = createTrack.Id,
                    Name = createTrack.Name,
                    Date = DateTime.Now,
                    Duration = createTrack.Duration,
                    Albumid = createTrack.Albumid,
                    Auditions = createTrack.Auditions,
                    Imagesource = createTrack.Imagesource,
                    Filename = createTrack.Filename
                };
                _context.Tracks.Add(newtrack);
                await _context.SaveChangesAsync();
                createTrack.Id = newtrack.Id;
                return CreatedAtAction(nameof(GetTrack), new { id = newtrack.Id }, createTrack);
            }




        }

        [HttpPut("UpdateTrack")]
        public async Task<IActionResult> UpdateTrack(int id, TrackDTO trackDTO)
        {
            if (id != trackDTO.Id)
            {
                return BadRequest();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            // Обновляем данные товара на основе полученных данных
            track.Id = trackDTO.Id;
            track.Name = trackDTO.Name;
            track.Date = trackDTO.Date;
            track.Duration = trackDTO.Duration;
            track.Albumid = trackDTO.Albumid;
            track.Imagesource = trackDTO.Imagesource;
            track.Filename = trackDTO.Filename;
            track.Auditions = trackDTO.Auditions;

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