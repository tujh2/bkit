using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class LevDistance {
    
        public static int Distance(string s1, string s2)
        {
            int len1 = s1.Length, len2 = s2.Length;
            if (len1 == 0 || len2 == 0)
                return Math.Max(len1, len2);
            string tmp1 = s1.ToUpper();
            string tmp2 = s2.ToUpper();

            int[,] d = new int[len1+1, len2+1];
            for (int i = 0; i <= len1; ++i) d[i, 0] = i;
            for (int j = 0; j <= len2; ++j) d[0, j] = j;

            for (int i = 1; i <= len1; ++i)
                for (int j = 1; j <= len2; ++j)
                {
                    int indicator = (tmp1[i-1] == tmp2[j-1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                    d[i-1, j] + 1,
                    Math.Min(d[i, j-1] + 1,
                    d[i - 1, j - 1] + indicator)
                    );
                }
            return d[len1, len2];
        }

        public static int DistanceDameray(string s1, string s2) {
            int len1 = s1.Length, len2 = s2.Length;
            if (len1 == 0 || len2 == 0)
                return Math.Max(len1, len2);
            string tmp1 = s1.ToUpper();
            string tmp2 = s2.ToUpper();

            int[,] d = new int[len1 + 1, len2 + 1];
            for (int i = 0; i <= len1; ++i) d[i, 0] = i;
            for (int j = 0; j <= len2; ++j) d[0, j] = j;

            for (int i = 1; i <= len1; ++i)
                for (int j = 1; j <= len2; ++j) {
                    int indicator = (tmp1[i-1] == tmp2[j-1]) ? 0 : 1;
                    
                    d[i, j] = Math.Min(
                    d[i, j - 1] + 1,
                    Math.Min(d[i - 1, j] + 1,
                    d[i - 1, j - 1] + indicator)
                    );
                    if (i > 1 && j > 1 && tmp1[i - 1] == tmp2[j - 2] && tmp1[i - 2] == tmp2[j - 1])
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + indicator);
                }
            return d[len1, len2];
        }
    }
}
