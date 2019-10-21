using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerCharacter : MonoBehaviour
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
            setHealth(100);
            setAttack(25);
        }
        
        //Set range and health based on the selected class
        if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Mage"))
            range = 1;
        else if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Archer"))
            range = 2;
        else
        {
            setHealth(200);
            range = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            StartCoroutine(death());
        
    }
    
    //Return the currently occupied hex
    public GameObject getCurrentHex()
    {
        return currentHex;
    }

    //Return the player's range
    public float getRange()
    {
        return range;
    }
    
    //Return the player's health
    public int getHealth()
    {
        return health;
    }
    
    //Return the player's attack
    public int getAttack()
    {
        return attack;
    }
    
    //Set the player's current hex
    public void setCurrentHex(GameObject currentHex)
    {
        this.currentHex = currentHex;
    }
    
    //Set the player's health
    public void setHealth(int health)
    {
        this.health = health;

        foreach (Transform child in GameObject.Find("Canvas").transform)
        {
            if (child.gameObject.name == "Inventory")
            {
                foreach (Transform invChild in child)
                {
                    if (invChild.gameObject.name.Contains("HP"))
                        invChild.gameObject.GetComponent<Text>().text = health.ToString();
                }
            }
        }
    }
    
    //Set the player's attack
    public void setAttack(int attack)
    {
        this.attack = attack;
        foreach (Transform child in GameObject.Find("Canvas").transform)
        {
            if (child.gameObject.name == "Inventory")
            {
                foreach (Transform invChild in child)
                {
                    if (invChild.gameObject.name.Contains("ATK"))
                        invChild.gameObject.GetComponent<Text>().text = attack.ToString();
                }
            }
        }
    }
    
    //Coroutine to start the player's death animation and return to title screen
    private IEnumerator death()
    {
        gameObject.GetComponent<Animator>().SetBool("Death", true);
        yield return new WaitForSeconds(2.58f);
        currentHex.GetComponent<Hex>().setEnemyUnit(null);
        SceneManager.LoadScene("TitleScene");
        yield return null;
    }

}
