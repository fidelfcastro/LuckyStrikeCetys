using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luckyStrike
{
    class imagen6:Imagenes
    {
        public imagen6()
        {
            string Filename = "ficha.png";
            FileInfo f = new FileInfo(Filename);
            string fullname = f.FullName;
            _path = fullname;
            _point = 70;
            _Id = 6;
        }
    }
}
