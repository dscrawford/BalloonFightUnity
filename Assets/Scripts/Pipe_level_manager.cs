using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipe_level_manager : MonoBehaviour
{
    public int numPipes = 4;
    public string nextScene = "Main_menu";

    protected List<GameObject> pipes = new List<GameObject>();
    private System.Random rand = new System.Random();
    private int limit;
    private int pipesFinished = 0;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipes.Add(g);
        }

            limit = rand.Next(15) + 12;
    }

    // Update is called once per frame
    void Update()
    {
        
        checkLimit(pipes);
        
        StartCoroutine(FinishLevel());

    }

    void checkLimit(List<GameObject> pipes)
    {
        foreach(GameObject g in pipes)
        {
            if(g.GetComponent<Pipe_balloon>().getTotalBalloonsShot() > limit)
            {
                Debug.Log(g.GetComponent<Pipe_balloon>().getTotalBalloonsShot());
                g.GetComponent<Pipe_balloon>().Stop();
                pipesFinished++;
                pipes.Remove(g);
            }
        }
    }

    IEnumerator FinishLevel()
    {
        if (pipesFinished == numPipes)
        {
            yield return new WaitForSeconds(4);
            Debug.Log("WE DONE");
            SceneManager.LoadScene(nextScene);

        }
    }
}
