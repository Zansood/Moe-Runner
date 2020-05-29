using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject agent;
    public GameObject target;
    public Text CountZom;
    float Count;
    public UI ui;

    void Start()
    {
        // Invoke("SpawnAgent",2);
        CountZom.color = Color.yellow;
    }
    void Update()
    {
       /* if (Spawn == true)
        {
            Count += 1;
            CountZom.text = Count.ToString("Zombie : 0");
            Spawn = false;
        }

        if (UI.check == true)
        {
            Invoke("SpawnAgent", 0);
            UI.check = false;
        }*/
    }

    // Update is called once per frame
   /* void SpawnAgent()
    {
        GameObject ag = Instantiate(agent, new Vector3(Random.Range(-20,-5),0,Random.Range(40,100)),Quaternion.identity);
        ag.GetComponent<Zombieswalk>().goal = target.transform;
        Spawn = true;
    }*/
}
