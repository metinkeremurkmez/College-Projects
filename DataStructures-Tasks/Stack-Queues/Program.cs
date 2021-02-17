using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Proje2B
{

    class Program
    {
        public class MyStack // int veri tipi saklayabilen Stack 
        {
            public int maxSize;
            public int[] myArray;
            public int top;

            public MyStack(int size)
            {
                maxSize = size;
                myArray = new int[maxSize];
                top = -1;

            }
            public void push(int number)
            {
                myArray[++top] = number;

            }
            public int pop()
            {
                return myArray[top--];
            }
            public bool isEmpty()
            {
                return top == -1;
            }
        }


        public class MyQueue // int ve double veri tipi saklayabilen Queue
        {
            public int maxSize;
            public int[] queArray;
            public double[] queArray_;
            public int front;
            public int rear;
            public int nItems;

            public MyQueue(int s)
            {
                maxSize = s;
                queArray = new int[maxSize];
                queArray_ = new double[maxSize];
                front = 0;
                rear = -1;
                nItems = 0;
            }

            public void insert(int j)
            {
                if (rear == maxSize - 1)
                    rear = -1;
                queArray[++rear] = j;
                nItems++;
            }
            public void insert(double j)
            {
                if (rear == maxSize - 1)
                    rear = -1;
                queArray_[++rear] = j;
                nItems++;
            }

            public int remove()
            {
                int temp = queArray[front++];
                if (front == maxSize)
                    front = 0;
                nItems--;
                return temp;
            }
            public double Remove()
            {
                double temp = queArray_[front++];
                if (front == maxSize)
                    front = 0;
                nItems--;
                return temp;
            }

            public bool isEmpty()
            {
                return (nItems == 0);
            }



            public class MyPriorityQueue // Il tipinde nesne ve double veri tipi saklayabilen PriorityQueue
            {
                public int maxSize;
                public Il [] queArray;
                public double[] queArray_;
                public int nItems;

                public MyPriorityQueue(int s)
                {
                    maxSize = s;
                    queArray = new Il[maxSize];
                    queArray_ = new double[maxSize];
                    nItems = 0;
                }

                public void insert(Il item) //alfabetik azalan sırada il nesnesi ekleyen metod
                {
                    int c;
                    if (nItems == 0) // item yoksa
                        queArray[nItems++] = item; // başa koy
                    else 
                    {

                        for (c = nItems - 1; c >= 0; c--) // sondan başlar                    
                        {

                            if (string.CompareOrdinal(queArray[c].ilAdi, item.ilAdi) < 0)
                            { // yeni item büyükse 
                                queArray[c + 1] = queArray[c]; // kaydır

                            }
                            else //yeni item küçükse
                                break;
                        }
                        queArray[c + 1] = item;
                        nItems++;
                    } 
                }

                public void insert(double item_) // artan sırada double verileri ekleyen metod (Method Overloading)
                {
                    int t;
                    if (nItems == 0) 
                        queArray_[nItems++] = item_; 
                    else 
                    {
                        for (t = nItems - 1; t >= 0; t--) 
                        {
                            if (item_ > queArray_[t]) 
                                queArray_[t + 1] = queArray_[t]; 

                            else 
                                break; 
                        } 
                        queArray_[t + 1] = item_; 
                        nItems++;
                    } 
                }
                public double Remove()
                { return queArray_[--nItems]; }
                               
                public Il remove() 
                { return queArray[--nItems]; }

                public bool isEmpty()
                { return (nItems == 0); }

            }

            static void Main(string[] args)
            {

                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;



                string[] ilAdı = { "Edirne", "Istanbul", "Kırklareli", "Kocaeli", "Denizli", "Izmir", "Manisa", "Muğla", "Adana", "Antalya", "Mersin", "Ankara", "Bolu", "Trabzon", "Erzurum" };
                int[] havaSıcaklığı = { 21, 22, 21, 25, 24, 21, 22, 18, 27, 23, 26, 18, 17, 20, 8 };
                List<Il> GList = new List<Il>(); // il nesneleri içeren generic list
                List<List<Il>> GList_main = new List<List<Il>>(); // farklı sayıda il nesnesi içeren generic listleri kapsayan genercic list

                for (int i = 0; i < havaSıcaklığı.Length; i++)
                {
                    GList.Add(new Il(ilAdı[i], havaSıcaklığı[i]));
                }

                int sayac = 0;
                int counter = 0;
                int son = GList.Count;

                Console.WriteLine("**İl nesnelerinin veri yapısından çıktıları**");
                for (int m = 1; ; m++, m++)
                {
                    for (int j = 1; j <= m; j++)
                    {
                        GList_main.Add(new List<Il>());
                        GList_main[counter].Add(GList[sayac]);
                        Console.WriteLine(GList_main[counter][j - 1].ilAdi + "  Slot:" + m);
                        sayac++;
                        if (son <= sayac) break;
                    }
                    counter++;
                    if (son <= sayac) break;

                }
                
                Console.WriteLine();
                

                MyStack iller_s = new MyStack(havaSıcaklığı.Length);
                Console.WriteLine("**Hava sıcaklığı verileri stack yapısına geçiriliyor..**");
                for (int j = 0; j < havaSıcaklığı.Length; j++)
                {
                    iller_s.push(havaSıcaklığı[j]);
                }
                Console.WriteLine("**Hava sıcaklığı verilerinin stack yapısından çıktıları**"); 
                while (!iller_s.isEmpty())
                {
                    int donkey = iller_s.pop();
                    Console.WriteLine(donkey);
                }

                Console.WriteLine();

                
                MyQueue iller_q = new MyQueue(havaSıcaklığı.Length);
                Console.WriteLine("**Hava sıcaklığı verileri queue yapısına geçiriliyor..**");
                for (int j = 0; j < havaSıcaklığı.Length; j++)
                {
                    iller_q.insert(havaSıcaklığı[j]);
                }
                Console.WriteLine("**Hava sıcaklığı verilerinin queue yapısından çıktıları**");                
                    while (!iller_q.isEmpty())
                {
                    int donkey = iller_q.remove();
                    Console.WriteLine(donkey);
                }

                Console.WriteLine();


                MyPriorityQueue iller_pq = new MyPriorityQueue(GList.Count);
                Console.WriteLine("**Priority Queue il nesneleri alfabetik azalan sırada ekleme işlemi yapılıyor..**");
                for (int j = 0; j < GList.Count; j++)
                {
                    iller_pq.insert(GList[j]);
                }

                Console.WriteLine("**Priority Queue il nesneleri silme işlemi çıktıları**"); 
                while (!iller_pq.isEmpty())
                {
                    Console.WriteLine(iller_pq.remove().ilAdi);
                }

                Console.WriteLine();


                Random r = new Random();
                double toplam = 0;
                double[] numList = new double[25];
                MyPriorityQueue bee = new MyPriorityQueue(25);

                Console.WriteLine("**Priority Queue double veri olan müşteri işlem sürelerinin artan sırada ekleme işlemi yapılıyor..**");
                for (int i = 0; i < 25; i++)
                {
                    numList[i] = r.NextDouble() * (125.00 - 15.00) + 15.00;

                    if (i == 0)
                    {
                        bee.insert(numList[i]);
                        toplam = toplam + numList[i];
                        continue;
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        numList[i] = numList[i] + numList[j];
                    }
                    bee.insert(numList[i]);
                    toplam = toplam + numList[i];
                }
                Console.WriteLine("**Priority Queue double veri olan müşteri süreleri çıktıları**"); 
                while (!bee.isEmpty())
                {
                    Console.WriteLine(bee.Remove());
                }
                Console.WriteLine("**Priority Queue double veri olan müşteri süreleri ortalaması**");
                Console.WriteLine(toplam / numList.Length);

                Console.WriteLine();


                Random r_ = new Random();
                double toplam_ = 0;
                double[] numList_ = new double[25];
                MyQueue bee_ = new MyQueue(25);

                Console.WriteLine("**Queue double veri olan müşteri işlem sürelerinin artan sırada ekleme işlemi yapılıyor..**");
                for (int i = 0; i < 25; i++)
                {
                    numList_[i] = r_.NextDouble() * (125.00 - 15.00) + 15.00;    

                if (i == 0)
                {
                        bee_.insert(numList_[i]);
                        toplam_ = toplam_ + numList_[i];
                        continue;
                }
                for (int j = i-1; j >= 0; j--)
                {
                    numList_[i] = numList_[i] + numList_[j];
                }
                    bee_.insert(numList_[i]);
                    toplam_ = toplam_ + numList_[i];
                }
                Console.WriteLine("**Queue double veri olan müşteri süreleri çıktıları**");
                while (!bee_.isEmpty())
                {
                    Console.WriteLine(bee_.Remove());
                }
                Console.WriteLine("**Queue double veri olan müşteri süreleri ortalaması**");
                Console.WriteLine(toplam_ / numList_.Length);

                Console.WriteLine();
                
            }
    }
        }
        
    }
