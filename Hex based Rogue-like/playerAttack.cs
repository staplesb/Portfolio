using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public List<GameObject> projectilePrefabs;
    private bool canAttack;
    private bool canAttackEnemy;
    private bool attacking;
    private GameObject AIManager;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        canAttackEnemy = false;
        attacking = false;
        if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Swordsman"))
            canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAIManager(GameObject AIManager)
    {
        this.AIManager = AIManager;
    }

    private bool enemiesInRange()
    {
        foreach(Transform child in AIManager.transform)
        {
            if (Vector3.Distance(gameObject.GetComponent<playerCharacter>().getCurrentHex().transform.position, child.position) <= gameObject.GetComponent<playerCharacter>().getRange())
                return true;
        }
        return false;
    }

    public bool getCanAttackEnemy()
    {
        return attacking || (canAttack && enemiesInRange());
    }

    public bool getCanAttack()
    {
        return canAttack;
    }

    public void newTurn()
    {
        canAttack = true;
    }

    public void attackCurrentHex()
    {
        attacking = true;
        StartCoroutine(attackCurrentEnemy());
    }

    public IEnumerator attackCurrentEnemy()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        GameObject enemyUnit = gameObject.GetComponent<playerCharacter>().getCurrentHex().GetComponent<Hex>().getEnemyUnit();
        enemyUnit.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        while (!gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Idle") && !enemyUnit.GetComponent<SpriteRenderer>().sprite.name.Contains("Idle"))
            yield return null;
        if (gameObject.GetComponent<Animator>().name.Contains("Swordsman"))
        {
            enemyUnit.GetComponent<enemyCharacter>().setHealth(enemyUnit.GetComponent<enemyCharacter>().getHealth() - gameObject.GetComponent<playerCharacter>().getAttack() * 2);
            gameObject.GetComponent<playerCharacter>().setHealth(gameObject.GetComponent<playerCharacter>().getHealth() - enemyUnit.GetComponent<enemyCharacter>().getAttack());
        }
        else
        {
            enemyUnit.GetComponent<enemyCharacter>().setHealth(enemyUnit.GetComponent<enemyCharacter>().getHealth() - gameObject.GetComponent<playerCharacter>().getAttack());
            gameObject.GetComponent<playerCharacter>().setHealth(gameObject.GetComponent<playerCharacter>().getHealth() - enemyUnit.GetComponent<enemyCharacter>().getAttack());
        }
            
        attacking = false;
        yield return null;
    }

    public void attack(GameObject target)
    {
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= gameObject.GetComponent<playerCharacter>().getRange())
        {
            canAttack = false;
            StartCoroutine(launchAttack(target));
        }
    }


    private IEnumerator launchAttack(GameObject target)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        GameObject projectile;

        if (gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Contains("Archer"))
        {
            projectile = Instantiate(projectilePrefabs[0], gameObject.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().setDamage(gameObject.GetComponent<playerCharacter>().getAttack());
        }
        else
        {
            print(gameObject.GetComponent<Animator>().name);
            projectile = Instantiate(projectilePrefabs[1], gameObject.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().setDamage(gameObject.GetComponent<playerCharacter>().getAttack()*2);
        }    

        projectile.GetComponent<Projectile>().isPlayer();
        projectile.GetComponent<Rigidbody2D>().velocity = (target.transform.position - gameObject.transform.position).normalized;

        float diffX = target.transform.position.x - projectile.transform.position.x;
        float diffY = target.transform.position.y - projectile.transform.position.y;

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
