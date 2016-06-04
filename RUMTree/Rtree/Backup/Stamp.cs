using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace RUMTree
{
    class StampGenerator
    {
       private BigInteger stamp = 0;
       public BigInteger getStamp()
       {
           stamp++; return stamp;
       }
    }
}
