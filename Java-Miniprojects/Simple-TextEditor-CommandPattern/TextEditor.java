package texteditor;


import java.util.*; 
import java.text.CharacterIterator;
import java.text.StringCharacterIterator;
import java.awt.List;
import java.awt.event.*;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;
import static java.util.logging.Logger.global;

import javax.swing.Action;
import javax.swing.AbstractAction;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import static javax.swing.JFrame.EXIT_ON_CLOSE;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.filechooser.FileFilter;
import javax.swing.filechooser.FileNameExtensionFilter;
import javax.swing.text.BadLocationException;
import javax.swing.text.Document;

public class TextEditor extends JFrame{

    private JTextArea textArea = new JTextArea(20, 60);
    private JFileChooser fc = new JFileChooser();
    public Document doc = textArea.getDocument();
    TextEditor te = this; // TextEditor class'ından, comamnd design pattern'da kullanılmak üzere,
                          //te adında örnek bir nesne çekilmiştir.
    
    public TextEditor() {

        JScrollPane scrollPane = new JScrollPane(textArea, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
        
        FileFilter txtFilter = new FileNameExtensionFilter("Plain text", "txt");
        fc.setFileFilter(txtFilter);
        
        add(scrollPane);
        JMenuBar menuBar = new JMenuBar();
        setJMenuBar(menuBar);
        JMenu file = new JMenu("Tools");
        menuBar.add(file);
        
        file.add(Open);
        file.add(Save);
        file.add(Exit);
        file.add(deleteLast);
        file.add(applySingleTransposition);
        setDefaultCloseOperation(EXIT_ON_CLOSE);    
        pack();
        setLocationRelativeTo(null);
        setVisible(true);
    }
        
    Action applySingleTransposition = new AbstractAction("Apply Single Transposition") {
       @Override
        public void actionPerformed(ActionEvent e) {
           try {
               apply_single_transposition();
           } catch (IOException ex) {
               Logger.getLogger(TextEditor.class.getName()).log(Level.SEVERE, null, ex);
           }
    }  
    };
    
    Action deleteLast = new AbstractAction("Delete Last Letter") {

        @Override
        public void actionPerformed(ActionEvent e) {
            TextEditorInvoker remote = new TextEditorInvoker();                 // 83-87, Command desenini gerçekleştirimi
            UndoCommand undoletter = new UndoCommand(te);                       
            remote.setCommand(undoletter);
            remote.buttonWasPressed();
    } 
    };
    
    Action Open = new AbstractAction("Open File") {
        @Override
        public void actionPerformed(ActionEvent e) {
            if (fc.showOpenDialog(null) == JFileChooser.APPROVE_OPTION) {
            openFile(fc.getSelectedFile().getAbsolutePath());
            }
   
        }
    };
        
    Action Save = new AbstractAction("Save File") {
        @Override
        public void actionPerformed(ActionEvent e) {
            try {
                saveFile();
            } catch (IOException ex) {
                Logger.getLogger(TextEditor.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    };
    
    Action Exit = new AbstractAction("Quit") {
        @Override
        public void actionPerformed(ActionEvent e) {
            System.exit(0);
        }
    };
    
    public ArrayList<String> wordsList = new ArrayList();//words.txt den gelen    // Single transposition uygulanımı için gerekli          
    public ArrayList<String> textAreaList = new ArrayList();//textArea dan gelen  // veri yapılarının ve iteratorlerin oluşturulması
    public ListIterator<String> lit = textAreaList.listIterator();
    public ListIterator<String> lit2 = wordsList.listIterator();
    
    public void getdocstexts() throws FileNotFoundException, IOException {      // textArea ve words.txt içerisindeki girdilerin tutulması için yazılmış bir fonksiyondur.
        String text = textArea.getText();
        for (String x : text.split("-/*?!#[.,;]+")) {
            textAreaList.add(x);
        }
        File wordsFile = new File("words.txt");
        BufferedReader br = new BufferedReader(new FileReader(wordsFile));
        String st; 
        while ((st = br.readLine()) != null) {
            wordsList.add(st);     
        }
    }
    
/* Iterate fonksiyonunda hata alnımaktadır, fakat genel mantık şu şekildedir:
textArea'dan ve words.txt ten gelen veriler arrayList yapısında tutulmuştur yani wordsList ve textAreaList
ardından, hem Previous hem Next metotlarına sahip olduğu için ListIterator tercih edilmiştir.
İç içe while döngüleriyle textArea dan 1.kelimeyi alıp sabit tutup ikinci iteratorümüzle words.txt deki tüm kelimeleri geziyoruz
wordsList hasNext()metodu sağlayamadığı zaman kodun devamında süregeldiği şekilde .previous() ile geriye doğru diğer tutulan iterator
nesnesiyle kıyaslanır. Bu yüzden 149. satırdaki next() metodu 2 next işleminin birinde söz sahibidir.
gerekli if koşullarını geçen textAreaList ve wordsList elemanları single transposition eşleri olarak kabul ediliyor.
single transposition için if koşulları; 
kelimeler aynı uzunlukta olmalı, kümeleri denk olmalı, iki kelimenin birbirine index farkı sadece 2 konumda bulunmalı.   
*/
    public void Iterate() {                   // apply_single_transposition fonksiyonunu içinde, 
        while (lit.hasNext()) {               // girdilerin veri yapılarına çekilmesinden sonra iterator yapıları yardımıyla
            String templit = lit.next();      // gerekli kolleksiyon ve manipulasyon işlemlerinin yapıldığı fonksiyon
            Set<String> set = new HashSet<String>();
            CharacterIterator it = new StringCharacterIterator(templit);
            while (it.current() != CharacterIterator.DONE) {
                    set.add(String.valueOf(it.current())); 
                    it.next();
            }  
            while (lit2.hasNext()){
                String templit2 = lit2.next();
                Set<String> set2 = new HashSet<String>(); 
                CharacterIterator it2 = new StringCharacterIterator(templit2);
 
                while (it2.current() != CharacterIterator.DONE) {
                    set2.add(String.valueOf(it2.current()));
                    it2.next();
        }  
                if (templit.length() == templit2.length() && set.equals(set2)){
                    int count = 0;
                    for (int i = 0; i < templit.toCharArray().length ; i++) {
                        if (String.valueOf(templit.charAt(i)).equals(String.valueOf(templit2.charAt(i)))) {
                            count++;
                        }
                    }
                    if (count == templit.length() - 2) { 
                        textArea.setText(textArea.getText().replaceAll(templit,templit2));
  
                    }
                }
               
    }
            if (lit.hasNext()){
                String templit_ = lit.next();
                Set<String> set_ = new HashSet<String>();
                CharacterIterator it_ = new StringCharacterIterator(templit_);
                while (it_.current() != CharacterIterator.DONE) {
                    set_.add(String.valueOf(it_.current())); 
                    it_.next();
            }  
                while (lit2.hasPrevious()){
                    String templit2_ = lit2.previous();
                    Set<String> set2_ = new HashSet<String>(); 
                    CharacterIterator it2_ = new StringCharacterIterator(templit2_);
 
                while (it2_.current() != CharacterIterator.DONE) {
                    set2_.add(String.valueOf(it2_.current()));
                    it2_.next();
        }      
                    if (templit_.length() == templit2_.length()){
                    int count = 0;
                    for (int i = 0; i < templit_.toCharArray().length ; i++) {
                        if (String.valueOf(templit_.charAt(i)).equals(String.valueOf(templit2_.charAt(i)))) {
                            count++;
                        }}
                    if (count == templit_.length() - 2) {  
                        textArea.setText(textArea.getText().replaceAll(templit_,templit2_));}}               
}}}}
    
    public void apply_single_transposition() throws FileNotFoundException, IOException {
        getdocstexts();
        Iterate();
}

    public void deleteLast() throws BadLocationException {
        doc.remove(doc.getLength() - 1, 1);
    }
    
    public void openFile(String fileName) {
        FileReader fr = null;
        try {
            fr = new FileReader(fileName);
            textArea.read(fr, null);
            fr.close();
            setTitle(fileName);
            
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
            
    public void saveFile() throws FileNotFoundException, IOException {

                   
        if (fc.showSaveDialog(null) == JFileChooser.APPROVE_OPTION) {
            FileWriter fw = null;
            try {
                fw = new FileWriter(fc.getSelectedFile().getAbsolutePath() + ".txt");
                textArea.write(fw);
                fw.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        } 
    }  
}