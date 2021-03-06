/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.Connection;
import irc.Role;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
/**
 *
 * @author stapl
 */
public class UserPopupPanel extends javax.swing.JPanel {
    private String type;
    private Popup popup;
    private Connection connection;
    private HomeFrame homeFrame;
    
    /**
     * Creates a user list based pop up
     */
    public UserPopupPanel(HomeFrame homeFrame, Connection connection, Popup popup, String type) {
        this.popup = popup;
        this.type = type;
        this.connection = connection;
        this.homeFrame = homeFrame;
        initComponents();
        
        String [] userList = new String[connection.getChannel().getMembers().size()];
        Role [] roleList = new Role[connection.getChannel().getMembers().size()];
        int i = 0;
        for(Role role: connection.getChannel().getMembers()){
            userList[i] = role.getUser().getUsername();
            roleList[i] = role;
            i++; 
        }
        jComboBox1.setModel(new DefaultComboBoxModel(userList));
        if(type.equalsIgnoreCase("kick")){
            jButton1.setText("kick");
            jButton1.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                connection.kickUser(roleList[jComboBox1.getSelectedIndex()].getUser());
                popup.dispatchEvent(new java.awt.event.WindowEvent(popup, java.awt.event.WindowEvent.WINDOW_CLOSING));
            }
            });
        } else if(type.equalsIgnoreCase("leave")){
            jButton1.setText("leave");
            jLabel1.setText("Are you sure?");
            jComboBox1.setModel(new DefaultComboBoxModel(new String [] {"No", "Yes"}));
            jButton1.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                if(jComboBox1.getSelectedIndex()==1){
                    connection.kickUser(connection.getUser());
                    connection.setChannel(null);
                }
                popup.dispatchEvent(new java.awt.event.WindowEvent(popup, java.awt.event.WindowEvent.WINDOW_CLOSING));
            }
            });
        }
        
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jComboBox1 = new javax.swing.JComboBox();
        jLabel1 = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();

        jComboBox1.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Item 1", "Item 2", "Item 3", "Item 4" }));

        jLabel1.setText("Select a user");

        jButton1.setText("jButton1");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(this);
        this.setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap(60, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.CENTER)
                    .addComponent(jLabel1)
                    .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jButton1))
                .addContainerGap(60, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(30, 30, 30)
                .addComponent(jLabel1)
                .addGap(30, 30, 30)
                .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(30, 30, 30)
                .addComponent(jButton1)
                .addGap(30, 30, 30))
        );
    }// </editor-fold>//GEN-END:initComponents


    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButton1;
    private javax.swing.JComboBox jComboBox1;
    private javax.swing.JLabel jLabel1;
    // End of variables declaration//GEN-END:variables
}
