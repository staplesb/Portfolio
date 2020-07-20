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
public class Pathfinding {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        Read read = new Read();
        read.readFile("CS 330, Graph Adventure Bay v2.txt"); //read in file
        Graph g = new Graph(read.getConnections()); //make graph from read in data
        AStar ast = new AStar();
        ArrayList<ArrayList<Node>> arrays = new ArrayList(); //make an arraylist to hold the returned paths
        for(Node[] node: read.getRequests()){ //iterate through requests
            ast.pathFindAStar(g, node[0], node[1]); //find path for request
            arrays.add(ast.getPath()); //add path to array
        }
        String output = "Nodes\n"; //make an output string to have nodes, connections, and paths
        for(Node node: read.getNodes()){ 
            output+= node;
        }
        output+= "\nConnections\n";
        for(Connection connection: read.getConnections()){
            output+= connection;
        }
        for(ArrayList<Node> path: arrays){
            output+= "\nPath: "+ path.get(0).toString().replace("\n", "")+ " to " + path.get(path.size()-1) + "\n";
            for(Node node: path)
                output+= node;
        }
        Write write = new Write();
        write.writeFile("CS 330 19S, P3, Staples, Results.txt", output); //write the output to the file
        System.out.print(output); //print to console for debugging
        //read.print();
    }
    
}
