using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luckyStrike
{
    class imagen7:Imagenes
    {
        public imagen7()
        {
            string Filename = "monedas.png";
            FileInfo f = new FileInfo(Filename);
            string fullname = f.FullName;
            _path = fullname;
            _point = 400;
            _Id = 7;
        }
    }
}
