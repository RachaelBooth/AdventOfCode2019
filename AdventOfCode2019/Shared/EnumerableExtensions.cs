using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Shared
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> objects)
        {
            var objectList = objects.ToList();
            if (!objectList.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var index = 0;
                while (index < objectList.Count())
                {
                    var lastItem = objectList[index];
                    var rest = objectList.Where((e, i) => i != index);
                    foreach (var permutation in rest.Permutations())
                    {
                        yield return permutation.Append(lastItem);
                    }
                    index += 1;
                }
            }
        }
    }
}
