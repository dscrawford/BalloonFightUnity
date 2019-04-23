using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkyCollision : MonoBehaviour
{
    public bool bounceGround = true;
    public float speedx = -0.003f;
    public float speedy = -0.003f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);

        //Move steadily
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + GetComponent<Rigidbody2D>().velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Entity is popped and Player is Killed
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Move>().Electrocute();
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Ground")  //Bounce off Ground
        {
            print("Are we there yet");
            float tempx = collision.GetContact(0).normal.x;
            float tempy = collision.GetContact(0).normal.y;
            if(tempx != 0)
            {
                speedx = -speedx;
                speedy = speedy;
            }
            if(tempy != 0)
            {
                speedx = speedx;
                speedy = -speedy;
            }
        }
    }


}