using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private GameObject controller;
    private GameObject playerUnit;
    private GameObject enemyUnit;
    private List<GameObject> adjacentHexes;

    // Start is called before the first frame update
    private void Awake()
    {
    }
    private void Start()
    {
         
    }

    // Update is called once per frame
    private void Update()
    { 
        //If a hex is right clicked and an enemy occupies the hex, the player attacks the enemy
        if (Input.GetMouseButtonUp(1))
        {
            if (Vector2.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < gameObject.transform.localScale.x / 2.2)
            {
                if (enemyUnit != null)
                    controller.GetComponent<GameController>().attackEnemy(gameObject);
            }
        }
    }

    //Sets the adjacency list for this hex
    public void setAdjacency()
    {
        adjacentHexes = new List<GameObject>();
        GameObject parent = gameObject.transform.parent.gameObject;
        float dist;
        foreach(Transform child in parent.transform)
        {
            if (child.gameObject == null)
            {
                continue;
            }
            dist = Vector2.Distance(gameObject.transform.position, child.position);
            if (gameObject.Equals(child.gameObject))
                continue;
            if (dist <= 1)
            {
                adjacentHexes.Add(child.gameObject);
            }
        }
    }
    
    //Returns the adjacency list for this hex
    public List<GameObject> getAdjacent()
    {
        return adjacentHexes;
    }
    
    //Sets the player unit for this hex
    public void setPlayerUnit(GameObject playerUnit)
    {
        this.playerUnit = playerUnit;
    }
    
    //Returns the player unit for this hex
    public GameObject getPlayerUnit()
    {
        return playerUnit;
    }

    //Sets the enemy unit for this hex
    public void setEnemyUnit(GameObject enemyUnit)
    {
        this.enemyUnit = enemyUnit;
    }
    
    //Returns the enemy unit for this hex
    public GameObject getEnemyUnit()
    {
        return enemyUnit;
    }
    
    //Sets the sprite for this hex, for different terrain appearances
    public void setSprite(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Sets the game controller
    public void setController(GameObject controller)
    {
        this.controller = controller;
    }
    
    //Returns true if an enemy unit can be places on this hex
    public bool canPlaceUnit()
    {
        if (playerUnit != null)
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Mountain"))
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Water"))
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Portal"))
            return false;
        return true;
    }
    
    //Returns true if an enemy unit can move to this hex
    public bool canMoveTo()
    {
        if (enemyUnit != null)
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Mountain"))
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Water"))
            return false;
        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Portal"))
            return false;
        return true;
    }
    
    //If left mouse is clicked, call game controller to try and move the player
    private void OnMouseDown()
    {
        controller.GetComponent<GameController>().movePlayer(gameObject);
    }


    
}
