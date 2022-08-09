using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerSlider : MonoBehaviour
{
    public Slider timerSlider;
    public Text[] num;
    public GameObject[] piramits;
    static int[] numbers=new int[6];
    
    public GameObject plus1;
    public GameObject plus2;
    public GameObject plus3;
    public GameObject mult1;
    public GameObject mult2;
    public GameObject mult3;
    public GameObject[] back;
    public static int [] result=new int[3];
    public static int[] quesNum = new int[3];


    
    public float gameTime;
    private bool stopTimer;

    private static bool picker;
    public static int penalty=0;
    public static int level = 1;
    public static int type = 1;
    public static int pickCount;
    static bool levelUp = false;

    static int backCount; 
    Vector3 []konum=new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        picker = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        StartCoroutine(numberGenerator());
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;
        time -= penalty;
        if (time <= 0)
        {
            stopTimer = true;   
        }
        if (stopTimer == false)
        {
            timerSlider.value = time;
        }
       for(int i=0 ; i < (type+1)*(type +2)/2; i++)
       {
            
            if(type==1&&quesNum[0]!=i)
                num[i].text= numbers[i].ToString();
            else if (type == 2)
            {
                if (System.Array.IndexOf(quesNum, i) == -1)
                {
                    num[i].text = numbers[i].ToString();
                }
            }
       }
        
    }
    public IEnumerator numberGenerator()
    {
       
        while (!stopTimer&&!(type==2&&level==7))
        {
            BackGround();
            if (type == 1)
            {
               
                if (levelUp)
                {
                    DecLevel();
                    levelUp = false;
                    for(int i = 0; i < numbers.Length; i++)
                    {
                        numbers[i] =0;
                    }
                }
                if (level == 1)
                {
                    numbers[2] = Random.Range(1, 10);
                    numbers[1] = Random.Range(1, 10);
                    numbers[0] = numbers[2] + numbers[1];                   
                }
                else if(level ==2)
                {
                    numbers[2] = Random.Range(5, 10);
                    numbers[1] = Random.Range(11, 20);
                    numbers[0] = numbers[2] + numbers[1];
                }
                else if(level==3)
                {
                    numbers[2] = Random.Range(11, 20);
                    numbers[1] = Random.Range(11, 20);
                    numbers[0] = numbers[2] + numbers[1];
                }else if (level == 4)
                {
                    numbers[2] = Random.Range(11, 50);
                    numbers[1] = Random.Range(11, 50);
                    if (numbers[1] % 10 == 0)
                    {
                        numbers[1] += Random.Range(1, 10);
                    }
                    if (numbers[2] % 10 == 0)
                    {
                        numbers[2] += Random.Range(1, 10);
                    }
                    numbers[0] = numbers[2] + numbers[1];
                }
                else if(level==5)
                {
                    numbers[2] = Random.Range(31, 50);
                    numbers[1] = Random.Range(31, 50);
                    if (numbers[1] % 10 == 0)
                    {
                        numbers[1] += Random.Range(1, 10);
                    }
                    if (numbers[2] % 10 == 0)
                    {
                        numbers[2] += Random.Range(1, 10);
                    }
                    numbers[0] = numbers[2] + numbers[1];
                }else if (level == 6)
                {
                    plus1.SetActive(false);
                    plus2.SetActive(false);
                    plus3.SetActive(false);
                    mult1.SetActive(true);
                    mult2.SetActive(false);
                    mult3.SetActive(false);
                    numbers[2] = Random.Range(1, 10);
                    numbers[1] = Random.Range(1, 10);
                    numbers[0] = numbers[2] * numbers[1];
                }else if (level == 7)
                {
                    numbers[2] = Random.Range(2, 6);
                    numbers[1] = Random.Range(11, 20);
                    numbers[0] = numbers[2] * numbers[1];
                }else if(level==8){
                    numbers[2] = Random.Range(11, 20);
                    numbers[1] = Random.Range(11, 20);
                    numbers[0] = numbers[2] * numbers[1];
                }else if (level == 9)
                {
                    numbers[2] = Random.Range(11, 50);
                    numbers[1] = Random.Range(11, 50);
                    if (numbers[1] % 10 == 0)
                    {
                        numbers[1] += Random.Range(1, 10);
                    }
                    if (numbers[2] % 10 == 0)
                    {
                        numbers[2] += Random.Range(1, 10);
                    }
                    numbers[0] = numbers[2] * numbers[1];
                }
                if (level < 8)
                {
                    quesNum[0] = Random.Range(0, 3);
                }
                else
                {
                    quesNum[0] = Random.Range(1, 3);
                }
                result[0] = numbers[quesNum[0]];
                num[quesNum[0]].text = "?";

            }
             
           
            if (type == 2)
            {
                if (levelUp)
                {
                    IncLevel();
                    levelUp = false;
                }
                if (level == 1)
                {
                    plus2.SetActive(true);
                    plus3.SetActive(true);
                    numbers[5] = Random.Range(1, 10);
                    numbers[4] = Random.Range(1, 10);
                    numbers[3] = Random.Range(1, 10);
                    numbers[2] = numbers[4] + numbers[5];
                    numbers[1] = numbers[3] + numbers[4];
                    numbers[0] = numbers[1] + numbers[2];
                }else if (level == 2)
                {
                    numbers[5] = Random.Range(2, 10);
                    numbers[4] = Random.Range(5, 10);
                    numbers[3] = Random.Range(10, 25);
                    numbers[2] = numbers[4] + numbers[5];
                    numbers[1] = numbers[3] + numbers[4];
                    numbers[0] = numbers[1] + numbers[2];
                }else if (level == 3)
                {
                    numbers[5] = Random.Range(5, 10);
                    numbers[4] = Random.Range(10, 25);
                    numbers[3] = Random.Range(10, 25);
                    numbers[2] = numbers[4] + numbers[5];
                    numbers[1] = numbers[3] + numbers[4];
                    numbers[0] = numbers[1] + numbers[2];
                }else if (level == 4)
                {
                    numbers[5] = Random.Range(10, 25);
                    numbers[4] = Random.Range(10, 25);
                    numbers[3] = Random.Range(10, 25);
                    numbers[2] = numbers[4] + numbers[5];
                    numbers[1] = numbers[3] + numbers[4];
                    numbers[0] = numbers[1] + numbers[2];
                }else if (level == 5)
                {
                    numbers[5] = Random.Range(20, 50);
                    numbers[4] = Random.Range(20, 50);
                    numbers[3] = Random.Range(20, 50);
                    numbers[2] = numbers[4] + numbers[5];
                    numbers[1] = numbers[3] + numbers[4];
                    numbers[0] = numbers[1] + numbers[2];
                }
                else if (level == 6)
                {
                    mult2.SetActive(true);
                    mult3.SetActive(true);
                    numbers[5] = Random.Range(4, 7);
                    numbers[4] = Random.Range(4, 6);
                    numbers[3] = Random.Range(4, 7);
                    numbers[2] = numbers[4] * numbers[5];
                    numbers[1] = numbers[3] * numbers[4];
                    numbers[0] = numbers[1] * numbers[2];
                }
                int quesType;
                if (level < 5)
                {
                    quesType = Random.Range(0, 4);
                }
                else
                {
                    quesType = Random.Range(1, 3);
                }
                if (quesType == 0)
                {
                    for (int i = 0; i < quesNum.Length; i++)
                        quesNum[i] = 2 - i;
                }
                else if (quesType == 1)
                {
                    quesNum[0] = 1;
                    quesNum[1] = 4;
                    quesNum[2] = 3;
                }
                else if (quesType == 2)
                {
                    quesNum[0] = 2;
                    quesNum[1] = 4;
                    quesNum[2] = 5;
                }
                else
                {
                    quesNum[0] = 0;
                    quesNum[1] = 5;
                    quesNum[2] = 3;
                }
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = numbers[quesNum[i]];
                    num[quesNum[i]].text = "?";
                }



            }

            yield return new WaitWhile(() => !picker);
            picker = false;
        }
        if (stopTimer)
        {
            //��k�� ko�ulu 2
        }  
    }
    public static void win()
    {

        penalty -= 3;
        level++;
        backCount++;
        if (level == 6)
        {
            level = 1;
            type++;
            levelUp = true;
        }else if (level == 10)
        {
            type = 2;
            level = 6;
            levelUp = true;
        }else if (type == 2 && level == 7)
        {
            Application.Quit();//��k�� ko�ulu
        }
        if (type == 3)
        {
            type = 1;
            level = 6;
           
        }
        picker = true;

    }
    public static void lose()
    {
        penalty += 3;
        picker = true;
      
    }
    void BackGround()
    {
        if(backCount<11)//resim say�s� artarsa artabilir
        back[backCount].SetActive(true);
    }
    public void IncLevel()
    {
        if (type == 2)
        {
            for (int i = 0; i < piramits.Length; i++)
            {
                RectTransform t = piramits[i].GetComponent<RectTransform>();
                Vector2 dimension = t.sizeDelta;
                dimension.x = 200;
                dimension.y = 125;
                t.sizeDelta = dimension;
                piramits[i].SetActive(true);
                if (i == 1 || i == 2)
                {
                    konum[i-1] = t.localPosition;
                    t.localPosition= new Vector3(140*Mathf.Pow(-1,i),350, 0);
                    mult1.transform.localPosition= new Vector3(0, 350, 0);
                    plus1.transform.localPosition= new Vector3(0, 350, 0); 
                }
                
            }
                     
        }
    }
    void DecLevel()
    {
        
        for (int i = 0; i < piramits.Length; i++)
        {
            RectTransform t = piramits[i].GetComponent<RectTransform>();
            Vector2 dimension = t.sizeDelta;
            dimension.x = 250;
            dimension.y = 150;
            t.sizeDelta = dimension;
            if (i > 2)
                piramits[i].SetActive(false);

            if (i == 1 || i == 2)
            {
                t.localPosition = konum[i-1];
                mult1.SetActive(true);
                mult1.transform.localPosition = new Vector3(0, 334, 0);
            }

        }
    }
}
