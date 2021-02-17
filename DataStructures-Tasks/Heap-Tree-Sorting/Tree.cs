using sun.reflect.generics.tree;
using System;
using System.Collections.Generic;
using System.Text;
using com.sun.source.tree;
using Tree = com.sun.source.tree.Tree;

namespace Proje3B
{
    class MyTree
    {
        private TreeNode root;
       
        public MyTree()
        {
            root = null;     
        }

        public TreeNode getRoot()
        {
            return root;
        }

       
        public void preOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.displayNode();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }

        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }

        public void postOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                postOrder(localRoot.rightChild);
                postOrder(localRoot.leftChild);
                localRoot.displayNode();
            }
        }

        
        public void insert(Tur newData)
        {
            TreeNode node = new TreeNode();
            node.data = newData;

            //root.rotaAgac = 

            if (root == null)
            root = node;

            else
            {
                TreeNode current = root;
                TreeNode parent;

                while (true)
                {
                    parent = current;

                    if (String.CompareOrdinal(newData.turAdi, current.data.turAdi) < 0) //yeni item küçükse
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = node;
                            return;
                        }
                    }

                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = node;
                            return;
                        }
                    }

                }
            }
        }
        }
    }
