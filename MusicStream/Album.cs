using System;
using System.Collections.Generic;

namespace MusicStream;

public partial class Album
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Creatorid { get; set; }

    public string Duration { get; set; }

    public string Imagesource { get; set; }

    public DateTime Date { get; set; }

    public virtual Performer Creator { get; set; } = null!;

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
