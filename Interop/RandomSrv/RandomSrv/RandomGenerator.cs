using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RandomSrvv {
    [Guid("D8778C9F-2787-4B0F-A237-2903A0E10769")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRandomGenerator {
        double GenerateRandom(double min, double max);
    }

    [Guid("F66AC4C7-ADA8-4723-830C-7530BD517749")]
    [ClassInterface(ClassInterfaceType.None)]
    public class RandomGenerator : IRandomGenerator {
        public double GenerateRandom(double min, double max) {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
