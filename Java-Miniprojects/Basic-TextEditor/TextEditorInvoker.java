/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package texteditor;

/**
 *
 * @author kerem
 */
public class TextEditorInvoker {
    Command slot;
     
    public TextEditorInvoker() {}
     
    public void setCommand(Command command) {
        slot = command;
    }
     
    public void buttonWasPressed() {
        slot.execute();
    }
}