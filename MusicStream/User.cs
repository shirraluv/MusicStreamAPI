using System;
using System.Collections.Generic;

namespace MusicStream;

public partial class User
{
    public int Id { get; set; }

    public string Nick { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Imagesource { get; set; } = null!;

    public DateTime Regdate { get; set; }

}
