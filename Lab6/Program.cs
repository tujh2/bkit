using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab6 {
    class Program {
        delegate int SampleDelegate(String a, String b);

        static void Main(string[] args) {
            SampleDelegate sampleDelegate;
            sampleDelegate = DelegateLevDistance;

            Console.WriteLine(
                sampleDelegate("aaaaa", "bbbbb"));

            Console.WriteLine(
                DelegateAsParam(sampleDelegate, "aaaaa", "bbbbb"));

            Console.WriteLine(
                DelegateAsParam((String a, String b) => Lab5.LevDistance.Distance(a, b), "aaaaaaaaa", "bbbbb"));
            Func<String, String, int> func = sampleDelegate;
            Console.WriteLine(
                CommonDelegateAsParam(, "aaaaa", "bbbbb"));

            Console.WriteLine(
                CommonDelegateAsParam((String a, String b) => Lab5.LevDistance.Distance(a, b), "aaaaaaaaaaaaaaa", "bbbbb"));
        }

        static int DelegateLevDistance(String a, String b) {
            return Lab5.LevDistance.Distance(a, b);
        }

        static int DelegateAsParam(SampleDelegate sampleDelegate, String a, String b) {
            return sampleDelegate(a, b);
        }

        static int CommonDelegateAsParam(Func<String, String, int> func, String a, String b) {
            return func(a, b);
        }
    } 
}
