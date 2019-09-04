/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;

/**
 *
 * @author stapl
 */
public class Node {
    public int node;
    public double x;
    public double y;
    private String name;
    
    public Node(int node, double x, double y) { //data structure for an individual node
        this.node = node;
        this.x = x;
        this.y = y;
        this.name = "none";
    }
    public Node(int node, double x, double y, String name) {
        this.node = node;
        this.x = x;
        this.y = y;
        this.name = name;
    }
    
    public int getNodeInt(){
        return node;
    }
    
    public String toString(){
        return "Node " + node + " " + x + "x " + y + "y " + name + "\n";
    }
}
