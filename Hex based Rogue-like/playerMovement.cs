using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private bool firstMove = true;
    private bool secondMove = false;
    private bool moving = false;
    private bool crossMountains = false;
    private bool crossWater = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && gameObject.transform.position != gameObject.GetComponent<playerCharacter>().currentHex.transform.position && gameObject.GetComponent<playerCharacter>().currentHex.GetComponent<Hex>().getEnemyUnit() == null)
            gameObject.GetComponent<Rigidbody2D>().position = gameObject.GetComponent<playerCharacter>().currentHex.transform.position;
    }
    
    //Determine whether a player can move to a selected location, orient the player, and start the coroutine to move the player
    public void move(GameObject destination, float time)
    {

        if (Vector3.Distance(gameObject.transform.position, destination.transform.position) > 1.25f || moving)
            return;
        else if (destination.GetComponent<SpriteRenderer>().sprite.name.Contains("Mountain") && !crossMountains || destination.GetComponent<SpriteRenderer>().sprite.name.Contains("Water") && !crossWater)
            return;

        moving = true;
        gameObject.GetComponent<playerCharacter>().getCurrentHex().GetComponent<Hex>().setPlayerUnit(null);
        gameObject.GetComponent<playerCharacter>().setCurrentHex(destination);
        gameObject.GetComponent<playerCharacter>().getCurrentHex().GetComponent<Hex>().setPlayerUnit(gameObject);

        float diffX = destination.transform.position.x - gameObject.transform.position.x;
        float diffY = destination.transform.position.y - gameObject.transform.position.y;

        //This should be made to be more dynamic. 
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

        if(diffX > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        
        //Let the player move twice if the first hex he goes to is grasslands. Also use a different animation. 
        if (firstMove && destination.GetComponent<SpriteRenderer>().sprite.name.Contains("Grass"))
        {
            firstMove = false;
            secondMove = true;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        } else if (secondMove && destination.GetComponent<SpriteRenderer>().sprite.name.Contains("Grass"))
        {
            secondMove = false;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
            
        }
        else
        {
            firstMove = false;
            secondMove = false;
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }
        StartCoroutine(moveToDestination(destination, time));
    }
    
    //Coroutine to move the player to a destination hex over a given amount of time. 
    IEnumerator moveToDestination(GameObject destination, float time)
    {
        float lerpTime = 0.0f;
        Vector3 startPos = gameObject.transform.position;
        Vector3 adjustedDestination = destination.transform.position;

        if (destination.GetComponent<Hex>().getEnemyUnit() != null)
        {
            if (adjustedDestination.x > gameObject.transform.position.x)
            {
                adjustedDestination.x -= 0.25f;
                destination.GetComponent<Hex>().getEnemyUnit().GetComponent<Rigidbody2D>().position = new Vector3(adjustedDestination.x + 0.5f, adjustedDestination.y, adjustedDestination.z);
            }
            else
            {
                adjustedDestination.x += 0.25f;
                destination.GetComponent<Hex>().getEnemyUnit().GetComponent<Rigidbody2D>().position = new Vector3(adjustedDestination.x - 0.5f, adjustedDestination.y, adjustedDestination.z);
            }    
        }

        while (lerpTime < 1)
        {
            lerpTime += Time.deltaTime / time;
            gameObject.GetComponent<Rigidbody2D>().position = Vector3.Lerp(startPos, adjustedDestination, lerpTime);
            yield return null;
        }
        gameObject.transform.rotation = new Quaternion();
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Animator>().SetTrigger("Idle");
        if (destination == null) { }
        else if(destination.transform.position != adjustedDestination)
        {
            gameObject.GetComponent<playerAttack>().attackCurrentHex();
        }

        moving = false;
        yield return null;
    }
    
    //Reset for a new turn
    public void newTurn()
    {
        firstMove = true;
    }
    
    //Return true if the player can move
    public bool canMove()
    {
        if (firstMove || secondMove)
            return true;
        return false;
    }
    
    //Return ture if the player is moving
    public bool getMoving()
    {
        return moving;
    }
    
    //If the player collides with an item, pick it up and give the plater the desired stats
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && gameObject.tag != "Enemy")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Life"))
            {
                gameObject.GetComponent<playerCharacter>().setHealth(gameObject.GetComponent<playerCharacter>().getHealth() + 50);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Atk"))
            {
                gameObject.GetComponent<playerCharacter>().setAttack(gameObject.GetComponent<playerCharacter>().getAttack() + 13);
            }
            else
            {
                collision.transform.SetParent(gameObject.transform);
                collision.transform.localPosition = Vector3.zero;
                collision.transform.localRotation = Quaternion.identity;
            }
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Wings"))
            {
                crossMountains = true;
                collision.GetComponent<Rigidbody2D>().Sleep();
            } else
            {
                crossWater = true;
                collision.gameObject.SetActive(false);
            }
                
        }
    }

    
}
