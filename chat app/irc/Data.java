/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package irc;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

/**
 *
 * @author stapl
 */
public class Data {
    private static Data dataInst = null;
    private ArrayList<Connection> allConnections;
    private ArrayList<User> allUsers;
    private ArrayList<Channel> allChannels;
    /** A private constructor for data (singleton)
     * 
     *@param reset Set the user and channel lists to empty
     */
    private Data(boolean reset){ 
        allChannels = new ArrayList();
        allUsers = new ArrayList();
        allConnections = new ArrayList();
        if(reset)
            close();
        initialize();
    }
    /** Gets the singleton instance
     * 
     * @param reset Set the user and channel lists to empty
     * @return The single instance of data
     */
    public static Data getInstance(boolean reset){
        if(dataInst==null)
            dataInst = new Data(reset);
        return dataInst;
    }
    /** Gets the singleton instance
     * 
     * @return The single instance of data
     */
    public static Data getInstance(){
        if(dataInst==null)
            dataInst = new Data(false);
        return dataInst;
    }
    
    /** Read in all users and channels. Called in the constructor.
     * 
     */
    public void initialize(){//call after constructor
        this.allChannels = (ArrayList) readFromSer("channels.ser");
        this.allUsers = (ArrayList) readFromSer("users.ser");
    }
    /** Save all users and channels. Called when connection closes.
     * 
     */
    public void close(){//call when data ends
        writeToSer(allChannels, "channels.ser");
        writeToSer(allUsers, "users.ser");
    }
    /** Removes a user from a specific channel
     * 
     * @param connection for the current user and channel.
     * @param user the user to be kicked.
     */
    public void kickUser(Connection connection, User user){
        boolean setNull = true;
        for(Channel channel: allChannels){
            if(channel.getInvite().equals(connection.getChannel().getInvite())){
                setNull = channel.kick(user, connection.getUser());
                break;
            }
        }
        for(Connection someConnection: allConnections){
            someConnection.getChannel();
            if(someConnection.getUser().getEmail().equals(user.getEmail())){
                someConnection.getUser();
                someConnection.updateChannels();
                if(setNull)
                    someConnection.setChannel(null);
                someConnection.setText();
            }
            someConnection.updateUsers();
            
        }
    }
    /** Creates a channel and stores it
     * 
     * @param connection the connection used to create the channel.
     * @param channelName the name of the channel.
     */
    public void createChannel(Connection connection, String channelName){
        Channel channel = new Channel(channelName, connection.getUser());
        allChannels.add(channel);
        for(User user: allUsers){
            if(user.getEmail().equals(connection.getUser().getEmail()))
                user.addChannel(channel);
        }
        connection.updateChannels();
    }
    /** Deletes a channel 
     * 
     * @param channel the channel to be deleted.
     * @return The channel was successfully deleted
     */
    public boolean deleteChannel(Channel channel){
        boolean r = false;
        for(User user: allUsers){
            for(Channel someChannel: user.getUserChannels()){
                if(channel.getInvite().equals(someChannel.getInvite())){
                    user.removeChannel(someChannel);
                    break;
                }
            }
        }
        r = allChannels.remove(channel);
        for(Connection connection: allConnections){
            connection.getChannel();
            connection.updateChannels();
            if(connection.getChannel()==null){
                connection.updateUsers();
                connection.setText();
            }
        }
        return r;
    }
    /** Adds a user to preexisting channel
     * 
     * @param user the user being added to a channel.
     * @param invite the unique invite to the channel.
     * @return The user joined the channel successfully
     */
    public boolean joinChannel(User user, String invite){
        boolean r = false;
        for(Channel channel: allChannels){
            if(channel.getInvite().equals(invite)){ 
                if(channel.addUser(user)){
                    user.addChannel(channel);
                    r = true;
                    break;
                }
            }
        }
        for(Connection connection: allConnections){
            connection.getChannel();
            connection.updateChannels();
            connection.updateUsers();
            connection.setText();
        }
        return r;
    }
    /** Creates a new user and stores it
     * 
     * @param email the users email.
     * @param userName the users username.
     * @param password the users password.
     * @return The email is already in use.
     */
    public boolean createUser(String email, String userName, String password){
        for(User user: allUsers){
            if (user.getEmail().equals(email))                
                return true;//already exists 
        }
        User user = new User(email, userName, password);
        allUsers.add(user);
        return false;
    }
    /** Deletes a user. Not used in GUI.
     * 
     * @param email the users email.
     * @param password the users password.
     * @return The user was successfully deleted
     */
    public boolean deleteUser(String email, String password){
        for (User user: allUsers){
            if (user.getEmail().equals(email)){
                if (user.getPassword().equals(password)){
                    allUsers.remove(user);
                    return true;
                }
            }
        }
        return false; //either username of password doesn't match
    }
    /** Finds a user
     * 
     * @param email the users email.
     * @param password the users password.
     * @return The user to be logged in
     */
    public User login(String email, String password){
        for (User user: allUsers){
            if (user.getEmail().equals(email)){
                if (user.getPassword().equals(password)){
                    return user;
                }
            }
        }
        return null; //either username or password doesn't match
    }
    /** Adds a connection to the list of connections
     * 
     * @param connection the connection to be added.
     * @return The connection was added successfully
     */
    public boolean addConnection(Connection connection){
        return allConnections.add(connection);
    }
    /** Removes a connection to the list of connections
     * 
     * @param connection the connection to be removed.
     * @return The connection was removed successfully
     */
    public boolean removeConnection(Connection connection){
        return allConnections.remove(connection);
    }
    /** Adds a message to the specified channel and stores it
     * 
     * @param channel the channel for the message to be added to.
     * @param message the message to be added.
     */
    public void addMessage(Channel channel, Message message){
        for(Channel someChannel: allChannels){
            if(channel.getInvite().equals(someChannel.getInvite())){
                someChannel.addMessage(message);
            }
        }
        for(Connection connection: allConnections){
            if(connection.getChannel()!=null && connection.getChannel().getInvite().equals(channel.getInvite())){
                connection.updateMessage(message);
            }
        }
    }
    /** Finds a channel
     * 
     * @param channel the channel to be found.
     * @return An up to date version of the channel
     */
    public Channel findChannel(Channel channel){
        for(Channel someChannel: allChannels){
            if(channel.getInvite().equals(someChannel.getInvite())){
                return someChannel;
            }
        }
        return null;
    }
    /** Finds a user
     * 
     * @param user the user to be found.
     * @return An up to date version of the user
     */
    public User findUser(User user){
        for(User someUser: allUsers){
            if(someUser.getEmail().equals(user.getEmail()))
                return someUser;
        }
        return null;
    }
    
    
    /** Serializes the desired object
     * 
     * @param obj the desired object.
     * @param fileName the write location.
     */
    private void writeToSer(Object obj, String fileName){
        ObjectOutputStream oos;
        FileOutputStream fos;
        try{
            fos = new FileOutputStream(fileName);
            oos = new ObjectOutputStream(fos);
            oos.writeObject(obj);
            oos.close();
            fos.close();
           }
        catch(IOException io){
            io.printStackTrace();
        }
    }
    
    /** Reads a serialized object
     * 
     * @param fileName the file location.
     * @return The object
     */
    private Object readFromSer(String fileName){
        ObjectInputStream ois;
        FileInputStream fis;
        Object obj = null;
        try{
            fis = new FileInputStream(fileName);
            ois = new ObjectInputStream(fis);
            obj = ois.readObject();
            ois.close();
            fis.close();
           }
        catch(IOException io){
            io.printStackTrace();
        }
        catch (ClassNotFoundException c) 
        {
            System.out.println("Class not found");
            c.printStackTrace();
        }
        return obj;
    }
    
}
