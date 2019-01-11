/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.Connection;
import irc.Message;
import javax.swing.*;
import java.awt.*;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import javax.swing.border.*;

/**
 *
 * @author stapl
 */
public class HomeFrame extends JFrame {
    private Connection connection;
    private ChannelPanel channelPanel;
    private HomeMenu homeMenu;
    private View channelView;
    private UserPanel userPanel;
    /**
     * Creates new form ircFrame
     */
    public HomeFrame(Connection connection) {
        super("Homepage");
        this.connection = connection;
        this.homeMenu = new HomeMenu(this, connection);
        this.channelPanel = new ChannelPanel(this, connection);
        this.channelView = new View(connection);
        this.userPanel = new UserPanel(this, connection);
        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        this.setSize(800,600);
        this.setLocation(dim.width/2-this.getWidth()/2, dim.height/2-this.getHeight()/2);
        this.setExtendedState( this.getExtendedState()|JFrame.MAXIMIZED_BOTH );
        Container c = getContentPane();
        c.setLayout(new BorderLayout());
        EtchedBorder eb = new EtchedBorder (EtchedBorder.LOWERED);
        homeMenu.setBorder(eb);
        channelPanel.setBorder(eb);
        channelView.setBorder(eb);
        userPanel.setBorder(eb);
        c.add(homeMenu, BorderLayout.NORTH);
        c.add(channelPanel, BorderLayout.WEST);
        c.add(channelView, BorderLayout.CENTER);
        c.add(userPanel, BorderLayout.EAST);
        this.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent e) {
                connection.close();
                new MainFrame(connection.getData());
                setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            }
        });
        setVisible(true);
        //pack();
        
    }
    /** Stores the HomeFrame in the connection
     * 
     */
    public void initialize(){
        this.connection.setHomeFrame(this);
    }
    /** Messages the channel panel to update
     * 
     */
    public void updateChannels(){
        channelPanel.updateChannels();
    }
    /** Messages the channel view to set the starting text
     * 
     */
    public void setText(){
        channelView.setText();
    }
    /** Messages the channel view to update the text
     * 
     * @param message the message to be added.
     */
    public void updateText(Message message){
        channelView.updateText(message);
    }
    /** Gets the connection
     * 
     * @return the connection.
     */
    public Connection getConnection(){
        return connection;
    }
    /** Messages the user panel to update
     * 
     */
    public void updateUsers(){
        userPanel.updateUsers();
    }
    
}
