/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pathfinding;

/**
 *
 * @author stapl
 */
public class Heuristic {
    private Node goal;
    
    public Heuristic(Node goal) {
        this.goal = goal;
    }
    public double estimate(Node start){ //euclidean estimate from start to goal
        double distX;
        double distY;
        distX = goal.x - start.x;
        distY = goal.y - start.y;
        return Math.sqrt((distX*distX)+(distY*distY));
    }
}
