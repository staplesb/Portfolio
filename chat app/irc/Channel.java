package irc;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;
import java.io.*;
import java.util.Random;

public class Channel implements Serializable{
	private String name;
	private String invite;
	private ArrayList<Role> members;
	private ArrayList<Message> messageHistory;
        
        /** A Constructor for Channel 
         * 
        @param name A string with the name of the channel.
        @param creator The user who created the channel.
        */ 	
	public Channel(String name, User creator)
	{
		this.name=name;
		invite=createInvite();
                members = new ArrayList();
                messageHistory = new ArrayList();
		members.add(new Role(creator,3));
                messageHistory.add(new Message("Welcome to your new Channel!\n\n", new User(name, "", "")));
	}
        
        /** Creates a invite for the channel 
         * 
        @return A 12 ascii character invite. 
        */ 
        private String createInvite() {
            String invite = "";
            Random r = new Random();
            for(int i=0; i<12; i++){
                invite+= (char) (r.nextInt(93)+33);
            }
            return invite;
        }
	/** Adds a message to a channel
         * 
         * @param message the message to be added.
         */
	public void addMessage(Message message)
	{
		messageHistory.add(message);
	}
	/** Removes a message from a channel
         * 
         * @param message the message to be removed.
         */
	public void removeMessage(Message message)
	{
		messageHistory.remove(message);
	}
	/** Adds a user to the channel
         * 
         * @param user the user to be added. 
         * @return The user was added
         */
	public boolean addUser(User user)
	{
            Role role = new Role(user, 0);
            if(members.contains(role))
                return false;
            return members.add(new Role(user, 0));

	}
	/** Finds a user's role
         * 
         * @param user the specified user.
         * @return The Role of the desired user
         */
	public Role findUser(User user)
	{
		Iterator<Role> it=members.iterator();
                if(members.isEmpty()){
                    return null;
                }
		Role temp=members.get(0);
		if(temp.getUser()==user) return temp;
		while(it.hasNext())
		{
			temp=it.next();
			if(temp.getUser().getEmail().equals(user.getEmail())) return temp;
		}
		return null;
	}
	/** Removes a user from the channel
         * 
         * @param user the user to be removed. 
         */
	public void removeUser(User user)
	{
		Role temp=findUser(user);
		members.remove(temp);
	}
        /** Allows the creator to remove a user from the channel. 
         * 
         * @param user the user to be removed.
         * @param kicker the admin removing the user. If the same as user the user leaves the channel.
         * @return The user is kicked
         */
	public boolean kick(User user,User kicker)
	{
            if(user.getEmail().equals(kicker.getEmail())){
                removeUser(user);
                user.removeChannel(this);
                return true;
            } else if(findUser(kicker).getRole()>findUser(user).getRole()){
                removeUser(user);
               	user.removeChannel(this);
                return true;
            }
            return false;
	}
	/** Sets the name of the channel
         * 
         * @param newname the new name for the channel. 
         */
	public void setName(String newname)
	{
		name=newname;
	}
	/** Gets the name of the channel
         * 
         * @return the name for the channel. 
         */
	public String getName()
	{
		return name;
	}
	
	/** Gets the invite of the channel
         * 
         * @return the invite for the channel;
         */
	public String getInvite()
	{
		return invite;
	}
	/** Gets the messages for the channel
         * 
         * @return all of the messages for the channel. 
         */
	public ArrayList<Message> getMessages()
	{
		return messageHistory;
	}
	/** Gets the members of the channel
         * 
         * @return all of the members for the channel.
         */
	public ArrayList<Role> getMembers()
	{
		return members;
	}
        /** Turns the channel into a string
         * 
         * @return the name of the channel.
         */
        public String toString(){
            return name;
        }
}
