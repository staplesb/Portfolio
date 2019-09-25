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
        if (Input.GetMouseButtonUp(1))
        {

            if (Vector2.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < gameObject.transform.localScale.x / 2.2)
            {
                if (enemyUnit != null)
                    controller.GetComponent<GameController>().attackEnemy(gameObject);
            }

        }

    }

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

    public List<GameObject> getAdjacent()
    {
        return adjacentHexes;
    }

    public void setPlayerUnit(GameObject playerUnit)
    {
        this.playerUnit = playerUnit;
    }

    public GameObject getPlayerUnit()
    {
        return playerUnit;
    }

    public void setEnemyUnit(GameObject enemyUnit)
    {
        this.enemyUnit = enemyUnit;
    }

    public GameObject getEnemyUnit()
    {
        return enemyUnit;
    }

    public void setSprite(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void setController(GameObject controller)
    {
        this.controller = controller;
    }

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

    private void OnMouseDown()
    {
        controller.GetComponent<GameController>().movePlayer(gameObject);
    }


    
}
