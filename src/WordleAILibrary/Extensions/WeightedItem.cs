using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Extensions
{
    public abstract class WeightedItem
    {
        public double Weight { get; set; }
        public bool Active { get; set; }

    }
}
