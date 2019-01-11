/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sortmatrix;

/**
 *
 * @author stapl
 */
public class SortMatrix {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        Matrix mat = new Matrix();
        mat.createMatrix(100,100);
        mat.inputMatrix("matrix.txt");
        double [][] testMatrix = mat.getMatrix();
        /*for (double [] matrixI: testMatrix){
            for(double matrixJ: matrixI)
                System.out.print(matrixJ+" ");
            System.out.println();
        }*/
        mat.method1(testMatrix);
        mat.method2(testMatrix);
    }
    
    
    
    
    
}
