namespace MusicStreamService.DTO
{
    public class PerformerDTO
    {
        public int Id { get; set; }

        public string? Nick { get; set; }

        public int Auditions { get; set; }

        public DateTime Regdate { get; set; }

        public string Imagesource { get; set; }

        public string Password { get; set; } = null!;
    }
}
