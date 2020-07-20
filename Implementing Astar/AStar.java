/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;
import java.util.*;
/**
 *
 * @author stapl
 */
public class AStar {
    
    private ArrayList<Node> path;
    
    public AStar(){
        this.path = new ArrayList();
    }

    public boolean pathFindAStar(Graph graph, Node start, Node end){ //find path using A* algorithm
        path = new ArrayList();
        Heuristic heuristic = new Heuristic(end); //make a heuristic based on the end point 
        NodeRecord startRecord = new NodeRecord(start, null, 0, heuristic.estimate(start) ); //make a record for the start point
        NodeRecord current = new NodeRecord(); //initialize necessary variables
        Node endNode;
        double endNodeCost;
        double endNodeHeuristic;
        NodeRecord endNodeRecord;
        PathFindingList open = new PathFindingList();
        open.addNodeRecord(startRecord); //add start record to the list of open nodes
        PathFindingList closed = new PathFindingList();
        
        while(open.list.size() > 0){ //while there are still open nodes 
            current = open.list.peek(); //current is the lowest estimated cost node
            if (current.node == end) // if we'e reached the end point the exit
                    break;
            ArrayList<Connection> connections = graph.getConnections(current);//get the connections to the current node
            for(Connection connection: connections){ //for all the connections 
                endNode = connection.getToNode(); //get the connected node
                
                endNodeCost = current.costSoFar + connection.getCost(); //get the total cost for that node
                if(closed.find(endNode)!=null){ //if the end node is in the closed list
                    endNodeRecord = closed.find(endNode); //retrieve it from the closed list
                    if (endNodeRecord.costSoFar <= endNodeCost) //if the recorded cost is less than the new calculated cost
                        continue;
                    closed.list.remove(endNodeRecord);//else remove it from the close list
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;//and recalcualte the heuristic
                }
                else if (open.find(endNode)!=null){//if the node is in the opened list
                    endNodeRecord = open.find(endNode);//retrieve it
                    if (endNodeRecord.costSoFar <= endNodeCost)//if the recorded cost is less than the new calculated cost
                        continue;
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;//and recalculate the heuristic
                }
                else { //else create a node record for the endNode 
                    endNodeRecord = new NodeRecord(endNode, connection, endNodeCost, endNodeCost + heuristic.estimate(endNode));
                    if(open.find(endNode)==null){//if the endNode doesn't exist in the open list
                        open.addNodeRecord(endNodeRecord); // add the end node record to the open list
                    }
                        
                    
                }
                
            }
            open.list.remove(current); //remove the current node from the open list
            closed.list.add(current); //add it to the closed list.
            
        }
        if(current.node != end){ //if node isn't the end node
            return false; //failed
        }
        else {
            while(current.node != start){ //else save the path that was found
                path.add(current.connection.getToNode());
                current = closed.find(current.connection.getFromNode());
            }
            path.add(start); //make the path inclusive
            Collections.reverse(path); //and reverse it so that it is in order. 
        }
        return true;
    }
    
    public ArrayList<Node> getPath(){ //function to get the path
        return path;
    }
        
    private class PathFindingList {//data structure for getting the lowest estimated node
       public PriorityQueue<NodeRecord> list;
       
       public PathFindingList(){
           this.list = new PriorityQueue(new NodeRecordComparator());
       }
       
       public void addNodeRecord(NodeRecord nodeRecord){ //adds a nodeRecord to the queue this is unnecessary but was implemented
           list.add(nodeRecord);                        //because it was thought to be problematic. Later turned out to be benign
       }
       
       public NodeRecord find(Node node){ //finds a node record given the node
           Iterator it = list.iterator();
           NodeRecord temp;
           while(it.hasNext()){
               temp = (NodeRecord)it.next();
               if(temp.getNode() == node.getNodeInt()){
                   return temp;
               }
           }
           return null;
       }
       
       
    }
    
    public class NodeRecordComparator implements Comparator<NodeRecord>{ //used to sort priority queue making the lowest estimate first
        public int compare(NodeRecord n1, NodeRecord n2){
            if(n1.getEstimatedTotalCost()>n2.getEstimatedTotalCost())
                return 1;
            else if(n2.getEstimatedTotalCost()>n1.getEstimatedTotalCost())
                return -1;
            return 0;
            //return (int)(n1.getEstimatedTotalCost() - n2.getEstimatedTotalCost());
        }
    }
}
