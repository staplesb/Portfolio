/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package irc;

/**
 *
 * @author coach
 */
public class Role implements java.io.Serializable
{
    private User user;
    private int role;
    
    /** A null constructor for Role
     * 
     */
    public Role()
    {
        this.user = null;
    }
    /** A constructor for Role
     * 
     * @param user the user.
     * @param role the role of the user.
     */
    public Role(User user, int role){
        this.user = user;
        this.role = role;
    }
    /** Gets the user
     * 
     * @return the user.
     */
    public User getUser()
    {
        return user;
    }
    /** Sets the user
     * 
     * @param newUser the user.
     */
    public void setUser(User newUser)
    {
        user = newUser;
    }
    /** Sets the role of a user
     * 
     * @param num the role.
     */
    public void setRole(int num)
    {
       role = num;
    }
    /** Gets the role of a user
     * 
     * @return the role.
     */
    public int getRole()
    {
        return role;
    }
    
}
