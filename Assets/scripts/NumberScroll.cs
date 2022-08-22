using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class NumberScroll : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public Text [] ques;
    public AudioSource scroll;
    public Image [] Click;
    public Button[] buttons;


    private int positionLeft;
    private int positionRight;
    public static int select=-1;
    bool dragLeft = false;
    bool dragRight = false;
    bool truePick;
    bool pickWaiter;
    int TrueSelect;
    public static bool LearnEnd;
    bool [] pickTime;//do�ru se�im zamanlamas� i�in
    Sequence myseq1;
    Sequence myseq2;
    Sequence myseq3;
    // Start is called before the first frame update
    void Start()
    {
        positionLeft = (((int)left.localPosition.y+63)/100);
        positionRight = (((int)right.localPosition.y+63) / 100);
        LearnEnd = true;
        pickTime = new bool[14];
        for(int i = 0; i < pickTime.Length; i++)
        {
            pickTime[i] = true;
        }
        truePick = true;
        pickWaiter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerSlider.stopTimer)
        {
            left.GetComponent<Image>().DOFade(0, 0.5f).SetAutoKill();
            right.GetComponent<Image>().DOFade(0, 0.5f).SetAutoKill();
        }
        if (TimerSlider.timeBraker&&LearnEnd)
        {
            LearnEnd = false;
            HowtoPlay();
            truePick = false;
        }
        if ((dragLeft || dragRight)&&pickWaiter)
        {
            scroll.UnPause();
            positionLeft = (((int)left.localPosition.y + 63) / 100);
            positionRight = (((int)right.localPosition.y + 63) / 100);
            if (positionLeft > 9)
            {
                positionLeft = 9;
            }
            if (positionRight > 9)
            {
                positionRight = 9;
            }
           
            if(!(TimerSlider.type==2&&select==-1))//6 l� piramitte t�klamadan yazmay� engelleme
                write();
            
            
        }
        /*else
            scroll.Pause();*/
        if (!dragLeft)
        {
            lerpLeft(positionLeft * 100);
        }
        if (!dragRight)
        {
            lerpRight(positionRight * 100);
        }
       
    }
    void write()
    {
        if (select == -1)
        {
            if (positionLeft != 0)
                ques[TimerSlider.quesNum[Comparator.onayCount]].text = positionLeft + "" + positionRight;
            else
                ques[TimerSlider.quesNum[Comparator.onayCount]].text = "" + positionRight;
        }

        else
        {
            if (positionLeft != 0)
                ques[select].text = positionLeft + "" + positionRight;
            else
                ques[select].text = "" + positionRight;
        }
        for (int i = 0; i < ques.Length; i++)
        {
            ques[i].fontSize = 100;
        }
    }
    public void StartDragLeft()
    {
        dragLeft = true;
    }
    public void EndDragLeft()
    {
        dragLeft = false;
    }
    public void StartDragRight()
    {
        dragRight = true;
    }
    public void EndDragRight()
    {
        dragRight = false;
    }
    void lerpLeft(int position)
    {
        float newY = Mathf.Lerp(left.localPosition.y, position, Time.deltaTime * 30f);
        Vector2 newPos = new Vector2(left.localPosition.x,newY);
        left.localPosition = newPos;
    }
    void lerpRight(int position)
    {
        float newY = Mathf.Lerp(right.localPosition.y, position, Time.deltaTime * 30f);
        Vector2 newPos = new Vector2(right.localPosition.x, newY);
        right.localPosition = newPos;
    }
    public void select1()
    {
        if (truePick)
            select = 0;
        else if (TrueSelect == 0)
            pickWaiter = true;
    }
    public void select2()
    {
        if (truePick)
            select = 1;
        else if (TrueSelect == 1)
            pickWaiter=true;
    }
    public void select3()
    {
        if (truePick)
            select = 2;
        else if (TrueSelect == 2)
            pickWaiter=true;
    }
    public void select4()
    {
        if (truePick)
            select = 3;
        else if (TrueSelect == 3)
            pickWaiter=true;
    }
    public void select5()
    {
        if (truePick)
            select = 4;
        else if (TrueSelect == 4)
            pickWaiter=true;
    }
    public void select6()
    {
        if (truePick)
            select = 5;
        else if (TrueSelect == 5)
            pickWaiter=true;
    }
     void HowtoPlay()
    {
        if (TimerSlider.type == 2)
        {  
            StartCoroutine(tripleClick());
        }
        else
        {
            StartCoroutine(soloClick(TimerSlider.quesNum[0]));
        } 
       


    }
    IEnumerator soloClick(int k)
    {
        TrueSelect = k;
        Sequence myseq = DOTween.Sequence();
        myseq.Append(Click[k].DOFade(1,1f)).SetEase(Ease.Linear);
        myseq.AppendInterval(1f);
        myseq.Append(Click[k].DOFade(0,1f)).SetEase(Ease.Linear);
        myseq.SetLoops(5);
        StartCoroutine(pickTimer(12));
        yield return new WaitWhile(()=>!pickWaiter&&pickTime[12]);
        pickWaiter = true;
        myseq.Kill(false);
        Click[k].DOFade(0, 0.5f);
        ques[k].fontSize = 65;
        if (k == 0)
        {
            ques[0].text = ques[1].text + "+" + ques[2].text;
        }
        else if (k == 1)
        {
            ques[1].text = ques[0].text + "-" + ques[2].text;
        }
        else
        {
            ques[2].text = ques[0].text + "-" + ques[1].text;
        }
        Sequence myse = DOTween.Sequence();
        myse.Append(Click[6].DOFade(1, 1f)).SetEase(Ease.Linear);
        myse.AppendInterval(1f);
        myse.Append(Click[6].DOFade(0, 1f)).SetEase(Ease.Linear);
        myse.SetLoops(5);
        StartCoroutine(pickTimer(13));
        yield return new WaitWhile(() => pickTime[13] && !dragLeft&&!dragRight);
        myse.Kill(false);
        Click[6].DOFade(0, 0.5f);
        if(!pickTime[13])
        autoPick(TimerSlider.result[0], 13);
        else 
        {
            yield return new WaitWhile(() => pickTime[13]&& int.Parse(ques[k].text) != TimerSlider.result[0]);
            if(!pickTime[13])
            autoPick(TimerSlider.result[0], 13);
        }
        TimerSlider.timerStart();
        Sequence mys = DOTween.Sequence();
        mys.Append(Click[7].DOFade(1, 1f)).SetEase(Ease.Linear);
        mys.AppendInterval(1f);
        mys.Append(Click[7].DOFade(0, 1f)).SetEase(Ease.Linear);
        mys.SetLoops(5);
        yield return new WaitWhile(() => !Comparator.pick);
        mys.Kill(false);
        Click[7].DOFade(0, 0.5f);
    }
    IEnumerator tripleClick()
    {
        Comparator.pick = false;
        for(int i = 0; i < TimerSlider.quesNum.Length; i++)
        {
            pickWaiter = false;
            truePick = false;
            int k = TimerSlider.quesNum[i];
            TrueSelect = k;
            select = k;
            Sequence myseq = DOTween.Sequence();
            myseq.Append(Click[k].DOFade(1, 1f)).SetEase(Ease.Linear);
            myseq.AppendInterval(1f);
            myseq.Append(Click[k].DOFade(0, 1f)).SetEase(Ease.Linear);
            myseq.SetLoops(5);
            StartCoroutine(pickTimer(i));
            yield return new WaitWhile(() => /*select != k*/ !pickWaiter && pickTime[i]);
            pickWaiter = true;
            myseq.Kill(false);
            Click[k].DOFade(0, 0.5f);
            ques[k].fontSize = 65;
            if (k == 0)
            {
                ques[0].text = ques[1].text + "+" + ques[2].text;
                recolor(0, 1, 2);

            }
            else if (k == 1)
            {
                if (TimerSlider.quesType == 0)
                {
                    ques[1].text = ques[3].text + "+" + ques[4].text;
                    recolor(3, 1, 4);
                }
                else
                {
                    ques[1].text = ques[0].text + "-" + ques[2].text;
                    recolor(0, 1, 2);
                }
            }
            else if (k == 2)
            {
                if (TimerSlider.quesType == 0)
                {
                    ques[2].text = ques[4].text + "+" + ques[5].text;
                    recolor(4, 5, 2);
                }
                else
                {
                    ques[2].text = ques[0].text + "-" + ques[1].text;
                    recolor(0, 1, 2);
                }
            }
            else if (k == 3)
            {
                ques[3].text = ques[1].text + "-" + ques[4].text;
                recolor(3, 1, 4);
            }
            else if (k == 4)
            {
                if (TimerSlider.quesType == 1)
                {
                    ques[4].text = ques[2].text + "-" + ques[5].text;
                    recolor(4, 5, 2);
                }
                else
                {
                    ques[4].text = ques[1].text + "-" + ques[3].text;
                    recolor(4, 1, 3);
                }
            }
            else
            {
                ques[5].text = ques[2].text + "-" + ques[4].text;
                recolor(5, 4, 2);

            }
            Sequence myse = DOTween.Sequence();
            myse.Append(Click[6].DOFade(1, 1f)).SetEase(Ease.Linear);
            myse.AppendInterval(1f);
            myse.Append(Click[6].DOFade(0, 1f)).SetEase(Ease.Linear);
            myse.SetLoops(5);
            StartCoroutine(pickTimer(i+5));
            yield return new WaitWhile(() => pickTime[i+5] && !dragLeft && !dragRight);
            myse.Kill(false);
            myseq1.Kill(true);
            myseq2.Kill(true);
            myseq3.Kill(true);
            Click[6].DOFade(0, 0.5f);
            if (!pickTime[i+5])
                autoPick(TimerSlider.result[i], i+5);
            else
            {
                yield return new WaitWhile(() => pickTime[i+5] && int.Parse(ques[k].text) != TimerSlider.result[i]);
                if (!pickTime[i+5])
                    autoPick(TimerSlider.result[0], i+5);
            }

        }
        truePick = true;

        TimerSlider.timerStart();
        Sequence mys = DOTween.Sequence();
        mys.Append(Click[7].DOFade(1, 1f)).SetEase(Ease.Linear);
        mys.AppendInterval(1f);
        mys.Append(Click[7].DOFade(0, 1f)).SetEase(Ease.Linear);
        mys.SetLoops(5);
        yield return new WaitWhile(() => !Comparator.pick);
        pickWaiter = true;
        mys.Kill(false);
        Click[7].DOFade(0, 0.5f);
    }
    /*
    IEnumerator Clicker(int k,int result)
    {
        StartCoroutine(pickTimer(k));
        int counter = 0;
        select = -1;
        while(select!=k&&pickTime[k])
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (select==k)
                {
                    Click[k].color = new Color(1, 1, 1, 0);
                    break;
                }
                Click[k].color = new Color(1, 1, 1, i);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (select == k)
                {
                    Click[k].color = new Color(1, 1, 1, 0);
                    break;
                }
                Click[k].color = new Color(1, 1, 1, i);
                yield return null;
            }
         
            counter++;
        }
        if (!pickTime[k])
        {
            select = k;
            pickTime[k] = true;
        }
        ques[k].fontSize = 65;
        
            if (k == 0)
            {
                ques[0].text = ques[1].text + "+" + ques[2].text;
                recolor(0, 1, 2);

            }
            else if (k == 1)
            {
                if (TimerSlider.quesType == 0)
                {
                    ques[1].text = ques[3].text + "+" + ques[4].text;
                    recolor(3, 1, 4);
                }
                else
                {
                    ques[1].text = ques[0].text + "-" + ques[2].text;
                    recolor(0, 1, 2);
                }
            }
            else if (k == 2)
            {
                if (TimerSlider.quesType == 0)
                {
                    ques[2].text = ques[4].text + "+" + ques[5].text;
                    recolor(4, 5, 2);
                }
                else
                {
                    ques[2].text = ques[0].text + "-" + ques[1].text;
                    recolor(0, 1, 2);
                }
            }
            else if (k == 3)
            {
                ques[3].text = ques[1].text + "-" + ques[4].text;
                recolor(3, 1, 4);
            }
            else if (k == 4)
            {
                if (TimerSlider.quesType == 1)
                {
                    ques[4].text = ques[2].text + "-" + ques[5].text;
                    recolor(4, 5, 2);
                }
                else
                {
                    ques[4].text = ques[1].text + "-" + ques[3].text;
                    recolor(4, 1, 3);
                }
            }
            else
            {
                ques[5].text = ques[2].text + "-" + ques[4].text;
                recolor(5, 4, 2);

            }
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ScrollFade(k,result));
        
       
    }*/
    void recolor(int index1,int index2,int index3)
    {
        myseq1 = DOTween.Sequence();
        myseq2 = DOTween.Sequence();
        myseq3 = DOTween.Sequence();

        myseq1.Append( buttons[index1].image.DOColor(Color.green, 1f));
        myseq1.Append(buttons[index1].image.DOColor(new Color(1, 1, 1), 1f));
     

        myseq2.Append(buttons[index2].image.DOColor(Color.green, 1f));
        myseq2.Append(buttons[index2].image.DOColor(new Color(1, 1, 1), 1f));


        myseq3.Append(buttons[index3].image.DOColor(Color.green, 1f));
        myseq3.Append(buttons[index3].image.DOColor(new Color(1, 1, 1), 1f));

        myseq1.SetLoops(5);
        myseq2.SetLoops(5);
        myseq3.SetLoops(5);

    }
    /*
    IEnumerator ScrollFade(int k,int result)
    {
        
        StartCoroutine(pickTimer(5+k));
        while (!(dragRight||dragLeft|| !pickTime[5+k]))
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if(dragRight || dragLeft)
                {
                    Click[6].color = new Color(1, 1, 1, 0);
                    break;
                }
                Click[6].color = new Color(1, 1, 1, i);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (dragRight || dragLeft)
                {
                    Click[6].color = new Color(1, 1, 1, 0);
                    break;
                }
                Click[6].color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        
        yield return new WaitWhile(() =>  pickTime[5+k]&&int.Parse(ques[k].text) != TimerSlider.result[result]);
        if (!pickTime[5+k])
        {
            autoPick(TimerSlider.result[result],5+k);
            yield return new WaitWhile(() => !pickTime[5+k]);
        }
        TruePick = true;
        myseq1.Kill(true);
        myseq2.Kill(true);
        myseq3.Kill(true);
    }
    IEnumerator OnayFade()
    {
        
        TimerSlider.timerStart();
        while (!Comparator.pick)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (Comparator.pick)
                {
                    Click[7].color = new Color(1, 1, 1, 0);
                    break;
                }
                Click[7].color = new Color(1, 1, 1, i);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (Comparator.pick)
                {
                    Click[7].color = new Color(1, 1, 1, 0);
                    break;
                }
                // set color with i as alpha
                Click[7].color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        Comparator.pick = false;
        TruePick = true;
        
    }
    */
    IEnumerator pickTimer(int k)
    {
        float StartTime = Time.time;
        while (Time.time - StartTime < 5)
            yield return null;
        pickTime[k] = false;
    }
    void autoPick(int result,int k)
    {
        pickWaiter = false;
        positionLeft = result/10;
        positionRight = result%10;
        write();
        pickTime[k] = true;
        select = -1;
        
        
    }
   
    public void FadeAway()
    {
        right.GetComponent<Image>().DOFade(0, 0.5f).SetAutoKill();
        left.GetComponent<Image>().DOFade(0, 0.5f).SetAutoKill();
    }
    
}
