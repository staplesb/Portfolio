/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;
import java.util.ArrayList;
/**
 *
 * @author stapl
 */
public class Graph {
    private ArrayList<Connection> connections; //acts as the entire data structure as required.
    //To be more precise, a grapth is a list of connections which individually contain a from node, a to node, and a cost. 
    //Furthermore, each of node has an identifier, it's coordinates, and potentially a name. 
    
    public Graph(ArrayList<Connection> connections){ 
        this.connections = connections;
    }
    
    public ArrayList<Connection> getConnections(NodeRecord current){ //returns the connections of a give NodeRecord
        ArrayList<Connection> currentConnections = new ArrayList();
        for(Connection connection: connections){
            if(current.getNode() == connection.getFromNode().getNodeInt())
                currentConnections.add(connection);
        }
        return currentConnections;
    }
}
