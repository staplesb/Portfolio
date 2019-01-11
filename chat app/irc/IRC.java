/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package irc;
import gui.MainFrame;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;

/**
 *
 * @author stapl
 */
public class IRC {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        //Data.getInstance(true);
        MainFrame mainFrame = new MainFrame(Data.getInstance());
        MainFrame another = new MainFrame(Data.getInstance());
    }
    
}
