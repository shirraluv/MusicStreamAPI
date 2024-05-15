using System;
using System.Collections.Generic;

namespace MusicStream;

public partial class Performer
{
    public int Id { get; set; }

    public string Nick { get; set; } = null!;

    public string Imagesource { get; set; }

    public int Auditions { get; set; }

    public DateTime Regdate { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
