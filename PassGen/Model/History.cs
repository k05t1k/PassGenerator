using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGen
{
    public class History
    {
        public History(string generatedPassword, int lengthPassword) 
        {
            GeneratedPassword = generatedPassword;
            LengthPassword = lengthPassword;
        }

        public string GeneratedPassword { get; set; }
        public int LengthPassword { get; set; }
    }
}
