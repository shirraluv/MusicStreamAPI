namespace MusicStreamService.DTO
{
    public partial class UserDTO
    {
        public int Id { get; set; }

        public string Nick { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Imagesource { get; set; } = null!;

        public DateTime Regdate { get; set; }
    }
}
