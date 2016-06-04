using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RUMTree
{
    class GarbageCollectCtrl <T>
    {
        //Garbage Cleaner to limit # of obsolete entries in the tree and limit the size of UM.
        //del obsolete entries lazily and in batches
        
            //cleaning token passed from on leaf node to the next when RUM tree
        //receives i updates. (I=InspectionInterval)
        //The node holding cleaning token inspects all entries in the node and cleans its obsolte etnreis,
        //then pass the token to the next leaf node after i updates.
        //In order to locate the next leaf node quickly the leaf nodes of RUM tree node are 
        //double linked in cycle
        private int inspectionInterval = 10;
        private int counter=0;
        private LinkedList<Node<T>> list;
        RUMTree<T> tree;
        public GarbageCollectCtrl(LinkedList<Node<T>> list, RUMTree<T> tree) {
            string inspectionInterval = GetAppConfig("RUMTreeGCInspectionInterval");
            if (inspectionInterval != null) this.inspectionInterval = Convert.ToInt32(inspectionInterval);
            this.tree = tree; 
            this.list = list; 
        }
        public void update() { 
          counter++;
          if (counter == inspectionInterval)
          {
              counter = 0;

              Boolean gc = false;
              do
              {
                 Node<T> n = list.First();
                  list.RemoveFirst();
                  if (n.isLeaf())
                  {  
                      tree.garbageCollect(n);
                      list.AddLast(n);
                      gc = true;
                  }
              }
              while (!gc);                                               
          }
        }
        public int listSize()
        {
            return list.Count;
        }

        private static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key.Equals(strKey)) return ConfigurationManager.AppSettings[strKey];
            }
            return null;
        }
        
    }
}
