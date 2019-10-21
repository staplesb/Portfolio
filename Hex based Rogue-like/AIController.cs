using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public List<GameObject> enemyTypes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //If a enemies dies, it is set to inactive. This cleans up inactive enemies.
        foreach(Transform child in gameObject.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //Move all enemies for their turn
    public void enemyTurn()
    {
        foreach(Transform child in gameObject.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.GetComponent<enemyCharacter>().getCurrentHex().GetComponent<Hex>().setEnemyUnit(null);
                child.GetComponent<enemyMovement>().move();
            } 
        }    
    }
    
    //Create a number of enemies with given stats on any number of potential hexes
    public void instantiateEnemies(int numberOfEnemies, double enemyStatMultiplier, List<GameObject> availableHexes)
    {
        GameObject availableHex;
        
        for(int i=0; i<numberOfEnemies; i++)
        {
            availableHex = availableHexes[Random.Range(0, availableHexes.Count)];
            availableHexes.Remove(availableHex);
            instantiateEnemy(enemyStatMultiplier, availableHex);
            
        }
    }
    
    //Create a single enemy with given stats on a single hex
    public void instantiateEnemy(double enemyStatMultiplier, GameObject hex)
    {
        GameObject newEnemy;
        newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], hex.transform.position, Quaternion.identity);
        hex.GetComponent<Hex>().setEnemyUnit(newEnemy);
        newEnemy.GetComponent<enemyCharacter>().setCurrentHex(hex);
        newEnemy.GetComponent<enemyCharacter>().setHealth(Mathf.RoundToInt(50 * (float)enemyStatMultiplier));
        newEnemy.GetComponent<enemyCharacter>().setAttack(Mathf.RoundToInt(10 * (float)enemyStatMultiplier));
        newEnemy.transform.parent = gameObject.transform;
    }



}
