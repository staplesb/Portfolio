/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package irc;
import gui.HomeFrame;
/**
 *
 * @author stapl
 */
public class Connection {
    private final Data data;
    private User currentUser;
    private Channel currentChannel;
    private HomeFrame homeFrame;
    
    /** A constructor for a connection
     * 
     * @param data the data that it communicates with.
     */
    public Connection(Data data){
        currentUser = null;
        currentChannel = null;
        this.data = data;
    }
    /** Ensures that the connection can interact with the GUI
     * 
     * @param homeFrame the GUI it interacts with
     */
    public void setHomeFrame(HomeFrame homeFrame){
        this.homeFrame = homeFrame;
        data.addConnection(this);
    }
    /** Resets the text for a channel
     * 
     */
    public void setText(){
        homeFrame.setText();
    }
    /** Updates the messages of the GUI
     * 
     * @param message the new message to be added to the GUI.
     */
    public void updateMessage(Message message){
        homeFrame.updateText(message);
    }
    /** Updates the channels of the GUI
     * 
     */
    public void updateChannels(){
        homeFrame.updateChannels();
    }
    /** Updates the users of the GUI
     * 
     */
    public void updateUsers(){
        homeFrame.updateUsers();
    }
    /** Messages data class to save users and channels
     * 
     */
    public void close(){
        data.close(); 
        data.removeConnection(this);
    }
    /** Messages data to create a channel
     * 
     * @param channelName the name of the created channel
     */
    public void createChannel(String channelName){ 
        data.createChannel(this, channelName);
    }
    /** Messages data to delete a channel
     * 
     * @param channel the channel to be deleted.
     * @return The channel was deleted.
     */
    public boolean deleteChannel(Channel channel){
        if(currentChannel!=null){
            boolean r = data.deleteChannel(channel);
            currentChannel = null;
            return r;
        }
        return false;
    }
    /** Messages data to update the current channel
     * 
     * @param channel the channel to be changed to.
     * @return Already in the channel.
     */
    public boolean switchChannel(Channel channel){ 
        if(currentChannel!= null && currentChannel.equals(channel)) 
            return true; //already in channel
        currentChannel = data.findChannel(channel);
        updateUsers();
        setText();
        return false;
    }
    /** Messages data to join a channel
     * 
     * @param invite the channel specific invite.
     * @return The channel was successfully joined.
     */
    public boolean joinChannel(String invite){
        return data.joinChannel(currentUser, invite);
    }
    /** Messages data to create a user
     * 
     * @param email the users email.
     * @param userName the users username.
     * @param password the users password.
     * @return The user already exists.
     */
    public boolean createUser(String email, String userName, String password){
        boolean r = data.createUser(email, userName, password);
        currentUser = data.login(email, password);
        return r;
    }
    /** Messages data to delete a user
     * 
     * @param email the users email.
     * @param password the users password.
     * @return The user was successfully deleted.
     */
    public boolean deleteUser(String email, String password){
        return data.deleteUser(email, password);
    }
    /** Messages data to remove a user from the current channel
     * 
     * @param user the user to be removed
     */
    public void kickUser(User user){
        data.kickUser(this, user);
        if(user.getEmail().equals(currentUser.getEmail())){
            currentChannel = null;
        }
    }
    /** Messages data to login and updates the current user
     * 
     * @param email the input email address.
     * @param password the input password.
     * @return The user was successfully logged in.
     */
    public boolean login(String email, String password){
        currentUser = data.login(email, password);
        return currentUser!=null;
    }
    /** Logs the user out.
     * 
     */
    public void logout(){
        currentUser = null;
        currentChannel = null;
    }
    /** Messages data to update the current user
     * 
     * @return The current user.
     */
    public User getUser(){
        currentUser = data.findUser(currentUser);
        return currentUser;
    }
    /** Messages data to update the current channel
     * 
     * @return the current channel
     */
    public Channel getChannel() {
        if(currentChannel!=null){
            currentChannel = data.findChannel(currentChannel);
        }
        return currentChannel;
    }
    /** Gets the invite for the current channel
     * 
     * @return the invite for the current channel.
     */
    public String getInvite() {
        return currentChannel.getInvite();
    }
    /** Gets the data class for the connection
     * 
     * @return the data class
     */
    public Data getData(){
        return data;
    }
    /** Messages data to add a message to the current channel
     * 
     * @param message the message to be added. 
     */
    public void addMessage(Message message){
        data.addMessage(currentChannel, message);
    }
    /** Sets the current user. Not used in the GUI.
     * 
     * @param user the desired user.
     */
    public void setUser(User user){
        this.currentUser = user;
    }
    /** Sets the current channel. Not used in the GUI.
     * 
     * @param channel the desired channel.
     */
    public void setChannel(Channel channel){
        this.currentChannel = channel;
    }
    /** Changes connection into a string. Mainly for debugging.
     * 
     * @return The current user and channel.
     */
    public String toString(){
        if(currentUser==null)
        {
            return "Logged out";
        }
        else if(currentChannel==null)
        {
            return "User: " + currentUser + " is at the homepage";
        }
        return "User: " + currentUser + " Channel: " + currentChannel;
    }
}
