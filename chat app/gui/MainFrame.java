/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package gui;
import irc.Data;
import irc.Connection;
import javax.swing.JFrame;
/**
 *
 * @author stapl
 */
public class MainFrame extends JFrame {
    private JFrame jFrame;
    /** Creates a window
     * 
     * @param data the data to be used for creating a connection.
     */
    public MainFrame(Data data){
        jFrame = new LoginFrame(data);
    }
    /** Creates a window
     * 
     * @param connection the connection to be used to create a home or create user window.
     * @param state the type of window to create.
     */
    public MainFrame(Connection connection, int state){
        if(state==1){
            HomeFrame homeFrame = new HomeFrame(connection);
            homeFrame.initialize();
            jFrame = homeFrame;
        }
        else if (state==2){
            jFrame = new CreateUserFrame(connection);
        }
    }
}
