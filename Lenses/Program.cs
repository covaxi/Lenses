using Lenses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lenses
{
    class Program
    {
        static void Main(string[] args)
        {
            new { A = 1 }.With(x => x.A, 2);
        }
    }
}
