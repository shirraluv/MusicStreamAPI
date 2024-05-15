using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStream
{
    public static class DB
    {
        private static ThrowdownyourtearsContext _context;
        public static ThrowdownyourtearsContext Instance
        {
            get
            {
                if (_context == null)
                    _context = new ThrowdownyourtearsContext();
                return _context;
            }
        }
    }
}
