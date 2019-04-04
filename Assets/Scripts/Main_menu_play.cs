using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu_play : MonoBehaviour
{
    float r, g, b;
    // Start is called before the first frame update
    void Start()
    {
        r = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color.r;
        g = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color.g;
        b = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                g.GetComponent<SpriteRenderer>().color = new Color(255, 102, 0, 1);

                pause();
                SceneManager.LoadScene("Map1");
            }


        }
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
    }
}
