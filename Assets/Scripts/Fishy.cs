﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishy : MonoBehaviour
{
    List<GameObject> targets = new List<GameObject>();
    Vector2 fishPos = new Vector2();
    private float biteSpotX1, biteSpotX2, biteSpotY1, biteSpotY2;
    public float biteRangeY;
    int targetIndex;
    bool isBiting;
    bool reachedTop;
    public float biteSpeed = 1.5f;
    Vector2 endPos;
    Vector2 defaultPos;
    Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targets.Add(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            targets.Add(g);
        
        targetIndex = -1;
        isBiting = false;
        reachedTop = false;
        defaultPos = transform.position;
        endPos = defaultPos;

        biteSpotY1 = -1;
        biteSpotY2 = biteSpotY1 + 0.20f;
        biteSpotX1 = -0.32f;
        biteSpotX2 = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBiting)
        {
            targetIndex = isInBiteRange();
            gameObject.GetComponent<Rigidbody2D>().position = defaultPos;
        }
       
        
        //Check to see if target is near bite spot
        if (targetIndex >= 0 && !isBiting)
        {
            isBiting = true;
            reachedTop = false;

            //Move fish to bite spot
            fishPos.x = targets[targetIndex].GetComponent<Rigidbody2D>().position.x;
            fishPos.y = targets[targetIndex].GetComponent<Rigidbody2D>().position.y - .3f;
            gameObject.GetComponent<Rigidbody2D>().position = fishPos;

            //Start biting animation
            targetPos = targets[targetIndex].GetComponent<Rigidbody2D>().position;
            targetPos.y -= .085f;

        }
        else if(isBiting) //if fish is currently biting
        {
            //PHYSICS
            //if the fish hasn't reached it's peak target location, keep moving towards it
            if (Vector2.Distance(transform.position, targetPos) > .07f && !reachedTop)
                transform.position = Vector2.Lerp(transform.position, targetPos, biteSpeed * Time.deltaTime);
            else
                reachedTop = true;

            if (reachedTop) //if the fish has reached it's target destination, move back to the default spot and get ready to bite again.
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, defaultPos.y), biteSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, new Vector2(transform.position.x, defaultPos.y)) < .05f)
                    isBiting = false;
            }
            EatObject(targets[targetIndex]);
        }

    }

    int isInBiteRange()
    {
        float x, y;
        foreach(GameObject t in targets)
        {
            x = t.GetComponent<Rigidbody2D>().position.x;
            y = t.GetComponent<Rigidbody2D>().position.y;
            if (x < biteSpotX2 && x > biteSpotX1)
                if (y < biteSpotY2 && y > biteSpotY1)
                    return targets.IndexOf(t);
            
        }

        return -1;
    }

    void EatObject(GameObject obj)
    {
        if (obj.tag == "Player")
        {
            obj.GetComponent<Player_Move>().ChangePosition(transform.position);
        }

        if (obj.tag == "Enemy")
        {
            if (!obj.GetComponent<BasicEnemy1>().dead)
                obj.GetComponent<BasicEnemy1>().ChangePosition(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if there's a collision with one of the targets while the animation is playing, stop their movement and drag them down with the fish
        // then execute their Die() function

    }
}
