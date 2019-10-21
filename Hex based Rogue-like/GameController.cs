using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject map;
    public GameObject player;
    public GameObject ruin;

    private GameObject currentRuin;
    private GameObject currentMap;
    private GameObject currentPlayer;
    private GameObject AIManager;

    private int level;
    private double enemyStatMultiplier = 1;
    private bool enemyTurn = false;




    private void Awake()
    {
        AIManager = GameObject.Find("AIManager");
        instantiateMap();
    }

    void Start()
    {
        level = 1;

        GameObject.Find("BoatImage").SetActive(false);
        GameObject.Find("WingImage").SetActive(false);

        //Ensure a valid map with a path from start portal to end portal
        while (!currentMap.GetComponent<PlaceHexes>().searchForPath())
        {
            Destroy(currentMap);
            instantiateMap();
        }

        //Get the player's selected class
        string playerClass = GameObject.Find("CharEmpty").GetComponent<CharEmpty_Script>().Char;
        
        if(playerClass == "Swordsman")
            instantiatePlayer("Swordsman");
        else if (playerClass == "Archer")
            instantiatePlayer("Archer");
        else
            instantiatePlayer("Mage");
        
        instantiateEnemies(5);
        instantiateRuins();
    }

    private void Update()
    {
        //When player reached the exit portal, destroy current level and create the next level
        if(currentPlayer.GetComponent<playerCharacter>().getCurrentHex() == currentMap.GetComponent<PlaceHexes>().getExitPortal())
        {
            Destroy(currentRuin);
            foreach (Transform child in AIManager.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(currentMap);

            instantiateMap();
            while (!currentMap.GetComponent<PlaceHexes>().searchForPath())
            {
                Destroy(currentMap);
                instantiateMap();
            }

            level++;
            if (level % 5 == 0) //Enemies stats increase every 5th level
                enemyStatMultiplier *= 1.5;
            instantiateEnemies(5 + (level - 1) % 5);
            instantiateRuins();
            currentPlayer.GetComponent<Rigidbody2D>().position = currentMap.GetComponent<PlaceHexes>().getEnterPortal().transform.position;
            currentPlayer.GetComponent<playerCharacter>().setCurrentHex(currentMap.GetComponent<PlaceHexes>().getEnterPortal());
            currentPlayer.GetComponent<playerAttack>().setAIManager(AIManager);
            currentPlayer.GetComponent<playerMovement>().newTurn();
            currentMap.GetComponent<PlaceHexes>().getEnterPortal().GetComponent<Hex>().setPlayerUnit(currentPlayer);
        } 
        //Check to see if the player's turn has ended
        else if(!enemyTurn && !currentPlayer.GetComponent<playerMovement>().canMove() && !currentPlayer.GetComponent<playerMovement>().getMoving() && !currentPlayer.GetComponent<playerAttack>().getCanAttackEnemy())
        {
            enemyTurn = true;
            StartCoroutine(startEnemyTurn());
        } 
        //Check to see if Enemy's turn has ended
        else if (currentPlayer.GetComponent<playerMovement>().canMove())
        {
            enemyTurn = false;
        }
            
    }
    //Coroutine to ensure that no projectiles exist before transitioning to the enemy's turn
    private IEnumerator startEnemyTurn()
    {
        yield return new WaitForSeconds(0.2f);
        while (GameObject.FindGameObjectWithTag("Projectile") != null || enemyDeath())
        {
            yield return null;
        }
        AIManager.GetComponent<AIController>().enemyTurn();
        yield return null;
    }
    
    //If an enemy died return true
    private bool enemyDeath()
    {
        foreach(Transform child in AIManager.transform)
        {
            if (child.gameObject.GetComponent<Animator>().GetBool("Death"))
                return true;
        }
        return false;
    }
    
    //Create a new map
    private void instantiateMap()
    {
        currentMap = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
        currentMap.GetComponent<PlaceHexes>().setHexController(gameObject);
    }

    //Create a new player
    public void instantiatePlayer(string playerClass)
    {
        if (playerClass == "Swordsman")
        {
            player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/SwordsmanAnimator") as RuntimeAnimatorController;
        } else if (playerClass == "Archer")
        {
            player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/ArcherAnimator") as RuntimeAnimatorController;
        } else if (playerClass == "Mage")
        {
            player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/MageAnimator") as RuntimeAnimatorController;
        }
        currentPlayer = Instantiate(player, currentMap.GetComponent<PlaceHexes>().getEnterPortal().transform.position, Quaternion.identity);
        currentPlayer.GetComponent<playerCharacter>().setCurrentHex(currentMap.GetComponent<PlaceHexes>().getEnterPortal());
        currentPlayer.GetComponent<playerAttack>().setAIManager(AIManager);
        currentMap.GetComponent<PlaceHexes>().getEnterPortal().GetComponent<Hex>().setPlayerUnit(currentPlayer);
    }

    //Create a new ruin
    public void instantiateRuins()
    {
        List<GameObject> potentialHexes = new List<GameObject>();

        foreach (Transform child in currentMap.transform)
        {
            if (child.gameObject.GetComponent<Hex>().canPlaceUnit())
                potentialHexes.Add(child.gameObject);
        }

        GameObject ruinHex = potentialHexes[Random.Range(0, potentialHexes.Count)];
        currentRuin = Instantiate(ruin, ruinHex.transform.position, Quaternion.identity);
        currentRuin.GetComponent<Ruins>().setHex(ruinHex);
        currentRuin.GetComponent<Ruins>().setPlayer(currentPlayer);
        currentRuin.GetComponent<Ruins>().setEnemyStatMultiplier(enemyStatMultiplier);
        currentRuin.GetComponent<Ruins>().setAIManager(AIManager);
        //Because ruins instantiate enemies when the player is in a certain range, we need to set several variables
    }
    
    //Create new enemies
    private void instantiateEnemies(int numberOfEnemies)
    {
        List<GameObject> availableHexes = new List<GameObject>();

        foreach (Transform child in currentMap.transform)
        {
            if (child.GetComponent<Hex>().canPlaceUnit())
            {
                availableHexes.Add(child.gameObject);
            }
        }

        AIManager.GetComponent<AIController>().instantiateEnemies(numberOfEnemies, enemyStatMultiplier, availableHexes);
     }

    //Allows other scripts to move the player
    public void movePlayer(GameObject destination)
    {
        if (currentPlayer.GetComponent<playerMovement>().canMove())
        {
            currentPlayer.GetComponent<playerMovement>().move(destination, 0.5f);
        }
    }

    //Allows other scripts to make the player attack
    public void attackEnemy(GameObject target)
    {
        if (currentPlayer.GetComponent<playerAttack>().getCanAttack())
        {
            currentPlayer.GetComponent<playerAttack>().attack(target);
        }
    }

}
