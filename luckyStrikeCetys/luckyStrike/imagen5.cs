using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luckyStrike
{
    class imagen5:Imagenes
    {
        public imagen5()
        {
            string Filename = "dado.png";
            FileInfo f = new FileInfo(Filename);
            string fullname = f.FullName;
            _path = fullname;
            _point = 100;
            _Id = 5;
        }
    }
}
