using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCharacter : MonoBehaviour
{
    public GameObject currentHex;
    public int health = 0;
    public int attack = 0;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        if (health == 0)
        {
            setHealth(50);
            setAttack(10);
        }
        if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Archer"))
            range = 2;
        else
        {
            range = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            StartCoroutine(death());

    }


    public GameObject getCurrentHex()
    {
        return currentHex;
    }

    public float getRange()
    {
        return range;
    }

    public int getHealth()
    {
        return health;
    }

    public int getAttack()
    {
        return attack;
    }

    public void setCurrentHex(GameObject currentHex)
    {
        this.currentHex = currentHex;
    }

    public void setHealth(int health)
    {
        this.health = health;
        
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.name == "EnemyCanvas")
            {
                foreach (Transform statChild in child)
                {
                    if (statChild.gameObject.name == "HP")
                        statChild.gameObject.GetComponent<Text>().text = health.ToString();
                }
            }
        }
    }

    public void setAttack(int attack)
    {
        this.attack = attack;
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.name == "EnemyCanvas")
            {
                foreach (Transform statChild in child)
                {
                    if (statChild.gameObject.name == "ATK")
                        statChild.gameObject.GetComponent<Text>().text = attack.ToString();
                }
            }
        }

    }

    private IEnumerator death()
    {
        gameObject.GetComponent<Animator>().SetBool("Death", true);
        yield return new WaitForSeconds(0.58f);
        currentHex.GetComponent<Hex>().setEnemyUnit(null);
        gameObject.SetActive(false);
        if (Random.value < 0.55f)
        {
            int randomItem = Random.Range(0, 4);
            //items from resources
            GameObject item = Resources.Load("Items/Item" + randomItem) as GameObject;
            Instantiate(item, currentHex.transform.position, Quaternion.identity);
        }
        yield return null;
    }
}
