using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BepisModManager.Installation
{
    static class TempStorage
    {
        public static string TempPath
        {
            get => Path.Combine(Path.GetTempPath(), "BepisModManager");
        }
    }
}
