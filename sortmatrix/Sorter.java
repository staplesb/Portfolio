/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sortmatrix;

import java.util.Random;
/**
 *
 * @author stapl
 */
public class Sorter {
    private int comparisonCount;
    private int assignCount;
    
    public Sorter() {
        comparisonCount=0;
        assignCount=0;
    }
    
    
    private boolean EQ(double a, double b){
        comparisonCount++;
        if(a==b)
            return true;
        return false;
    }
    private boolean LT(double a, double b){
        comparisonCount++;
        if(a<b)
            return true;
        return false;
    }
    private boolean GT(double a, double b){
        comparisonCount++;
        if(a>b)
            return true;
        return false;
    }
    private double assign(double a){
        assignCount++;
        return a;
    }
    
    private int partition(double[] array, int left, int right){
        int index = left;
        double hold = 0.0;
        Random r = new Random();
        if(right-left>=3){
            int[] newR = new int[3];
            newR[0] = r.nextInt(right-left)+left;
            newR[1] = r.nextInt(right-left)+left;
            newR[2] = r.nextInt(right-left)+left;
            if(LT(array[newR[0]],array[newR[1]])){
                if(GT(array[newR[0]],array[newR[2]])) {
                    hold = assign(array[right]);
                    array[right] = assign(array[newR[0]]);
                    array[newR[0]] = assign(hold);
                }
                else if(GT(array[newR[2]],array[newR[1]])){
                    hold = assign(array[right]);
                    array[right] = assign(array[newR[1]]);
                    array[newR[1]] = assign(hold);
                }
            }
            else{
                hold = assign(array[right]);
                array[right] = assign(array[newR[2]]);
                array[newR[2]] = assign(hold);
            }
        }
        
        for(int i=left; i<right; i++){
            if(LT(array[i],array[right])){
                hold = assign(array[index]);
                array[index] = assign(array[i]);
                array[i] = assign(hold);
                index++;
            }
        }
        hold = assign(array[index]);
        array[index] = assign(array[right]);
        array[right] = assign(hold);
        return index;
    }
    
    private int partitionTwo(double[] array, int left, int right) {
        int i = left+1; int j = right;
        double hold = 0.0;
        while(!(GT(i,j))){
            if(!(GT(array[i],array[left])))
                i++;
            else
            if(!(LT(array[j],array[left])))
                j--;
            else {
                hold = assign(array[i]);
                array[i] = assign(array[j]);
                array[j] = assign(hold);
                i++; j--;
            }
        }
        hold = assign(array[left]);
        array[left] = assign(array[j]);
        array[j]=assign(hold);
        return j;
    }
    
    
    
    public void quickSort(double[] array, int left, int right){
        if(LT(left, right)){
            int pivot = partition(array, left, right);
            quickSort(array, left, pivot-1);
            quickSort(array, pivot+1, right);
        }
    }
    
    public void quickSortTwo(double[] array, int left, int right){
        if(LT(left, right)){
            int pivot = partitionTwo(array, left, right);
            quickSort(array, left, pivot-1);
            quickSort(array, pivot+1, right);
        }
    }
    
    public String toString(){
        return "Comparison Count = "+comparisonCount+"\nAssignment Count = "+
                assignCount+"\n";
    }
    
}
