using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_balloon : MonoBehaviour
{

    public GameObject balloon;
    public int maxTime = 6;
    public int seed = 0;
    

    private float xPos, yPos, xBalloonStart, yBalloonStart;
    private System.Random rand;
    private float timeLeft;
    private bool stop = false;
    private int totalBalloonsShot = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
        xBalloonStart = xPos;
        yBalloonStart = yPos - .1f;
        rand = new System.Random(seed);
        timeLeft = rand.Next(maxTime) + 1;
        //Debug.Log(xBalloonStart + " " + yBalloonStart);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0 && !stop)
        {
            timeLeft = rand.Next(maxTime) + 1;
            GameObject balloon_proj_handler;
            balloon_proj_handler = Instantiate(balloon, new Vector2(xBalloonStart, yBalloonStart), transform.rotation);

            totalBalloonsShot++;
            Destroy(balloon_proj_handler, 20f);
        }
    }

    public void Stop()
    {
        stop = true;
    }

    public int getTotalBalloonsShot()
    {
        return totalBalloonsShot;
    }
}
