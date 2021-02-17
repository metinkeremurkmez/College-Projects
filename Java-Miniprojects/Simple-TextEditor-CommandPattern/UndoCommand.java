/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package texteditor;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.text.BadLocationException;
import javax.swing.text.Document;

/**
 *
 * @author kerem
 */
public class UndoCommand implements Command {
    TextEditor textEditor;

    public UndoCommand(TextEditor textEditor) {
        this.textEditor = textEditor;
    }
    @Override
    public void execute() {
        try {
            textEditor.deleteLast();

        } catch (BadLocationException ex) {
            Logger.getLogger(UndoCommand.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }
    
}
