using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform_level_manager : MonoBehaviour
{

    private List<GameObject> enemies = new List<GameObject>();
    private int numEnemies;
    private int numDeadEnemies;
    public string nextScene = "Pipes_1";
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(g);
            Debug.Log("added one");
        }

        numEnemies = enemies.Count;
        numDeadEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
        StartCoroutine(FinishLevel());
    }

    void checkDeath()
    {
        foreach (GameObject g in enemies)
        {
            if (g.GetComponent<BasicEnemy1>().dead)
            {
                Debug.Log("enemy died");
                
                numDeadEnemies++;
                enemies.Remove(g);
            }
        }
    }

    IEnumerator FinishLevel()
    {
        if (numDeadEnemies == numEnemies)
        {
            yield return new WaitForSeconds(4);
            Debug.Log("WE DONE");
            SceneManager.LoadScene(nextScene);

        }
    }
}
