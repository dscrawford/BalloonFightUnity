using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    private Animator anim;

    private float speedx = 0.001f;
    private float speedy = 0.0067f;
    private long frame = 0;
    private float period =90;

    public float startX = 0;
    public float startY = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Right", true);
        //transform.position = new Vector2(startX, startY);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (frame<period)
        {
            anim.SetBool("Right", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);
            frame++;
        }
        else if (frame<period*2)
        {
            anim.SetBool("Right", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedx, speedy);
            frame++;
        }
        else
            frame = 0;

        //Move steadily
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + GetComponent<Rigidbody2D>().velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Entity is popped
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }


}
