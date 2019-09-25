using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage;
    private bool player = false;
    // Start is called before the first frame update

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player && collision.gameObject.tag == "Enemy")
        {

            collision.gameObject.GetComponent<enemyCharacter>().setHealth(collision.gameObject.GetComponent<enemyCharacter>().getHealth() - damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name.Contains("Mountain"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!player && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerCharacter>().setHealth(collision.gameObject.GetComponent<playerCharacter>().getHealth() - damage);
            Destroy(gameObject);
        }
    }


    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void isPlayer()
    {
        player = true;
    }

}
