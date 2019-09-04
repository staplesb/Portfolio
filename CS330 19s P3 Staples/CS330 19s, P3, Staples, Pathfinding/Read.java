/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;
import java.io.*;
import java.util.*;
/**
 *
 * @author stapl
 */
public class Read {
    private ArrayList<Node> nodes;
    private ArrayList<Connection> connections;
    private  ArrayList<Node[]> requests;
    
    public Read(){
        this.nodes = new ArrayList();
        this.connections = new ArrayList();
        this.requests = new ArrayList();
    }
    
    public void readFile(String fileName){
        try {
            BufferedReader br = new BufferedReader(new FileReader(fileName)); //reads in a file
            String line = br.readLine();
            String [] content;
            int counter =1;
            Node temp; //initialize necessary variables
            Node toNode;
            Node fromNode;
            while(line!=null){
                line = line.replaceAll("    ", "/"); // turns spaces into a unique character for ease of splitting the
                line = line.replaceAll("   ", "/");  // the string into an array
                line = line.replaceAll("  ", "/");
                line = line.replaceAll(" ", "/");
                content = line.split("/"); //split string into an array of strings
                if(content[0].equalsIgnoreCase("#")){ //if line starts with # ignore it and get next line
                    line = br.readLine();
                    continue;
                }
                else if(content[0].equalsIgnoreCase("N")){ //if line starts with N
                    if(content.length == 4)//if the line includes a name make a new node with a name
                        temp = new Node(counter, Double.parseDouble(content[1]), Double.parseDouble(content[2]), content[3]);
                    else //if it doesn't include a name make a new node with the name none
                        temp = new Node(counter, Double.parseDouble(content[1]), Double.parseDouble(content[2]));
                    counter++;//increment the identifier counter
                    nodes.add(temp); //add it to a list of nodes
                }
                else if(content[0].equalsIgnoreCase("C")){//if the line starts with C
                    fromNode = findNode(Integer.parseInt(content[1])); //find the corresponding from and to nodes
                    toNode = findNode(Integer.parseInt(content[2]));//then make a new connection and add it to the list of connecions
                    connections.add(new Connection(fromNode, toNode, Double.parseDouble(content[3])));
                }
                else if(content[0].equalsIgnoreCase("R")){//if the line starts with R
                    Node[] request = new Node[2]; //initialize an array to hold the requests
                    request[0] = findNode(Integer.parseInt(content[1])); //put the start node
                    request[1] = findNode(Integer.parseInt(content[2]));// and end node in the request array
                    requests.add(request); //add the request array to the list of requests
                }
                line = br.readLine();//get the next line
            }
            br.close(); //close the file
        }
        catch (IOException io){
            io.printStackTrace();
        }
    }
    
    public void print(){//print nodes, connections, and request for debugging
        for(Node node: nodes){
            System.out.print(node);
        }
        for(Connection connection: connections){
            System.out.print(connection);
        }
        for(Node[] request: requests){
            for(Node n: request)
                System.out.print(n);
            System.out.println("");
        }
    }
    
    public Node findNode(int number){ //find a node given it's identifier
        for(Node node: nodes){
            if(number == node.getNodeInt()){
                return node;
            }
        }
        return null;
    }
    
    public ArrayList<Node> getNodes(){ //get functions to retrieve the arraylist for further processing
        return nodes;
    }
    public ArrayList<Connection> getConnections(){
        return connections;
    }
    public ArrayList<Node[]> getRequests(){
        return requests;
    }
}
