/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.Connection;
import java.awt.*;
import javax.swing.*;
import javax.swing.border.EtchedBorder;
/**
 *
 * @author stapl
 */
public class Popup extends JFrame {
    private JPanel jPanel;
    /** Creates a new pop up window
     * 
     * @param homeFrame the current home window.
     * @param connection the current connection.
     * @param type the type of pop up.
     * @param user It is a user list based pop up.
     */
    public Popup(HomeFrame homeFrame, Connection connection,  String type,  boolean user){
        super(type);
        
        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        this.setLocation(dim.width/2-this.getWidth()/2, dim.height/2-this.getHeight()/2);
        Container c = getContentPane();
        c.setLayout(new BorderLayout());
        EtchedBorder eb = new EtchedBorder (EtchedBorder.LOWERED);
        if(!user) {
            this.setSize(370,200);
            this.jPanel = new PopupPanel(homeFrame, connection, this, type);
            jPanel.setBorder(eb);
            c.add(jPanel,BorderLayout.CENTER);
            setVisible(true);
            setResizable(false);
            setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        } else {
            if(connection.getChannel()!=null){
                this.setSize(215, 220);
                this.jPanel = new UserPopupPanel(homeFrame, connection, this, type);
                jPanel.setBorder(eb);
                c.add(jPanel,BorderLayout.CENTER);
                setVisible(true);
                setResizable(false);
                setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            } else {
                this.dispatchEvent(new java.awt.event.WindowEvent(this, java.awt.event.WindowEvent.WINDOW_CLOSING));
            }
        }
        
        
    }
    
}
