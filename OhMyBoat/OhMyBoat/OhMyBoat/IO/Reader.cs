using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OhMyBoat.IO
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream)
        {
        }
    }
}
