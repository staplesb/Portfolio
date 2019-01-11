/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sortmatrix;

import java.io.*;
import java.util.Random;

/**
 *
 * @author stapl
 */
public class Matrix {
    
    public void createMatrix(int rows, int columns){
        StringBuilder randomDouble = new StringBuilder();
        Random r = new Random();
        try {
            FileWriter writer = new FileWriter("matrix.txt");
            for (int i=0; i<rows; i++)
            {
                for (int j=0; j<columns; j++)
                {
                    randomDouble.delete(0, randomDouble.length());
                    randomDouble.append(r.nextInt(1000));
                    for (int k=randomDouble.length(); k<3; k++)
                        randomDouble.insert(0, 0);
                    randomDouble.append('.');
                    int lower = r.nextInt(100);
                    if (lower<10)
                        randomDouble.append(0);
                    randomDouble.append(lower);
                    randomDouble.append(" ");
                    writer.write(randomDouble.toString());
                }
                writer.write("\n");
            }
            writer.close();
        }
        catch (IOException e){
           e.printStackTrace();
       }
    }
    
    public void inputMatrix(String fileName)
    {
        BufferedReader reader = null;
        String input = null;
        double [] row = null;
        double [][] mat = null;
        int itr = 0;
        int numRows = 0;
        try {
            reader = new BufferedReader(new FileReader(fileName));
            while(reader.readLine()!=null) numRows++;
            reader.close();
            mat = new double [numRows][];
            reader = new BufferedReader(new FileReader(fileName));
            do {
            input = reader.readLine();
            String [] sRow = input.split(" ");
            row = new double [sRow.length];
            for (int i=0; i<sRow.length; i++)
                row[i] = Double.parseDouble(sRow[i]);
            mat[itr] = row;
            itr++;
            }
            while(itr<numRows);
            reader.close();
        }
        catch(IOException e){
            e.printStackTrace();
        }
        matrix = mat;
    }
    
    public double[][] getMatrix()
    {
        return matrix;
    }
    
    public void method1(double[][] matrix){
        double[] array;
        Sorter quick = new Sorter();
        int length=0;
        for(double[] mat: matrix){
            for(double value: mat)
                length++;
        }
        array = new double[length];
        int itr=0;
        for(double[] mat: matrix){
            for(double value: mat){
                array[itr]=value;
                itr++;
            }   
        }
        
        quick.quickSort(array,0,array.length-1);
        itr=0;
        //for(double val: array)
            //System.out.print(val+" ");
        for(double[] mat: matrix){
            for(int i=0; i<mat.length; i++){
                mat[i]=array[itr];
                itr++;
            }   
        }
        output(matrix, quick, "bds0025_1.txt");
        System.out.println("Method 1:");
        System.out.println(quick);
        System.out.println("----------------------------");
    }
    
    public void method2(double[][] matrix){
        Sorter quickSort = new Sorter();
        int maxLength = 0;
        for(double[] mat: matrix){
            quickSort.quickSort(mat, 0, mat.length-1);
            //System.out.println(quickSort);
            if(mat.length>maxLength)
                maxLength = mat.length;
        }
        /*for(double[] mat: matrix){
            for(double value: mat)
               System.out.print(value+" ");
            System.out.println();
        }*/
        
        double[][] newMatrix = new double[maxLength][matrix.length];
        for(int i=0; i<matrix.length; i++){
            for(int j=0; j<matrix[i].length; j++){
                newMatrix[j][i]=matrix[i][j];
            }
        }
        for(double[] mat: newMatrix){
            quickSort.quickSort(mat, 0, mat.length-1);
        }
        double[][] outputMatrix = new double[newMatrix.length][maxLength];
        for(int i=0; i<newMatrix.length; i++){
            for(int j=0; j<newMatrix[i].length; j++){
                outputMatrix[j][i]=newMatrix[i][j];
            }
        }
        output(outputMatrix, quickSort, "bds0025_2.txt");
        System.out.println("Method 2:");
        System.out.println(quickSort);
    }
    
    private void output(double[][] outputMatrix, Sorter sort, String filename){
        try{
            FileWriter writer = new FileWriter(filename);
        
            for(double[] mat: outputMatrix){
                for(double value: mat){
                    String val = "";
                   if (value<10)
                       val = "00"+Double.toString(value);
                   else if (value<100)
                       val = "0"+Double.toString(value);
                   else
                       val = Double.toString(value);
                   if(val.length()<6)
                       val+="0";
                   writer.write(val+" ");
                   //System.out.print(val+" ");
                }
                writer.write("\n");
                //System.out.println();
            }
            writer.write(sort.toString());
            writer.close();
        }
        catch (IOException e){
           e.printStackTrace();
       }
    }
    
    
    private double[][] matrix;
    
}
