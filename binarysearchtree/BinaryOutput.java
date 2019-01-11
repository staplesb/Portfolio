/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package binarysearchtree;
import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
/**
 *
 * @author stapl
 */
public class BinaryOutput {
    private int [] p;
    private int [][] values;
    private int [][] roots;
    
    public BinaryOutput(int [] p){
        this.p = p;
    }
    
    public void createTable(){
        int n = p.length;
        values = new int[n+1][n];
        roots = new int[n+1][n];
        for(int i = 1; i  < n+1 ; i++){
            values[i][i-1] = 0;
        }
        
        for(int i = 1; i  < n; i++){
            values[i][i] = p[i];
            roots[i][i] = i;
        }    
        
        for(int d = 1; d  < (n-1); d++){
            for(int i = 1; i  < (n-d); i++){
                int j = i + d;
                int sum_p = 0;     
                int min_value = 100000;
                int min_root = 0;
                for(int k = i; k  <= j; k++){
                    sum_p += p[k];
                    int value = values[i][k-1] + values[k+1][j];
                    if(value < min_value){
                        min_value = value;
                        min_root = k;
                    }
                }
                values[i][j] = sum_p + min_value;
                roots[i][j] = min_root;
            }
        }
    }
    
    public void write(String file){
        BufferedWriter writer = null;
        try {
            writer = new BufferedWriter(new FileWriter(file));
            writer.write(printTable());
            writer.write("\nTree Output: A - represents a empty child node\n");
            writer.write(printTree(1,p.length-1,0));
            writer.close();
        }
        catch (IOException e){
            e.printStackTrace();
        }
        System.out.println(printTable());
        System.out.println("\nTree Output: A - represents an empty child node\n");
        System.out.println(printTree(1,p.length-1,0));
        
    }
    
    private String printTable(){
        String r = "";
        r+="Roots table:\n-\t";
        for(int i=0; i< roots[0].length; i++){
            r+=i+"\t";
        }
        r+="\n";
        for(int j=1; j<roots.length; j++){
            r+=j+"\t";
            for(int i=0; i< roots[j].length; i++){
                r+=roots[j][i]+"\t";
                }        
                r+="\n";
            }
        r+="\nValues table:\n-\t\t";
        for(int i=0; i< roots[0].length; i++){
                r+=i+"\t\t";
        }
        r+="\n";
        for(int j=1; j<values.length; j++){
            r+=j+"\t\t";
            for(int i=0; i< values[j].length; i++){
                r+=values[j][i] + "\t";
                if(values[j][i]<Math.pow(10, 3))
                    r+="\t";
                }        
                r+="\n";
            }
        return r;
    }
    
    private String printTree(int i, int  j, int space_needed){
        String someString = "";
        String space = "";
        
        if(space_needed>0)
            space = new String(new char[space_needed]).replace("\0", "\t"); 
        if(i <= j){
            someString += space + roots[i][j] + "\n";
            someString += printTree(i, roots[i][j] - 1, space_needed+1);
            if(printTree(i, roots[i][j] - 1, space_needed+1)=="")
                someString += space + "\t-\n";
            someString += printTree(roots[i][j] + 1, j, space_needed+1);
            if(printTree(roots[i][j] + 1, j, space_needed+1)=="")
                someString += space + "\t-\n";
            
        }
        return someString;
    }

}
