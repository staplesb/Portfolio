package irc;

import java.util.ArrayList;
import java.util.Date;

public class Message implements java.io.Serializable {
	private String message;
	private User from;
	private Date timestamp;
        
        /** A constructor for a message
         * 
         * @param message the message text.
         * @param from the message sender.
         */
        public Message(String message, User from){
            this.message = message;
            this.from = from;
        }
	/** Outputs a message as string
         * 
         * @return The message
         */
	public String toString()
	{
            return from + ": " + message;
	}
	/** Sets the message
         * 
         * @param in the message text
         */
	public void setMessage(String in)
	{
		message=in;
	}
	/** Gets the message
         * 
         * @return the message
         */
	public String getMessage()
	{
		return message;
	}
	/** Sets the sender of the message
         * 
         * @param in the sender
         */
	public void setSender(User in)
	{
		from=in;
	}
	/** Gets the sender of the message
         * 
         * @return the sender
         */
	public User getSender()
	{
		return from;
	}
	/** Sets the time of the message. Not implemented.
         * 
         * @param in the time of creation.
         */
	public void setTime(Date in)
	{
		timestamp=in;
	}
	/** Gets the time of the message
         * 
         * @return the time of creation.
         */
	public Date getTime()
	{
		return timestamp;
	}
}
