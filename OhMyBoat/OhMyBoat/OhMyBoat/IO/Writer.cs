using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OhMyBoat.IO
{
    public class Writer : BinaryWriter
    {
        public Writer(Stream stream)
            : base(stream)
        {
        }
    }
}
