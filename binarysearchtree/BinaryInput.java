/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package binarysearchtree;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

/**
 *
 * @author stapl
 */
public class BinaryInput {
    private int [] frequency;
    private String file;
    
    public BinaryInput(String file){
        this.file = file;
    }
    
    public void read(){
        BufferedReader reader = null;
        int input;
        int counter = 0;
        try {
            reader = new BufferedReader(new FileReader(file));
            counter = Integer.parseInt(reader.readLine());
            frequency = new int[counter+1];
            for(int i=1; i<counter+1; i++){
                frequency[i] = Integer.parseInt(reader.readLine());
            }
        }
        catch (IOException e){
            e.printStackTrace();
        }
    }
    
    public int[] getFrequency(){
        return frequency;
    }
    
    public String toString(){
        String r = "";
        for(int i: frequency){
            r+= i + " ";
        }
        return r;
    }
}
