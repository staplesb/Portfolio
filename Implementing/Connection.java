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
public class Connection {
    private Node fromNode;
    private Node toNode;
    private double cost;
    
    
    public Connection(Node fromNode, Node toNode,  double cost){ //data structure for connection
        this.fromNode = fromNode;
        this.toNode = toNode;
        this.cost = cost;
    }
    public double getCost(){
        return cost;
    }
    public Node getToNode(){
        return toNode;
    }
    public Node getFromNode(){
        return fromNode;
    }
    
    public String toString() {
        return "Connection:\n" + "From Node: " + fromNode + "To Node: " + toNode + "cost: "+ cost+ "\n";
    }
}
