using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{

    [SerializeField]MapData Data;
    [SerializeField] Image[] Fill;
    [SerializeField] GameObject CurrentLvl;
    [SerializeField] GameObject NextLvl;
    private int currLVL;

    void Start()
    {
        
    }
    void Update()
    {
    if(currLVL!=LevelManager.lvlNum){
        currLVL = LevelManager.lvlNum;
        ResetProgress();
        CurrentLvl.GetComponentInChildren<Text>().text = currLVL.ToString();
        NextLvl.GetComponentInChildren<Text>().text = (currLVL+1).ToString();

    }
       SetProgress(Fill[Data.lvlPart-1]);

    }

    void SetProgress(Image bar ){
        if( Data.GetComponentInChildren<PlayerControl>()==null) return;
        int done = Data.GetComponentInChildren<PlayerControl>().DoneBlockCount;
        int total = Data.TotalBlockCount;
        if (done==total){
        bar.fillAmount = 1;
        return;
        }

        float fill = bar.fillAmount;
        float newFill =Mathf.Lerp(fill,(float)done/total,Time.fixedDeltaTime*10);
        bar.fillAmount = newFill;

        // Debug.Log((float)done/total+"  " +done+" ");
    }

    void ResetProgress(){
        foreach (var fill in Fill)
        {
            fill.fillAmount = 0;
        }
    }

}
