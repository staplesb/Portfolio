/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.Connection;
import java.awt.*;
import javax.swing.*;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Toolkit;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EtchedBorder;

/**
 *
 * @author stapl
 */


public class CreateUserFrame extends JFrame {
    private Connection connection;
    private CreateUser createUser;
    /** Creates a user window
    * 
    */
    public CreateUserFrame (Connection connection) {
        //initComponents();
        super("Create User");
        this.createUser = new CreateUser(this, connection);
        this.setSize(300,340);
        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        this.setLocation(dim.width/2-this.getWidth()/2, dim.height/2-this.getHeight()/2);
        Container c = getContentPane();
        c.setLayout(new BorderLayout());
        EtchedBorder eb = new EtchedBorder (EtchedBorder.LOWERED);
        createUser.setBorder(eb);
        c.add(createUser,BorderLayout.CENTER);
        setVisible(true);
        setResizable(false);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
    }
}
