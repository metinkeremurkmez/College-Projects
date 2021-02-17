using System;
using System.Collections.Generic;

namespace proje4b
{
    class Program
    {
        public static int INFINITY = 1000;

       


        class AVL                                                              // AVL
        {
            class Node
            {
                public int data;
                public Node left;
                public Node right;
                public Node(int data)
                {
                    this.data = data;
                }
            }
            Node root;
            public AVL()
            {
            }
            public void Add(int data)
            {
                Node newItem = new Node(data);
                if (root == null)
                {
                    root = newItem;
                }
                else
                {
                    root = RecursiveInsert(root, newItem);
                }
            }
            private Node RecursiveInsert(Node current, Node n)
            {
                if (current == null)
                {
                    current = n;
                    return current;
                }
                else if (n.data < current.data)
                {
                    current.left = RecursiveInsert(current.left, n);
                    current = balance_tree(current);
                }
                else if (n.data > current.data)
                {
                    current.right = RecursiveInsert(current.right, n);
                    current = balance_tree(current);
                }
                return current;
            }
            private Node balance_tree(Node current)
            {
                int b_factor = balance_factor(current);
                if (b_factor > 1)
                {
                    if (balance_factor(current.left) > 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
                else if (b_factor < -1)
                {
                    if (balance_factor(current.right) > 0)
                    {
                        current = RotateRL(current);
                    }
                    else
                    {
                        current = RotateRR(current);
                    }
                }
                return current;
            }
            public void Delete(int target)
            {//and here
                root = Delete(root, target);
            }
            private Node Delete(Node current, int target)
            {
                Node parent;
                if (current == null)
                { return null; }
                else
                {
                    //left subtree
                    if (target < current.data)
                    {
                        current.left = Delete(current.left, target);
                        if (balance_factor(current) == -2)//here
                        {
                            if (balance_factor(current.right) <= 0)
                            {
                                current = RotateRR(current);
                            }
                            else
                            {
                                current = RotateRL(current);
                            }
                        }
                    }
                    //right subtree
                    else if (target > current.data)
                    {
                        current.right = Delete(current.right, target);
                        if (balance_factor(current) == 2)
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else
                            {
                                current = RotateLR(current);
                            }
                        }
                    }
                    //if target is found
                    else
                    {
                        if (current.right != null)
                        {
                            //delete its inorder successor
                            parent = current.right;
                            while (parent.left != null)
                            {
                                parent = parent.left;
                            }
                            current.data = parent.data;
                            current.right = Delete(current.right, parent.data);
                            if (balance_factor(current) == 2)//rebalancing
                            {
                                if (balance_factor(current.left) >= 0)
                                {
                                    current = RotateLL(current);
                                }
                                else { current = RotateLR(current); }
                            }
                        }
                        else
                        {   //if current.left != null
                            return current.left;
                        }
                    }
                }
                return current;
            }
            public void Find(int key)
            {
                if (Find(key, root).data == key)
                {
                    Console.WriteLine("{0} was found!", key);
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
            private Node Find(int target, Node current)
            {

                if (target < current.data)
                {
                    if (target == current.data)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.left);
                }
                else
                {
                    if (target == current.data)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.right);
                }

            }
            public void DisplayTree()
            {
                if (root == null)
                {
                    Console.WriteLine("Tree is empty");
                    return;
                }
                InOrderDisplayTree(root);
                Console.WriteLine();
            }
            private void InOrderDisplayTree(Node current)
            {
                if (current != null)
                {
                    InOrderDisplayTree(current.left);
                    Console.Write("({0}) ", current.data);
                    InOrderDisplayTree(current.right);
                }
            }
            private int max(int l, int r)
            {
                return l > r ? l : r;
            }
            private int getHeight(Node current)
            {
                int height = 0;
                if (current != null)
                {
                    int l = getHeight(current.left);
                    int r = getHeight(current.right);
                    int m = max(l, r);
                    height = m + 1;
                }
                return height;
            }
            private int balance_factor(Node current)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int b_factor = l - r;
                return b_factor;
            }
            private Node RotateRR(Node parent)
            {
                Node pivot = parent.right;
                parent.right = pivot.left;
                pivot.left = parent;
                return pivot;
            }
            private Node RotateLL(Node parent)
            {
                Node pivot = parent.left;
                parent.left = pivot.right;
                pivot.right = parent;
                return pivot;
            }
            private Node RotateLR(Node parent)
            {
                Node pivot = parent.left;
                parent.left = RotateRR(pivot);
                return RotateLL(parent);
            }
            private Node RotateRL(Node parent)
            {
                Node pivot = parent.right;
                parent.right = RotateLL(pivot);
                return RotateRR(parent);
            }
        }
        // AVL END
        //     BFS
        public class Employee
        {
            public Employee(string name)
            {
                this.name = name;
            }

            public string name { get; set; }
            public List<Employee> Employees
            {
                get
                {
                    return EmployeesList;
                }
            }

            public void isEmployeeOf(Employee p)
            {
                EmployeesList.Add(p);
            }

            List<Employee> EmployeesList = new List<Employee>();

            public override string ToString()
            {
                return name;
            }
        }

        public class BreadthFirstAlgorithm
        {
            public Employee BuildEmployeeGraph()
            {
                Employee Eva = new Employee("Eva");
                Employee Sophia = new Employee("Sophia");
                Employee Brian = new Employee("Brian");
                Eva.isEmployeeOf(Sophia);
                Eva.isEmployeeOf(Brian);

                Employee Lisa = new Employee("Lisa");
                Employee Tina = new Employee("Tina");
                Employee John = new Employee("John");
                Employee Mike = new Employee("Mike");
                Sophia.isEmployeeOf(Lisa);
                Sophia.isEmployeeOf(John);
                Brian.isEmployeeOf(Tina);
                Brian.isEmployeeOf(Mike);

                return Eva;
            }

            public Employee Search(Employee root, string nameToSearchFor)
            {
                Queue<Employee> Q = new Queue<Employee>();
                HashSet<Employee> S = new HashSet<Employee>();
                Q.Enqueue(root);
                S.Add(root);

                while (Q.Count > 0)
                {
                    Employee e = Q.Dequeue();
                    if (e.name == nameToSearchFor)
                        return e;
                    foreach (Employee friend in e.Employees)
                    {
                        if (!S.Contains(friend))
                        {
                            Q.Enqueue(friend);
                            S.Add(friend);
                        }
                    }
                }
                return null;
            }

            public void Traverse(Employee root)
            {
                Queue<Employee> traverseOrder = new Queue<Employee>();

                Queue<Employee> Q = new Queue<Employee>();
                HashSet<Employee> S = new HashSet<Employee>();
                Q.Enqueue(root);
                S.Add(root);

                while (Q.Count > 0)
                {
                    Employee e = Q.Dequeue();
                    traverseOrder.Enqueue(e);

                    foreach (Employee emp in e.Employees)
                    {
                        if (!S.Contains(emp))
                        {
                            Q.Enqueue(emp);
                            S.Add(emp);
                        }
                    }
                }

                while (traverseOrder.Count > 0)
                {
                    Employee e = traverseOrder.Dequeue();
                    Console.WriteLine(e);
                }
            }
        }                                                                      // BFS END              
                                                                               // DJIKSTRA
        public static int[] Distance(int N, int[,] cost, int[] D, int src)
        {

            int w, v, min;

            bool[] visited = new bool[N];

            int[] previous = new int[N]; //for tracking shortest paths (güzergah)

            //initialization of D[], visited[] and previous[] arrays according to src node
            for (v = 0; v < N; v++)
            {
                if (v != src)
                {
                    visited[v] = false;
                    D[v] = cost[src, v];
                    if (D[v] != INFINITY) //there is a connection between src and v
                    {
                        previous[v] = src;
                    }
                    else //no path from source
                    {
                        previous[v] = -1;
                    }
                }
                else
                {
                    visited[v] = true;
                    D[v] = 0;
                    previous[v] = -1;
                }

            }

            // Searching for shortest paths
            for (int i = 0; i < N; ++i)
            {
                min = INFINITY;
                for (w = 0; w < N; w++)
                    if (!visited[w])
                        if (D[w] < min)
                        {
                            v = w;
                            min = D[w];
                        }

                visited[v] = true;

                for (w = 0; w < N; w++)
                    if (!visited[w])
                        if (min + cost[v, w] < D[w])
                        {
                            D[w] = min + cost[v, w];
                            previous[w] = v; //update the path info
                        }
            }

            return previous;
        }

        public static void printShortestPathStraight(int dest, int[] previous)
        {
            Stack<int> pathStack = new Stack<int>();

            int current = dest;
            pathStack.Push(current);

            while (previous[current] != -1)
            {
                current = previous[current];
                pathStack.Push(current);
            }

            if (pathStack.Count == 1)
            {
                Console.Write(" NO PATH");
                return;
            }

            while (pathStack.Count > 0)
            {
                Console.Write(" -> " + pathStack.Pop());
            }
        }

        public static void printShortestPathReverse(int dest, int[] previous)
        {
            int current = dest;
            Console.Write(dest + " <- ");

            while (previous[current] != -1)
            {
                current = previous[current];
                Console.Write(current + " <- ");
            }

        }
        //DJIKSTRA END

        static void Main(string[] args)
        {
            Console.WriteLine("******************BFS BÖLÜMÜ*************");
            BreadthFirstAlgorithm b = new BreadthFirstAlgorithm();
            Employee root = b.BuildEmployeeGraph();
            Console.WriteLine("Traverse Graph\n------");
            b.Traverse(root);
            Console.WriteLine("\nSearch in Graph\n------");
            Employee e = b.Search(root, "Eva");
            Console.WriteLine(e == null ? "Employee not found" : e.name);
            e = b.Search(root, "Brian");
            Console.WriteLine(e == null ? "Employee not found" : e.name);
            e = b.Search(root, "Soni");
            Console.WriteLine(e == null ? "Employee not found" : e.name);

            Console.WriteLine("******************AVL INSERT BÖLÜMÜ*************");
            AVL tree = new AVL();
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(2);
            tree.Delete(7);
            tree.DisplayTree();

            Console.WriteLine("******************DJIKSTRA BÖLÜMÜ*************");
            int N = 5;
            int SRC = 0;

            // int[][] cost	= new int[N][N];

            int[,] cost = {
       { INFINITY,    5,    3, INFINITY,    2},
       { INFINITY, INFINITY,    2,    6, INFINITY},
       { INFINITY,    1, INFINITY,    2, INFINITY},
       { INFINITY, INFINITY, INFINITY, INFINITY, INFINITY},
       { INFINITY,    6,   10,    4,    INFINITY}  };

            int[] distances = new int[N];

            int[] previous = Distance(N, cost, distances, SRC);

            for (int i = 0; i < distances.Length; ++i)
                if (distances[i] != INFINITY)
                    Console.WriteLine(distances[i]);
                else Console.WriteLine("INFINITY");

            int DEST = 1;
            Console.WriteLine("\n Shortest path from " + SRC + " to " + DEST + " (straight):");
            printShortestPathStraight(DEST, previous);
            Console.WriteLine("\n\n Shortest path from " + SRC + " to " + DEST + " (reverse) :");
            printShortestPathReverse(DEST, previous);
            Console.ReadLine();

           
            
         


        }
    }
}

