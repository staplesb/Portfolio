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
public class NodeRecord { //data structure that contains a record from a path in A*
    public Node node;
    public Connection connection;
    public double costSoFar;
    public double estimatedTotalCost;
    
    public NodeRecord(){
        this.node = null;
        this.connection = null;
        this.costSoFar = -1;
        this.estimatedTotalCost = -1;
    }
    public NodeRecord(Node node){//no longer in use
        this.node = node;
        this.connection = null;
        this.costSoFar = -1;
        this.estimatedTotalCost = -1;
    }
    public NodeRecord(Node node, Connection connection, double costSoFar, double estimatedTotalCost){
        this.node = node;
        this.connection = connection;
        this.costSoFar = costSoFar;
        this.estimatedTotalCost = estimatedTotalCost;
    }
    public int getNode(){//used to compare node to determine if they are the same 
        return node.getNodeInt();//returns their identifier
    }
    
    public double getEstimatedTotalCost(){
        return estimatedTotalCost;
    }
    
    public String toString(){
        return node + " " + costSoFar + " "+ estimatedTotalCost + "\n";
    }
}
