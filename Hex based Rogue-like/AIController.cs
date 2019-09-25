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
        foreach(Transform child in gameObject.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }

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
