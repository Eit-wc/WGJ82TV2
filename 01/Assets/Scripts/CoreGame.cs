using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public GameObject scoreText;
       PlayerControl playerControl;


       public float decreseOverTime = 10.0f;
       public float decreseOverTimeMax = 0.1f;
       public float decreseTimeMultiply = 0.999f;
       private float currentTimeDecrese = 0.0f;
       public int currentDecrese = -1;

        public GameObject gmaeOverText;

        public GameObject timeTextObject;
        private TMPro.TMP_Text textTime;
        private float currentTime= 0;
    
    void Start()
    {
        textTime = timeTextObject.GetComponent<TMPro.TMP_Text>();
        currentTime = 0.0f;


        Random.InitState((int)System.DateTime.Now.Ticks);
        //UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks; 
        playerControl = player.GetComponent<PlayerControl>();
        Global.score = 10;
        gmaeOverText = GameObject.FindWithTag("GameOverTag");
        textTime.text = "";
    }

    // Update is called once per frame
    private void FixedUpdate() {

        if(!Global.Gameover)
        {
            scoreText.GetComponent<TMPro.TMP_Text>().text = Global.score.ToString();

                currentTimeDecrese += Time.deltaTime;
            if(currentTimeDecrese > decreseOverTime)
            {
                decreseOverTime = decreseOverTime*decreseTimeMultiply;
                if(decreseOverTime < decreseOverTimeMax)
                {
                    decreseOverTime = decreseOverTimeMax;
                }
                //currentDecrese -= 1;
                Global.score += currentDecrese;
                currentTimeDecrese = 0.0f;
            }

            //gameover condition
            if(Global.score <= 0)
            {
                Debug.Log("GameOver");
                Global.Gameover=true;
                foreach (Transform item in gmaeOverText.transform)
                {
                    item.gameObject.SetActive(true);
                }
                StartCoroutine("restart");
            }
        }
        //scoreText.GetComponent<TMPro.TMP_Text>().text = "Hello";
        // update time
        if(!Global.Gameover)
        {
            currentTime += Time.deltaTime;
        }else
        {
            textTime.text = ((int)currentTime).ToString();
        }
        
        
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(4);
        Global.Gameover = false;
        Application.LoadLevel(Application.loadedLevel);
    }
    
}
