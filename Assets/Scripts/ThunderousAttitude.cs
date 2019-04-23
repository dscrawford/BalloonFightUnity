using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderousAttitude : MonoBehaviour
{
    private Animator anim;

    public GameObject sparky;

    //The actual player
    public Transform target;
    

    private long frame;
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //SET THE PLAYER'S TAG TO "Player" IN THE INSPECTOR FOR THIS TO WORK.
        anim.SetBool("Flared", false);
    }

    // Update is called once per frame
    void Update()
    {
        //When player comes within certain range to storm cloud
        //it will be weighted by a probability (around 25% to 100% increasing towards clouds coords.)
        //if this probability is hit then summon sparky out

        if (frame>65) {
            anim.SetBool("Flared", false);
            frame = 0;  //reset frame
            SummonSparky();
        }
        else if (frame >= 1){   //count the frame for animation
            frame++;
        }
        else if (frame <= -100)  //when distant*chance detection is recovered
        {
            frame = 0;  //reset frame
        }
        else if (frame <= -1)   //count the frame after distance*chance detection fails
        {
            frame--;
        }
        else if(Vector2.Distance(transform.position, target.position) < 0.3)   //player comes within range (0.4)
        {   
            if(rand.Next((int)(Vector2.Distance(transform.position, target.position) * 100)) < 8)   //weight the distance toward the center of the cloud as probability of strike
            {//cloud will strike
                anim.SetBool("Flared", true);
                frame = 1;  //start counting frames
            }
            else
            {//setup the distance*chance wait
                frame = -1;
            }
        }
    }

    void SummonSparky()
    {
        //instantiate new sparky
        Instantiate(sparky, new Vector3(-0.1f, -0.35f, transform.position.z-10f) + transform.position, Quaternion.identity);
    }
}
