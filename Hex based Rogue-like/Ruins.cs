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
        if (!occupied() && !instantiating && numberSpawned < 3 && Vector3.Distance(player.transform.position, hex.transform.position) <=3)
        {
            numberSpawned++;
            instantiating = true;
            StartCoroutine(delayedInstantiate());
        }
        if(player.transform.position == gameObject.transform.position)
        {
            int randomItem = Random.Range(0, 4);
            //items from resources
            GameObject item = Resources.Load("Items/Item" + randomItem) as GameObject;
            Instantiate(item, hex.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private IEnumerator delayedInstantiate()
    {
        yield return new WaitForSeconds(0.5f);
        AIManager.GetComponent<AIController>().instantiateEnemy(enemyStatMultiplier, hex);
        instantiating = false;
        yield return null;
    }

    private bool occupied()
    {
        if (hex.GetComponent<Hex>().getEnemyUnit() || hex.GetComponent<Hex>().getPlayerUnit())
            return true;
        return false;
    }


    public void dropItem()
    {
        int randomItem = Random.Range(0, 4); 
        GameObject item = Resources.Load("Items/Item" + randomItem) as GameObject;
        Instantiate(item, gameObject.transform.position, Quaternion.identity);
    }

    public void setHex(GameObject hex)
    {
        this.hex = hex;
    }
    public GameObject getHex()
    {
        return hex;
    }

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

    public void setEnemyStatMultiplier(double enemyStatMultiplier)
    {
        this.enemyStatMultiplier = enemyStatMultiplier;
    }

    public void setAIManager(GameObject AIManager)
    {
        this.AIManager = AIManager;
    }

}
