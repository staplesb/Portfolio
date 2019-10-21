using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruins : MonoBehaviour
{
    private GameObject hex;
    private GameObject player;
    private double enemyStatMultiplier;
    private GameObject AIManager;
    private int numberSpawned;
    private bool instantiating = false;
    // Start is called before the first frame update
    private void Start()
    {
        
        numberSpawned = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //If the ruins isn't occupied, hasn't spawned the max number of enemies, and the player is close to the ruin, spawn an enemy. 
        if (!occupied() && !instantiating && numberSpawned < 3 && Vector3.Distance(player.transform.position, hex.transform.position) <=3)
        {
            numberSpawned++;
            instantiating = true;
            StartCoroutine(delayedInstantiate());
        }
        //If the player is on the ruin, drop the player an item, and destroy the ruin. 
        if(player.transform.position == gameObject.transform.position)
        {
            int randomItem = Random.Range(0, 4);
            //items from resources
            GameObject item = Resources.Load("Items/Item" + randomItem) as GameObject;
            Instantiate(item, hex.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
    
    //Coroutine to create an enemy at the ruin
    private IEnumerator delayedInstantiate()
    {
        yield return new WaitForSeconds(0.5f);
        AIManager.GetComponent<AIController>().instantiateEnemy(enemyStatMultiplier, hex);
        instantiating = false;
        yield return null;
    }
    
    //Return true if an enemy unit is on the ruin
    private bool occupied()
    {
        if (hex.GetComponent<Hex>().getEnemyUnit() || hex.GetComponent<Hex>().getPlayerUnit())
            return true;
        return false;
    }

    //Create a random item
    public void dropItem()
    {
        int randomItem = Random.Range(0, 4); 
        GameObject item = Resources.Load("Items/Item" + randomItem) as GameObject;
        Instantiate(item, gameObject.transform.position, Quaternion.identity);
    }
    
    //Set the hex for this ruin
    public void setHex(GameObject hex)
    {
        this.hex = hex;
    }
    //Return the hex for this ruin
    public GameObject getHex()
    {
        return hex;
    }
    
    //Set the player
    public void setPlayer(GameObject player)
    {
        this.player = player;
    }
    
    //Set the enemy stat multiplier
    public void setEnemyStatMultiplier(double enemyStatMultiplier)
    {
        this.enemyStatMultiplier = enemyStatMultiplier;
    }
    
    //Set the AI controller
    public void setAIManager(GameObject AIManager)
    {
        this.AIManager = AIManager;
    }

}
