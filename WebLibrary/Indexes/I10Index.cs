using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Indexes
{
    public class I10Index : IIndex
    {
        public int GetIndex(IEnumerable<int> citationCountList)
        {
            return citationCountList.Where(item => item >= 10).Count();
        }
    }
}
