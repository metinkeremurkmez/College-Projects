/* Metin Kerem ÜRKMEZ 05170000059 
   Thread ile uygulanan 1.soru cevap kaynak kodları
   Buffer yapısı kullanılmamıştır.
*/

package nesne_final;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.Arrays;
import java.util.Scanner;

public class Soru_1 {
    // boyutlar için değişkenler.
    public static int M = 3;
    public static int K = 2;
    public static int N = 3;
    //A, B, C Dizilerini ve Dizi || WorkerThreads oluşturuyoruz.
    public static int [][] A ;
    public static int [][] B ;
    public static int [][] C = new int [M][N];
    public static WorkerThread [][] Threads = new WorkerThread[3][3];
    
    
    public static void main(String[] args) throws FileNotFoundException { 
        Scanner sc = new Scanner(new BufferedReader(new FileReader("Matris1.txt")));        // matris txtlerini okuyoruz ve Soru_1.A & Soru_1.B için atama yapıyoruz.
        int rows = 3;
        int columns = 2;
        A = new int[rows][columns];
        while(sc.hasNextLine()) {
            for (int i=0; i<A.length; i++) {
                String[] line = sc.nextLine().trim().split(" ");
                for (int j=0; j<line.length; j++) {
                A[i][j] = Integer.parseInt(line[j]);
                }
            }
        }
        Scanner sc2 = new Scanner(new BufferedReader(new FileReader("Matris2.txt")));
        int rows2 = 2;
        int columns2 = 3;
        B = new int[rows2][columns2];
        while(sc2.hasNextLine()) {
            for (int i=0; i<B.length; i++) {
                String[] line2 = sc2.nextLine().trim().split(" ");
                for (int j=0; j<line2.length; j++) {
                    B[i][j] = Integer.parseInt(line2[j]);
                }
            }
        }

        
        // Çalışmak üzere 9iş parçacığı oluşturulur. Her bir iş parçacığı bir Matris Değeri hesaplar ve bunu C matrisine taşır.
        for (int i = 0; i<M; i++){
            for (int j=0; j<N; j++){
                Threads[i][j] = new WorkerThread(i,j,A,B,C);
                Threads[i][j].start();
            }
        }
        System.out.println("Result Matrix C[][]:");
        for (int[] row : C) {
            System.out.println(Arrays.toString(row)); 
        } 
    }
}
