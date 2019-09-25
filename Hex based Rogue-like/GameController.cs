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

        while (!currentMap.GetComponent<PlaceHexes>().searchForPath())
        {
            Destroy(currentMap);
            instantiateMap();
        }

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
            if (level % 5 == 0)
                enemyStatMultiplier *= 1.5;
            instantiateEnemies(5 + (level - 1) % 5);
            instantiateRuins();
            currentPlayer.GetComponent<Rigidbody2D>().position = currentMap.GetComponent<PlaceHexes>().getEnterPortal().transform.position;
            currentPlayer.GetComponent<playerCharacter>().setCurrentHex(currentMap.GetComponent<PlaceHexes>().getEnterPortal());
            currentPlayer.GetComponent<playerAttack>().setAIManager(AIManager);
            currentPlayer.GetComponent<playerMovement>().newTurn();
            currentMap.GetComponent<PlaceHexes>().getEnterPortal().GetComponent<Hex>().setPlayerUnit(currentPlayer);
        } else if(!enemyTurn && !currentPlayer.GetComponent<playerMovement>().canMove() && !currentPlayer.GetComponent<playerMovement>().getMoving() && !currentPlayer.GetComponent<playerAttack>().getCanAttackEnemy())
        {
            enemyTurn = true;
            StartCoroutine(startEnemyTurn());
        } else if (currentPlayer.GetComponent<playerMovement>().canMove())
        {
            enemyTurn = false;
        }
            
    }

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

    private bool enemyDeath()
    {
        foreach(Transform child in AIManager.transform)
        {
            if (child.gameObject.GetComponent<Animator>().GetBool("Death"))
                return true;
        }
        return false;
    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (currentPlayer == null)
    //        return;


    //    if (currentPlayer.GetComponent<Character>().getCurrentHex() != null)
    //    {
    //        if (currentPlayer.GetComponent<Character>().getCurrentHex().Equals(currentMap.GetComponent<PlaceHexes>().getExitPortal()))
    //        {
    //            level++;

    //            foreach (Transform child in GameObject.Find("Canvas").transform)
    //            {
    //                if (child.gameObject.name == "LevelDisplay")
    //                {
    //                    foreach (Transform invChild in child)
    //                    {
    //                        if (invChild.gameObject.name.Contains("LevelText"))
    //                            invChild.gameObject.GetComponent<Text>().text = level.ToString();
    //                    }
    //                }
    //            }

    //            if (level % 5 == 0)
    //            {
    //                enemyStatMultiplier *= 1.5f;
    //            }
    //            Destroy(currentMap);
    //            Destroy(currentRuin);

    //            instantiateMap();
    //            currentPlayer.GetComponent<Rigidbody2D>().position = currentMap.GetComponent<PlaceHexes>().getEnterPortal().transform.position;
    //            currentPlayer.GetComponent<Character>().setCurrentHex(currentMap.GetComponent<PlaceHexes>().getEnterPortal());
    //            currentMap.GetComponent<PlaceHexes>().getEnterPortal().GetComponent<Hex>().setPlayerUnit(currentPlayer);
    //            instantiateEnemies();
    //            instantiateRuins();
    //            numberSpawned = 0;
    //            deathPhase = false;
    //            enemyPhase = false;
    //            currentPlayer.GetComponent<Character>().unlockPlayer();
    //            currentPlayer.GetComponent<Attack>().setCanAttack();
    //            preventEnemyMove = true;
    //            StartCoroutine(preventEnemyMov());
    //        }
    //    }

    //    if (!deathPhase && !enemyPhase)
    //    {
    //        try
    //        {
    //            foreach (GameObject enemy in currentEnemies)
    //            {
    //                if (enemy.GetComponent<Character>().getHealth() <= 0)
    //                {
    //                    StartCoroutine(enemyDeath(enemy));
    //                    deathPhase = true;
    //                }
    //            }
    //        }
    //        catch { }
    //    }


    //    if (!preventEnemyMove)
    //    {


    //        if (currentPlayer.GetComponent<Character>().isLockPlayer() && (!currentPlayer.GetComponent<Attack>().getCanAttack() || !canAttackEnemy) && !enemyPhase)
    //        {
    //            enemyPhase = true;
    //            currentPlayer.GetComponent<Attack>().canNotAttack();
    //            StartCoroutine(deathPhaseOne());
    //        }
    //        if (currentRuin != null)
    //        {
    //            if (currentRuin.GetComponent<Ruins>().getHex().GetComponent<Hex>().getEnemyUnit() == null && numberSpawned < 4 && !enemyPhase)
    //            {
    //                if (Vector3.Distance(currentRuin.transform.position, currentPlayer.transform.position) < 3 && canSpawn)
    //                {
    //                    numberSpawned++;
    //                    GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], currentRuin.transform.position, Quaternion.identity);
    //                    newEnemy.GetComponent<Character>().setCurrentHex(currentRuin.GetComponent<Ruins>().getHex());
    //                    newEnemy.GetComponent<Character>().setHealth(Mathf.RoundToInt(50 * enemyStatMultiplier));
    //                    newEnemy.GetComponent<Character>().setAttack(Mathf.RoundToInt(10 * enemyStatMultiplier));
    //                    currentRuin.GetComponent<Ruins>().getHex().GetComponent<Hex>().setEnemyUnit(newEnemy);
    //                    currentEnemies.Add(newEnemy);
    //                    canSpawn = false;
    //                }
    //            }
    //            if (currentPlayer.transform.position == currentRuin.transform.position)
    //            {
    //                currentRuin.GetComponent<Ruins>().dropItem();
    //                Destroy(currentRuin);
    //            }
    //        }
    //    }



    //}






    private void instantiateMap()
    {
        currentMap = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
        currentMap.GetComponent<PlaceHexes>().setHexController(gameObject);
    }



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
    }




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


    public void movePlayer(GameObject destination)
    {

        if (currentPlayer.GetComponent<playerMovement>().canMove())
        {
            currentPlayer.GetComponent<playerMovement>().move(destination, 0.5f);
        }
           

    }

    public void attackEnemy(GameObject target)
    {
        if (currentPlayer.GetComponent<playerAttack>().getCanAttack())
        {
            currentPlayer.GetComponent<playerAttack>().attack(target);
        }
    }



    //private bool enemiesInRange(GameObject hex)
    //{
    //    foreach(GameObject enemy in currentEnemies)
    //    {
    //        if (Vector3.Distance(hex.transform.position, enemy.transform.position) <= currentPlayer.GetComponent<Character>().getRange()*1.2)
    //            return true;
    //    }
    //    return false;
    //}


    //IEnumerator enemyMovementPhase(float time)
    //{
    //    //yield return new WaitForSeconds(time);

    //    List<GameObject> adjacentHexes;
    //    List<GameObject> potentialHexes;
    //    GameObject destination;
    //    int moveRange = 3;
    //    foreach (GameObject enemy in currentEnemies)
    //    {
    //        try
    //        {
    //            enemy.GetComponent<Character>().getCurrentHex();
    //        }
    //        catch { yield break; }
    //        adjacentHexes = enemy.GetComponent<Character>().getCurrentHex().GetComponent<Hex>().getAdjacent();
    //        potentialHexes = new List<GameObject>();
    //        foreach (GameObject hex in adjacentHexes)
    //        {
    //            if (hex.GetComponent<Hex>().canMoveTo())
    //            {
    //                potentialHexes.Add(hex);
    //            }
    //        }

    //        if (potentialHexes.Count != 0)
    //        {
    //            destination = potentialHexes[Random.Range(0, potentialHexes.Count)];
    //            if (Vector3.Distance(enemy.transform.position, currentPlayer.transform.position) < moveRange)
    //            {
    //                foreach (GameObject hex in potentialHexes)
    //                {
    //                    if (enemy.GetComponent<SpriteRenderer>().sprite.name.Contains("Archer") && hex.GetComponent<Hex>().playerUnit != null)
    //                        continue;
    //                    if (Vector3.Distance(hex.transform.position, currentPlayer.transform.position) < Vector3.Distance(destination.transform.position, currentPlayer.transform.position))
    //                        destination = hex;
    //                }
    //            }

    //            enemy.GetComponent<Movement>().move(destination, time);
    //        }
    //        yield return new WaitForSeconds(time);
    //    }
    //    StartCoroutine(enemyAttackPhase(0.2f));
    //    yield return null;
    //}

    //IEnumerator enemyAttackPhase(float time)
    //{
    //    foreach (GameObject enemy in currentEnemies)
    //    {
    //        if (Vector3.Distance(enemy.transform.position, currentPlayer.transform.position) <= enemy.GetComponent<Character>().getRange())
    //        {
    //            enemy.GetComponent<Character>().unlockPlayer();
    //            enemy.GetComponent<Attack>().attackAtRange(currentPlayer.GetComponent<Character>().getCurrentHex());
    //            yield return new WaitForSeconds(Vector3.Distance(enemy.transform.position, currentPlayer.transform.position));
    //        }

    //    }
    //    StartCoroutine(deathPhaseTwo());
    //    yield return null;
    //}

    //public GameObject getCurrentPlayer()
    //{
    //    return currentPlayer;
    //}

}
