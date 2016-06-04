using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace RUMTree
{
    public class UpdatedMemo
    {
       // o_id object identifier(UpdatedMemo is hashed on o_id)
        //s_latest stamp of the latest entry of the object oid
        //n_old maximum number of obsolete entries for the object o_id in the RUM-tree
        //e.g. O_99,1000,2 entails that in the RUM-tree there exist at most two obsolte entries for
        //object O_99, latest entry for O_99 has stamp 1000
        //No UM entry has n_old=0,which means, 
        //objects that are assured to have no obsolete entries in the RUM-tree do not own a a UM entry
        //As an update occurs, the old entry of the data is allowed to coexist with newer entries before
        //it is removed later. Only one entry of aan object is the most recent entry(latest entry), all the
        //other entries are old entries(obsolte entries), UpdateMemo is maintained to identify the latest 
        //entries from the obsolete entries. The obsolete entries are identified and removed from RUM tree by
        //GarbageCleaner

        private int o_id;
        private BigInteger s_latest;
        private int n_old;
        public int ObsoleteEntryCnt { get { return n_old; } }
        public void incrementObsoleteEntryCnt(){
            this.n_old++;
        }
        public void decreaseObsoleteEntryCnt()
        {
            this.n_old--;
        }
        public BigInteger latestStamp { get { return s_latest; } }
        public void setLatestStamp(BigInteger stamp) {  s_latest=stamp;  }

        public UpdatedMemo(int o_id, int n_old, BigInteger stamp)
        {
            this.o_id = o_id;
            this.s_latest = stamp;
            this.n_old = n_old;
        }
        
     }
}
