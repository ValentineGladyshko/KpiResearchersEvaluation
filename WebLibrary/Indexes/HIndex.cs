using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Indexes
{
    public class HIndex : IIndex
    {
        public int GetIndex(IEnumerable<int> citationCountList)
        {
            //citationCountList.Sort();
            citationCountList = citationCountList.OrderByDescending(item => item);
            if(citationCountList.Count() == 0)
            {
                return 0;
            }
            int i = 0;
            while (citationCountList.ElementAt(i) >= (i + 1))
            {
                i++;
            }
            return i;
        }
    }
}
