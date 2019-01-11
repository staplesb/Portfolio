/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.User;
import irc.Role;
import irc.Connection;
import irc.Channel;
import java.awt.event.*;
import javax.swing.JButton;
import javax.swing.*;
import java.awt.*;
/**
 *
 * @author stapl
 */
public class UserPanel  extends javax.swing.JPanel {
    private Connection connection;
    private HomeFrame homeFrame;
    /** Creates a user panel
     * 
     */
    public UserPanel(HomeFrame homeFrame, final Connection connection) {
        //initComponents();
        this.connection = connection;
        this.homeFrame = homeFrame;
        setLayout(new javax.swing.BoxLayout(this, javax.swing.BoxLayout.Y_AXIS));
        if(connection.getChannel()!=null){
            for(Role role: connection.getChannel().getMembers()){
                JLabel userLabel = new JLabel();
                userLabel.setText(role.getUser().getUsername());
                userLabel.setAlignmentX(this.LEFT_ALIGNMENT);
                this.add(userLabel);
                add(Box.createRigidArea(new Dimension(0,2)));
            }
        }
           
     }
     /** Updates the user panel
      * 
      */
     public void updateUsers(){
        this.removeAll();
        if(connection.getChannel()!=null){
            for(Role role: connection.getChannel().getMembers()){
                 JLabel userLabel = new JLabel();
                    userLabel.setText(role.getUser().getUsername());
                    userLabel.setAlignmentX(this.LEFT_ALIGNMENT);
                    this.add(userLabel);
                    add(Box.createRigidArea(new Dimension(0,2)));
            }
        }
        revalidate();
        repaint();
    }
     
}