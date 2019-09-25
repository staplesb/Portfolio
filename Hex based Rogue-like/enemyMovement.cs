using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    private GameObject player;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && gameObject.transform.position != gameObject.GetComponent<enemyCharacter>().currentHex.transform.position && gameObject.GetComponent<enemyCharacter>().currentHex.GetComponent<Hex>().getPlayerUnit() == null)
            gameObject.GetComponent<Rigidbody2D>().position = gameObject.GetComponent<enemyCharacter>().currentHex.transform.position;
    }

    public void move()
    {
        moving = true;
        StartCoroutine(findDestinationHex());
    }

    private IEnumerator findDestinationHex()
    {
        List<GameObject> adjacentHexes;
        List<GameObject> potentialHexes;
        GameObject destination;
        int moveRange = 3;
        adjacentHexes = gameObject.GetComponent<enemyCharacter>().getCurrentHex().GetComponent<Hex>().getAdjacent();
        potentialHexes = new List<GameObject>();
        foreach (GameObject hex in adjacentHexes)
        {
            if (hex.GetComponent<Hex>().canMoveTo())
            {
                potentialHexes.Add(hex);
            }
        }

        if (potentialHexes.Count != 0)
        {
            destination = potentialHexes[Random.Range(0, potentialHexes.Count)];
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < moveRange)
            {
                foreach (GameObject hex in potentialHexes)
                {
                    if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Archer") && hex.GetComponent<Hex>().getPlayerUnit() != null)
                        continue;
                    if (Vector3.Distance(hex.transform.position, player.transform.position) < Vector3.Distance(destination.transform.position, player.transform.position))
                        destination = hex;
                }
            }
            destination.GetComponent<Hex>().setEnemyUnit(gameObject);
            orientCharacter(destination);
        }
        
        yield return null;
    }

    public void orientCharacter(GameObject destination)
    {
        float time = 0.5f;
        float diffX = destination.transform.position.x - gameObject.transform.position.x;
        float diffY = destination.transform.position.y - gameObject.transform.position.y;


        if (diffX > 0 && diffY > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().rotation = 60f;
        }
        else if (diffX > 0 && diffY < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().rotation = -60f;
        }
        else if (diffX < 0 && diffY > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().rotation = -60f;
        }
        else if (diffX < 0 && diffY < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().rotation = 60f;
        }

        if (diffX > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.name == "EnemyCanvas")
                {
                    child.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (destination.GetComponent<SpriteRenderer>().sprite.name.Contains("Grass"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        StartCoroutine(moveToDestination(destination, time));

    }

    private IEnumerator moveToDestination(GameObject destination, float time)
    {
        float lerpTime = 0.0f;
        Vector3 startPos = gameObject.transform.position;
        Vector3 adjustedDestination = destination.transform.position;

        if (destination.GetComponent<Hex>().getPlayerUnit() != null)
        {
            if (adjustedDestination.x > gameObject.transform.position.x)
            {
                adjustedDestination.x -= 0.25f;
                destination.GetComponent<Hex>().getPlayerUnit().GetComponent<Rigidbody2D>().position = new Vector3(adjustedDestination.x + 0.5f, adjustedDestination.y, adjustedDestination.z);
            }
            else
            {
                adjustedDestination.x += 0.25f;
                destination.GetComponent<Hex>().getPlayerUnit().GetComponent<Rigidbody2D>().position = new Vector3(adjustedDestination.x - 0.5f, adjustedDestination.y, adjustedDestination.z);
            }
        }

        while (lerpTime < 1)
        {
            lerpTime += Time.deltaTime / time;
            gameObject.GetComponent<Rigidbody2D>().position = Vector3.Lerp(startPos, adjustedDestination, lerpTime);
            yield return null;
        }
        gameObject.transform.rotation = new Quaternion();
        gameObject.GetComponent<enemyCharacter>().setCurrentHex(destination);

        gameObject.transform.localScale = new Vector3(1, 1, 1);

        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.name == "EnemyCanvas")
            {
                child.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        gameObject.GetComponent<Animator>().SetTrigger("Idle");

        if (destination.transform.position != adjustedDestination)
        {
            gameObject.GetComponent<enemyAttack>().attackCurrentHex();
        } else if (gameObject.GetComponent<enemyAttack>().canAttackPlayer())
            gameObject.GetComponent<enemyAttack>().attack();
        moving = false;
        yield return null;
    }

    
}
