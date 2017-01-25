using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    public class PresidentialDataStore
    {
        public IDictionary<int,string> GetPresidents()
        {
            return new Dictionary<int, string>
            {
                {45, "Donald Trump"},
                {44, "Barack Obama"},
                {43, "George W Bush"},
                {42, "Bill Clinton"},
                {41, "George H Bush"},
                {40, "Ronald Reagan"},

            };
        }
    }
}
