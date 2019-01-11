/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package irc;
import java.util.ArrayList;

/**
 *
 * @author coach
 */
public class User implements java.io.Serializable
{
    
    private String password;
    private String username;
    private String email;
    private ArrayList<Channel> userChannels;
    
    /** A null constructor for user
     * 
     */
    public User()
    {
        this.password = "";
        this.username = "";
        this.email = "";
        this.userChannels = new ArrayList();
    }
    /** A constructor for user
     * 
     * @param email the users email.
     * @param userName the users username.
     * @param password the users password.
     */
    public User(String email, String userName, String password)
    {
        this.password = password;
        this.username = userName;
        this.email = email;
        this.userChannels = new ArrayList();
    }
    /** Gets the users password
     * 
     * @return the users password.
     */
    public String getPassword()
    {
        return password;
    }
    /** sets the users password
     * 
     * @param newPassword the users password.
     */
    public void setPassword(String newPassword)
    {
        password = newPassword;
    }
    /** Gets the users username
     * 
     * @return the users username.
     */
    public String getUsername()
    {
        return username;
    }
    /** Sets the users username
     * 
     * @param newUsername the users username.
     */
    public void setUsername(String newUsername)
    {
        username = newUsername;
    }
    /** Gets the users email
     * 
     * @return the users email.
     */
    public String getEmail()
    {
        return email;
    }
    /** Sets the users email
     * 
     * @param newEmail the users email.
     */
    public void setEmail(String newEmail)
    {
     email = newEmail;
    }
    /** Gets the users list of channels
     * 
     * @return the users channels
     */
     public ArrayList<Channel> getUserChannels()
     {
         return userChannels;
     }
     /** Adds a channel to a users channel list
      * 
      * @param newChannel the channel to be added.
      */
     public void addChannel(Channel newChannel)
     {
         userChannels.add(newChannel);
     }
     /** Removes a channel from a users channel list
      * 
      * @param channel the channel to be removed. 
      */
     public void removeChannel(Channel channel)
     {
         userChannels.remove(channel);
     }
     /** Changes a user into a string
      * 
      * @return the users username. 
      */
     public String toString(){
         return username;
     }
}
