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
        class A
        { 
            public int X { get; set; } 
            public int Y { get; set; }
            public override string ToString() => "X:" + X + " Y:" + Y;
        }

        static void Main()
        {
            Console.WriteLine(new A { X = 1 }.With(x => x.X = 2));
            Console.ReadKey();
        }

    }
}
