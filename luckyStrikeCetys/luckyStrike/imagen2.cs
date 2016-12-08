using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luckyStrike
{
    class imagen2:Imagenes
    {
        public imagen2()
        {
            string Filename = "bolsa.png";
            FileInfo f = new FileInfo(Filename);
            string fullname = f.FullName;
            _path = fullname;
            _point = 500;
            _Id = 2;
        }
    }
}
