using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luckyStrike
{
    class imagen4:Imagenes
    {
        public imagen4()
        {
            string Filename = "cherry.png";
            FileInfo f = new FileInfo(Filename);
            string fullname = f.FullName;
            _path = fullname;
            _point = 300;
            _Id = 4;
        }
    }
}
