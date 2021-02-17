using com.sun.source.tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje3B
{
    class TreeNode
    {
        public Tur data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public MyTree rotaAgac;


        public void displayNode()
        {
            Console.Write(data.turAdi + " ");
        }


    }
}
