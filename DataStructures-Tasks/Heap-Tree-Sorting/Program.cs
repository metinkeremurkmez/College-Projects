using System;
using System.Collections;
using System.Collections.Generic;

namespace Proje3B
{
    class Program
    {
        public static void kabarcikSirala(int[] siralanacakDizi)
        {

            int i = 1, j, deger;
            int diziAdet = siralanacakDizi.Length;
            while (i < diziAdet)
            {
                j = diziAdet - 1;
                while (j >= 1)
                {
                    if (siralanacakDizi[j - 1] > siralanacakDizi[j])
                    {
                        deger = siralanacakDizi[j];
                        siralanacakDizi[j] = siralanacakDizi[j - 1];
                        siralanacakDizi[j - 1] = deger;
                    }
                    j--;
                }
                i++;
            }
        }

        public static void diziYazdir(int[] dizi)
        {
            Console.WriteLine("Bubble sort sıralaması uygulandı..");
            for (int i = 0; i < dizi.Length; i++)
            {
                Console.WriteLine(dizi[i]);
            }
            Console.ReadKey();

        }


        private static void Quick_Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;


            String[] turlar = { "Cunda Kazdağları,100,1,0,Ayvalık,Cunda Adası,Kazdağları", "Batı Karadeniz,699,3,2,Bolu,Yedigöller,Gölcük,Abant,Sapanca,Karabük,Bartın", "Doğu Karadeniz,1599,5,4,Samsun,Ordu,Giresun,Trabzon,Akçaabat,Rize,Artvin,Batum,Zilkale,Ayder,Karagöl,Sümela Manastırı", "Kapadokya,499,3,2,Ihlara Vadisi,Narlı Göl,Yer altı Şehri,Nevşehir,Dervent Vadisi", "Selçuk-Efes,119,2,1,Selçuk,Efes Antik Kent,Meryemana,Şirince,Yedi Uyuyanlar", "Doğu Ekspresi,3379,3,2,Sarıkamış,Şehitler Anıtı,Kars Kalesi,Taş Köprü,12 Havariler Camii", "Kiev,1995,4,3,İkinci Dünya Savaşı Müzesi,Pecerskaya Lavra Manastırı,Mikro Minyatür Müzesi" };
            String[] splitNesne = null;
            List<Tur> turNesneler = new List<Tur>();
            

            foreach (String tur in turlar)
            {
                
                splitNesne = tur.Split(",");
                string[] rota = new string[splitNesne.Length-4];
                
                int x = Int32.Parse(splitNesne[1]);
                int y = Int32.Parse(splitNesne[2]);
                int z = Int32.Parse(splitNesne[3]);

                for ( int i = 4; i < splitNesne.Length; i++)
                {
                    rota[i - 4] = splitNesne[i];
                }

                turNesneler.Add(new Tur(splitNesne[0],x,y,z,rota));    
            }

            MyTree myTree = new MyTree();
            foreach (Tur x in turNesneler)
            {
               
                myTree.insert(x);             
            }
            Console.WriteLine("Mevcut turlar ..");
            myTree.preOrder(myTree.getRoot());

            Console.WriteLine("\n");
            Console.WriteLine("Tur özeti hashtable'a yükleniyor .. ");
            Hashtable katalog = new Hashtable();
            foreach (var m in turNesneler)
            {
                katalog.Add(m.turAdi, m.fiyat);
            }
            ICollection c = katalog.Keys;
            Console.WriteLine("\n");
            Console.WriteLine("Tur indirimi uygulanıyor .. ");
            foreach (var tur in turNesneler)
            {
                if (tur.fiyat > 900)
                {
                    tur.fiyat = tur.fiyat * 90 / 100;

                    katalog[tur.turAdi] = tur.fiyat;
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine("Güncellenmiş tur özeti hashtable'dan printleniyor .. ");
            foreach (DictionaryEntry oge in katalog) Console.WriteLine(oge.Key + "-" + oge.Value);

            Heap myHeap = new Heap(7);            
            
            foreach (var m in turNesneler)
            {
                myHeap.insert(m.fiyat);
            }

            Console.WriteLine("\n");
            myHeap.displayHeap();

            Console.WriteLine("\n");

            int[] dizi = { 6, 12, 24, 3, 8, 4 };

            Console.WriteLine("Quick sort sıralaması uygulandı..");
            Quick_Sort(dizi, 0, dizi.Length - 1);

            Console.WriteLine();
            foreach (var item in dizi)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();


            kabarcikSirala(dizi);
            diziYazdir(dizi);

            














        }
    



        }
    }

