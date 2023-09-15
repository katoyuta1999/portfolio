using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class MapSelect : MonoBehaviour
{
    public int myPosErea;
    public int myPosStage;
    public static int selectLoad;
    public int clickId;
    public static int beforeId = 1;
    public static int beforeStage = 1;
    public static int yourmap = 1;
    public static int randomseed = 353;//(100~で作成すること)*******
    public Sprite battlesprite;
    public Sprite eventsprite;
    public Sprite shopsprite;
    public Sprite bosssprite;
    public Image spriteRenderer;
    public int roomseed;
    void Start()
    {
        myPosErea = GetMyErea();
        myPosStage = GetMyStage();
        
        LoadShow();//道の表示
        UsetoButton();//ボタンの表示
        //部屋の内容を決定（x-1は戦闘マス固定。x-6はボスで固定）
        if(myPosStage>1&&myPosStage<6)
        GetRoomSprite();



    }

    public void Select()
    {
        StageRun();//ステージ進行。
        GetClickErea();//クリックした場所をbeforeIdへ入れる。
        //部屋の内容を読み込んで遷移する。

        Random rnd = new Random(randomseed*myPosStage+(yourmap*10)*myPosErea);
        roomseed = rnd.Next(1,101);
        if(myPosStage==1)
        {
             roomseed = 10;
        }
        if(myPosStage==6)
        {
            randomseed = 150;
        }

        if (roomseed<46)
        {
            //battle
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);

            
        }

        if((roomseed>45)&&(roomseed<91))
        {
            //event.
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);


        }

        if((roomseed>90)&&(roomseed<101))
        {
            //shop
            SceneManager.LoadScene("ShopScene", LoadSceneMode.Single);


        }
        if(roomseed>100)
        {
            //boss
            
        }



    }

    int GetMyErea()
    {
        int myId;
        string myName =this.GetComponent<MapSelect>().ToString();

        if (myName.Contains("A"))
        {
            myId=0;
            
        }else if(myName.Contains("B"))
        {
            myId=1;
            
        }else
        {
            myId=2;
            
        }
        return myId;
    }
    int GetMyStage()
    {
        int myStage;
        string myName =this.GetComponent<MapSelect>().ToString();

        if (myName.Contains("1"))
        {
            myStage=1;
        }else if (myName.Contains("2"))
        {
            myStage=2;
            
        }else if(myName.Contains("3"))
        {
            myStage=3;
            
        }else if(myName.Contains("4"))
        {
            myStage=4;
            
        }else if(myName.Contains("5"))
        {
            myStage=5;
            
        }else 
        {
            myStage=6;
            
        }    
        return myStage;
        
    }
    public void LoadShow()
    {
            //道の選択肢

        if(beforeId==0)
        {
            if((beforeStage==myPosStage)&&(beforeId==myPosErea))
            {
                List<Image> loads = this.GetComponentsInChildren<Image>().ToList();
                for (int i = 0; i <loads.Count; i++)
                {
                    loads[i].enabled = true;

                }
            }

        }else if (beforeId==1)
        {
            if((beforeStage==myPosStage)&&(beforeId==myPosErea))
            {
                List<Image> loads = this.GetComponentsInChildren<Image>().ToList();
                for (int i = 0; i <loads.Count; i++)
                {
                    loads[i].enabled = true;

                }
            }
        }else
        {
            if((beforeStage==myPosStage)&&(beforeId==myPosErea))
            {
                List<Image> loads = this.GetComponentsInChildren<Image>().ToList();
                for (int i = 0; i <loads.Count; i++)
                {
                    loads[i].enabled = true;

                }
            }

        }
    }


    public void UsetoButton()
    {
        //クリック出来るボタンを指定
        if(beforeId==1)
        {   
            List<int> nextEreas;
            if (beforeStage == 5)
            {
                nextEreas = new List<int>(){1};   
            }else
            {
                nextEreas = new List<int>(){0,1,2};
            }
            int nextStage = beforeStage+1;
            for (int i = 0; i <nextEreas.Count; i++)
            {
                if((myPosStage==nextStage)&&(myPosErea==nextEreas[i]))
                {
                    var thisErea = this.GetComponent<Button>();
                    thisErea.enabled = true;
                }
            }
        }
        if(beforeId==0)
        {
            List<int> nextEreas;
            if (beforeStage == 5)
            {
                nextEreas = new List<int>(){1};   
            }else
            {
                nextEreas = new List<int>(){0,1};
            }
            int nextStage = beforeStage+1;
            for (int i = 0; i <nextEreas.Count; i++)
            {
                if((myPosStage==nextStage)&&(myPosErea==nextEreas[i]))
                {
                    var thisErea = this.GetComponent<Button>();
                    thisErea.enabled = true;
                }
            }
        }
        if(beforeId==2)
        {
          List<int> nextEreas;
            if (beforeStage == 5)
            {
                nextEreas = new List<int>(){1};   
            }else
            {
                nextEreas = new List<int>(){1,2};
            }
            int nextStage = beforeStage+1;
            for (int i = 0; i <nextEreas.Count; i++)
            {
                if((myPosStage==nextStage)&&(myPosErea==nextEreas[i]))
                {
                    var thisErea = this.GetComponent<Button>();
                    thisErea.enabled = true;
                }
            }
        }
    }
    
    public void StageRun()
    {
                //ステージ進行。
            beforeStage++;
    }

    public void GetClickErea()
    {
                    //現在地の取得(たて)
        clickId = myPosErea ;
        if ((clickId-beforeId==-1))
        {
            selectLoad = 0;
            beforeId -=1;

        }else if (clickId==beforeId)
        {
            selectLoad = 1;
        }else if ((clickId-beforeId==1))
        {
            selectLoad = 2;
            beforeId +=1;
        }else
        {
            return;
        }
    }
    public void GetRoomSprite()
    {
        Random rnd = new Random(randomseed*myPosStage+(yourmap*10)*myPosErea);
        roomseed = rnd.Next(1,101);
        spriteRenderer = this.GetComponent<Image>();
        if (roomseed<46)
        {
            //battle
            spriteRenderer.sprite = battlesprite;
            
        }else if((roomseed>45)&&(roomseed<91))
        {
            //event
            spriteRenderer.sprite = eventsprite;

        }else if((roomseed>90)&&(roomseed<101))
        {
            //shop
            spriteRenderer.sprite = shopsprite;

        }
    }
}


