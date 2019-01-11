/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
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
public class ChannelPanel extends javax.swing.JPanel {
    private final Connection connection;
    private HomeFrame homeFrame;
    
    /** Create a panel that contains a users channels
     * 
     */
    public ChannelPanel(HomeFrame homeFrame, final Connection connection) {
        this.connection = connection;
        this.homeFrame = homeFrame;
        setLayout(new javax.swing.BoxLayout(this, javax.swing.BoxLayout.Y_AXIS));
        //setLayout(new GridLayout(20,1,5,5));
        for(Channel channel: connection.getUser().getUserChannels()){
            JButton button = new JButton();
            button = new JButton(channel.getName());
            button.setAlignmentX(this.CENTER_ALIGNMENT);
            add(button);
            add(Box.createRigidArea(new Dimension(0,5)));
            button.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                connection.switchChannel(channel);
            }
            });
        }
    }
    /** Updates the channels for the user
     * 
     */
    public void updateChannels(){
        this.removeAll();
        for(Channel channel: connection.getUser().getUserChannels()){
            JButton button = new JButton();
            button = new JButton(channel.getName());
            button.setAlignmentX(this.CENTER_ALIGNMENT);
            add(button);
            add(Box.createRigidArea(new Dimension(0,5)));
            button.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                connection.switchChannel(channel);
            }
            });
        }
        revalidate();
        repaint();
        
    }

    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        setLayout(new javax.swing.BoxLayout(this, javax.swing.BoxLayout.LINE_AXIS));
    }// </editor-fold>//GEN-END:initComponents


    // Variables declaration - do not modify//GEN-BEGIN:variables
    // End of variables declaration//GEN-END:variables

}
