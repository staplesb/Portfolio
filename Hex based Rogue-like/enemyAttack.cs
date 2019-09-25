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

    public bool canAttackPlayer()
    {
        return canAttack;
    }

    public void attackCurrentHex()
    {
        StartCoroutine(attackCurrentPlayer());
    }

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

    public void attack()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= gameObject.GetComponent<enemyCharacter>().getRange())
        {
            StartCoroutine(launchAttack());
        }
        StartCoroutine(nextTurn());
    }

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
