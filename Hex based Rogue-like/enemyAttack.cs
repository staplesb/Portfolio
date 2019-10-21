using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{

    public GameObject projectilePrefab;
    private bool canAttack;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Swordsman"))
            canAttack = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Return if this enemy can attack
    public bool canAttackPlayer()
    {
        return canAttack;
    }
    
    //Called if enemy and player occupy the same hex
    public void attackCurrentHex()
    {
        StartCoroutine(attackCurrentPlayer());
    }
    
    //Coroutine to trigger attack, wait for attack to finish, and then deduct health based on damage done. 
    public IEnumerator attackCurrentPlayer()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        player.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        while (!gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Idle") && !player.GetComponent<SpriteRenderer>().sprite.name.Contains("Idle"))
            yield return null;
        player.GetComponent<playerCharacter>().setHealth(player.GetComponent<playerCharacter>().getHealth() - gameObject.GetComponent<enemyCharacter>().getAttack());
        gameObject.GetComponent<enemyCharacter>().setHealth(gameObject.GetComponent<enemyCharacter>().getHealth() - player.GetComponent<playerCharacter>().getAttack());
        yield return null;
    }
    
    //Called to attack an enemy at range
    public void attack()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= gameObject.GetComponent<enemyCharacter>().getRange())
        {
            StartCoroutine(launchAttack());
        }
        StartCoroutine(nextTurn());
    }
    
    //Coroutine to start next turn when enemies are done attacking
    private IEnumerator nextTurn()
    {
        yield return new WaitForSeconds(0.2f);
        while (GameObject.FindGameObjectWithTag("Projectile") != null)
        {
            yield return null;
        }
        if (!player.GetComponent<playerMovement>().canMove())
        {
            player.GetComponent<playerMovement>().newTurn();
            player.GetComponent<playerAttack>().newTurn();
        }
            
        yield return null;
    }

    //Coroutine to launch a ranged attack
    private IEnumerator launchAttack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        GameObject projectile;
        projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().setDamage(gameObject.GetComponent<enemyCharacter>().getAttack());
        projectile.GetComponent<Rigidbody2D>().velocity = (player.transform.position - gameObject.transform.position).normalized;

        float diffX = player.transform.position.x - projectile.transform.position.x;
        float diffY = player.transform.position.y - projectile.transform.position.y;
        
        //Need to fix so that it is dynamic. 
        if (diffX == 0 && diffY > 0)
        {
        }
        else if (diffX == 0 && diffY < 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = -180f;
        }
        else if (diffX < 0 && diffY == 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = 90f;
        }
        else if (diffX > 0 && diffY == 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = -90f;
        }
        else if (diffX > 0 && diffY > 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = -30f;
        }
        else if (diffX > 0 && diffY < 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = -150f;
        }
        else if (diffX < 0 && diffY > 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = 30f;
        }
        else if (diffX < 0 && diffY < 0)
        {
            projectile.GetComponent<Rigidbody2D>().rotation = 150f;
        }
        yield return null;
    }
}
