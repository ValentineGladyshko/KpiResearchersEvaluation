using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Indexes
{
    public interface IIndex
    {
        int GetIndex(IEnumerable<int> citationCountList);
    }
}
