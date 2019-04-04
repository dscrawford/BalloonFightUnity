using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collisions : MonoBehaviour
{
    private int currentHP = 2;
    public int bounciness = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Move>().ChangeVelocity(new Vector3(-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounciness, 0));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-gameObject.GetComponent<Rigidbody2D>().velocity.x, -bounciness / 2, 0);
        }
    }

    private void Death()
    {

    }
}
