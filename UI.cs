using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Image HP1;
    public static float currentTime = 0f;
    float startingTime = 10f;
    public Text cooldownTime;
    public static bool check = false;
    public static float count = 0f;
    public Text Key;
    public Chancontroller chancontroller;
    public GameObject Wave1;
    public GameObject Wave2;
    public GameObject Wave3;
    public GameObject Wave4;
    public GameObject Wave5;
    public float WaveCount= 0f;
    public Text WaveShow;
    public GameObject lose;
    public Text Ready;
    public GameObject WIN;
    public GameObject CONTINUE;
    public Camerafollow camfoll;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        WaveShow.canvasRenderer.SetAlpha(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HP1.fillAmount -= 0.1f;
            print("E");
            //hptype -= 0.01f;
            //print(hptype);
        }
        if (HP1.fillAmount == 0)
        {
            chancontroller.Dead();
            chancontroller.notcontrol();
            print("Dead");
        }
        if (HP1.fillAmount > 0)
        {
            chancontroller.notDead();
            print("notDead");
        }
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            HP1.fillAmount += 0.1f;
            print("E");
            //hptype -= 0.01f;
            //print(hptype);
            if (HP1.fillAmount == 1)
            {
                print("isfull");
            }
        }

        if(HP1.fillAmount == 0)
        {
            lose.gameObject.SetActive(true);
        }
        if(count == 5)
        {
            WIN.gameObject.SetActive(true);
            count = 0;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            CONTINUE.gameObject.SetActive(true);
            Time.timeScale = 0;

        }

        currentTime -= 1 * Time.deltaTime;

        cooldownTime.text = currentTime.ToString("TIME : 0");
        Ready.text = currentTime.ToString("START IN  0");
        Ready.color = Color.yellow;


        Key.text = count.ToString("Key : 0/5");
        Key.color = Color.yellow;

        if(count >= 5)
        {
            count = 5;
        }

        cooldownTime.color = Color.yellow;

        if(currentTime <= 0)
        {
            Ready.gameObject.SetActive(false);
            currentTime = 60;
            WaveCount += 1;
            Spawnwave();
        }
        if(currentTime <= 3)
        {
            cooldownTime.color = Color.red;
        }
    }
    /*public void HPLess()
    {
        HP1.fillAmount += 0.1f;
        print("E");
        //hptype -= 0.01f;
        //print(hptype);
        if (HP1.fillAmount == 1)
        {
            print("isfull");
        }
    }*/
    public void dontuseitem()
    {
        print(999);
        HP1.fillAmount -= 0.0f;
    }
    public void Attackplayer()
    {
        //print(999);
        HP1.fillAmount -= 0.1f;
    }

    public void Spawnwave()
    {
        if (WaveCount == 1)
        {
            Wave1.SetActive(true);
            WaveShow.gameObject.SetActive(true);
            WaveShow.color = Color.yellow;
            WaveShow.text = WaveCount.ToString("WAVE 1");
            StartCoroutine(Delay());
        }
        else if (WaveCount == 2)
        {
            Wave1.SetActive(false);
            Wave2.SetActive(true);
            WaveShow.gameObject.SetActive(true);
            WaveShow.color = Color.yellow;
            WaveShow.text = WaveCount.ToString("WAVE 2");
            StartCoroutine(Delay());
        }
        else if (WaveCount == 3)
        {
            Wave2.SetActive(false);
            Wave3.SetActive(true);
            WaveShow.gameObject.SetActive(true);
            WaveShow.color = Color.yellow;
            WaveShow.text = WaveCount.ToString("WAVE 3");
            StartCoroutine(Delay());
        }
        else if (WaveCount == 4)
        {
            Wave3.SetActive(false);
            Wave4.SetActive(true);
            WaveShow.gameObject.SetActive(true);
            WaveShow.color = Color.yellow;
            WaveShow.text = WaveCount.ToString("WAVE 4");
            StartCoroutine(Delay());
        }
        else if (WaveCount == 5)
        {
            Wave4.SetActive(false);
            Wave5.SetActive(true);
            WaveShow.gameObject.SetActive(true);
            WaveShow.color = Color.yellow;
            WaveShow.text = WaveCount.ToString("WAVE 5");
            StartCoroutine(Delay());
        }
        else
        {
            if(WaveCount == 6)
            {
                lose.gameObject.SetActive(true);
            }
        }
    }
   void fadeOut()
    {
        WaveShow.gameObject.SetActive(false);
        
    }
    public IEnumerator Delay()
    {
        WaveShow.CrossFadeAlpha(0, 3, false);
        yield return new WaitForSeconds(3f);
        fadeOut();
    }
    public void AGAIN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("MENU");
    }
}
