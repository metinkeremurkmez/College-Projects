/* Metin Kerem ÜRKMEZ 05170000059 
   Strategy Pattern ile uygulanan 2.soru cevap kaynak kodları
   kod düzenli ve metodik ilerlediği için yorum satırlarına gerek duyulmamıştır.
*/

package nesne_final;

import java.util.Arrays;
import java.util.Date;
import java.util.Random;

public class Soru_2 { 
    public static void main(String args[]) throws InterruptedException {
    
    Random rd = new Random(); 
    int[] var = new int[100];
    for (int i = 0; i < var.length; i++) {
        var[i] = rd.nextInt(2021);
    } 
    
    Context ctx;
    
    Date past = new Date();
    ctx = new Context(new BubbleSort()); 
    ctx.arrange(var);  
    Date future = new Date();
    System.out.println("Time (milliseconds) = " + (future.getTime() - past.getTime()));
    
    Date past2 = new Date();
    ctx = new Context(new InsertionSort());
    ctx.arrange(var);
    Date future2 = new Date();
    System.out.println("Time (milliseconds) = " + (future2.getTime() - past2.getTime()));
    
    Date past3 = new Date();
    ctx = new Context(new QuickSort());
    ctx.arrange(var);
    Date future3 = new Date();
    System.out.println("Time (milliseconds) = " + (future3.getTime() - past3.getTime()));
    } 
}
    
    interface Strategy { 
        public void sort(int[] inputArray ); 
    }
    
    class BubbleSort implements Strategy {
        @Override 
        public void sort(int[] inputArray ) { 
            int n = inputArray.length;
            for( int i = 0; i < n; i++ ) {
                for( int j = 1; j < (n - i); j++ ) {
                    if( inputArray[j - 1] > inputArray[j] ) {
                        swap(j - 1, j, inputArray);
                    }
                }
            }
            System.out.println("Array is sorted using Bubble Sort Algorithm");
            System.out.println(Arrays.toString(inputArray));   
        }

        private void swap( int k, int l, int[] inputArray ){
            int temp = inputArray[k];
            inputArray[k] = inputArray[l];
            inputArray[l] = temp;  
        }
    } 
    class InsertionSort implements Strategy { 
        @Override 
        public void sort(int[] inputArray ) {
            for( int i = 1; i < inputArray.length; i++ ) {
                int tmp = inputArray[i];
                int j;
                for( j = i; j > 0; j-- ) {
                    if( inputArray[j - 1] < tmp ) 
                        break;
                inputArray[j] = inputArray[j - 1];
                }
                inputArray[j] = tmp;
        }
    System.out.println("Array is sorted using Insertion Sort Algorithm");
    System.out.println(Arrays.toString(inputArray));     
        }
    } 
    class QuickSort implements Strategy {
        @Override 
        public void sort(int[] inputArray ) {
            recursiveQuickSort(inputArray, 0, inputArray.length - 1);
            System.out.println("Array is sorted using QuickSort Sort Algorithm");
            System.out.println(Arrays.toString(inputArray));  
        }  
        public static void recursiveQuickSort(int[] array, int startIdx, int endIdx) {
            int idx = partition(array, startIdx, endIdx); 
            if (startIdx < idx - 1) { 
                recursiveQuickSort(array, startIdx, idx - 1); 
            } 
            if (endIdx > idx) { 
                recursiveQuickSort(array, idx, endIdx); 
            } 
        }  
        public static int partition(int[] array, int left, int right) {
            int pivot = array[left]; 
            while (left <= right) { 
                while (array[left] < pivot) {
                    left++;
                }
                while (array[right] > pivot) {
                    right--; 
            } 
            if (left <= right) {
                int tmp = array[left];
                array[left] = array[right]; 
                array[right] = tmp; 
                left++; 
                right--; 
            }
    }
    return left; 
        }
    }

    class Context { 
        private final Strategy strategy; 
        public Context(Strategy strategy) { 
            this.strategy = strategy; 
        } 
        public void arrange(int[] input) {   
            strategy.sort(input); 
        } 
    }