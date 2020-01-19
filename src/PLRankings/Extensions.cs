using System;
using System.Collections.Generic;

namespace PLRankings
{
    public static class Extensions
    {
        /// <summary>Computes the Levenshtein Edit Distance between two enumerables.</summary>
        /// <typeparam name="T">The type of the items in the enumerables.</typeparam>
        /// <param name="x">The first enumerable.</param>
        /// <param name="y">The second enumerable.</param>
        /// <returns>The edit distance.</returns>
        public static int EditDistance<T>(this IEnumerable<T> x, IEnumerable<T> y)
            where T : IEquatable<T>
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            if (y == null)
                throw new ArgumentNullException(nameof(y));

            var first = x as IList<T> ?? new List<T>(x);
            var second = y as IList<T> ?? new List<T>(y);

            int n = first.Count, m = second.Count;

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            int curRow = 0, nextRow = 1;

            int[][] rows = { new int[m + 1], new int[m + 1] };

            for (var j = 0; j <= m; ++j)
                rows[curRow][j] = j;

            for (var i = 1; i <= n; ++i)
            {
                rows[nextRow][0] = i;

                for (var j = 1; j <= m; ++j)
                {
                    var dist1 = rows[curRow][j] + 1;
                    var dist2 = rows[nextRow][j - 1] + 1;
                    var dist3 = rows[curRow][j - 1] + (first[i - 1].Equals(second[j - 1]) ? 0 : 1);

                    rows[nextRow][j] = Math.Min(dist1, Math.Min(dist2, dist3));
                }

                if (curRow == 0)
                {
                    curRow = 1;
                    nextRow = 0;
                }
                else
                {
                    curRow = 0;
                    nextRow = 1;
                }
            }

            return rows[curRow][m];
        }
    }
}
