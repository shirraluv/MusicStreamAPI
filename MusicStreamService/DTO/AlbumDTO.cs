namespace MusicStreamService.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Creatorid { get; set; }

        public string Duration { get; set; }

        public string Imagesource { get; set; }

        public DateTime Date { get; set; }
    }
}
