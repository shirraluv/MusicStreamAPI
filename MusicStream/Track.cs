using System;
using System.Collections.Generic;

namespace MusicStream;

public partial class Track
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Duration { get; set; }

    public string Filename { get; set; }

    public int Auditions { get; set; }

    public int Albumid { get; set; }

    public string Name { get; set; } = null!;

    public string Imagesource { get; set; }

    public virtual Album Album { get; set; } = null!;

}
