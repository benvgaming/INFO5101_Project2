using System.Collections;

namespace Project2_INFO5101
{
    public class CompareExpressions : IComparer
    {
        public int Compare(object x, object y)
        {
            return x.Equals(y) ? 1 : 0;
        }
    }
}
