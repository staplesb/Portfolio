/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;
import java.io.*;
/**
 *
 * @author stapl
 */
public class Write {
    
    
    public void writeFile(String fileName, String output){ //simple function to write the output to a file
        try{
            FileWriter fileWriter = new FileWriter(fileName);
            fileWriter.write(output);
            fileWriter.close();
        } catch (IOException io){
            io.printStackTrace();
        }
    }
}
