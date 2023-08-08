using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Wordle.Extensions
{

    public static class IEnumerableExtensions
    {

        public static T RandomElementByWeight<T>(this IEnumerable<T> sequence) where T: WeightedItem
        {
            double totalWeight = sequence.Sum(x => (long)x.Weight); 
            
            // The weight we are after...
            double itemWeightIndex = (double)new Random().NextDouble() * totalWeight;
            double currentWeightIndex = 0;

            foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightedItem.Weight })
            {
                currentWeightIndex += item.Weight;

                // If we've hit or passed the weight we are after for this item then it's the one we want....
                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;

            }

            return default(T);

        }

    }
}
