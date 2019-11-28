using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public static int RotateOn;
    public int TotalBlockCount;
    public int MapWidth;
    public int MapHeight;
    public GameObject Map;
    public Vector3 PlayerPos;
    public GameObject PlayerPrafab;

    public int lvlPart;

    private GameObject MapPrefab;
    public GameObject player;
    private Camera cam;
    private CameraFollow camFollow;
    float high = -0.0000f;
    public static int mapNum;
    public bool customMap= false;
    public int setMapNum;
    public int setRotateOn;
    private Animator DS;
    private Transform rt;
    [SerializeField] TextAsset DustColorsTxt;
    public Color DustColor;

    void Start()
    {   
        if( SaveLoad.savedGame.map!=0)
           { mapNum = SaveLoad.savedGame.map;
            RotateOn = SaveLoad.savedGame.rotateOn;
           }
        else {mapNum = 1;
                RotateOn=0;}
        cam = Camera.main;
        camFollow = cam.GetComponent<CameraFollow>();
        lvlPart = 1;
        DS = GetComponentInParent<LevelManager>().DarkScreen;
        
    }

void Update()
    {

        if (LevelManager.State != LevelManager.lvlState.Play) return;

                if(DS.GetComponent<DarkScreenControl>().Dark&&Map!=null&&player==null){
                DestroyMap();
                }
                if(Map==null){
                LoadMap();
                player.SetActive(false);
                lvlPart++;
                }

        if(player==null) return;
                      rt = player.transform.Find("playeralign");

        if(player.activeSelf==false){
            player.SetActive(true);
        }
        if(TotalBlockCount==GetComponentInChildren<PlayerControl>().DoneBlockCount){
                    SwipeControl.BlockSwipeInput();
            Destroy(player);
             if((LevelManager.State!= LevelManager.lvlState.Fin)&&(lvlPart==3)) {
                LevelManager.State= LevelManager.lvlState.Fin;
                lvlPart=1;
            
            } else{

            DS.SetTrigger("Disappear");

            }


        } 



    }

    
    public void LoadMap(){
        if (customMap){
            RotateOn = setRotateOn;
             mapNum = setMapNum;}
        MapPrefab = Resources.Load("maps/"+mapNum) as GameObject;

        Map = Instantiate(MapPrefab, Vector3.zero,Quaternion.identity,transform);
 
       TotalBlockCount=0;
        var clearBlocks = new List<Transform>();

        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
           if(coll.gameObject.tag=="clear"){
               clearBlocks.Add(coll.transform);

           } 
        }
        TotalBlockCount = clearBlocks.Count;

        MapWidth=-1;
        MapHeight=-1;

        RaycastHit2D[] WidthRays = Physics2D.RaycastAll(new Vector3(0,0.5f,high), Vector2.right);
        foreach (var ray in WidthRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapWidth++;
                // Debug.DrawLine(new Vector3(0,0.5f,0), ray.point,Color.red,20);
                // Destroy(ray.collider.gameObject);
            }    
        }
           
        RaycastHit2D[] HeightRays = Physics2D.RaycastAll(new Vector3(0.5f,0,high), Vector2.up);
        foreach (var ray in HeightRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapHeight++;
                // Debug.DrawLine(new Vector3(0.5f,0,0), ray.point,Color.red,20);
                            // Destroy(ray.collider.gameObject);

            }    
        }
                        Debug.Log("Load");


        GetComponentInChildren<SetOutlineBorder>().UpdateBorder(this);
        camFollow.target = transform;
        transform.eulerAngles = new Vector3(0,0,90*RotateOn);
        Map.transform.localPosition = new Vector3(-MapWidth/2.0f,-MapHeight/2.0f,0);
        Map.transform.localEulerAngles = new Vector3(0,0,0);

        int rotateRnd = Random.Range(0,4);
rotateRnd =0;
        int[] rotateAngs = new int[4] {0,90,180,270};
        Vector3 rotateFix;
        switch (rotateRnd){
            case 1: rotateFix = new Vector3(1,0,0);
            break;
            case 2: rotateFix = new Vector3(1,1,0);
            break;
            case 3: rotateFix = new Vector3(0,1,0);
            break;
            default: rotateFix = new Vector3(0,0,0);
            break;
        }
        PlayerPos = clearBlocks[Random.Range(0, TotalBlockCount)].position  - new Vector3(0.5f,0.5f,0) +rotateFix;
        
            

        player = Instantiate(PlayerPrafab,PlayerPos,Quaternion.Euler(0,0,rotateAngs[rotateRnd]),transform);
        // player.GetComponent<PlayerControl>().startPos = PlayerPos
        player.SetActive(false);

        DS.SetTrigger("Appear");
        SetDustColor();

    }

    public void DestroyMap(){

        Destroy(Map,0);
                    Debug.Log("Dest");

        Resources.UnloadUnusedAssets();
        if(mapNum==LevelManager.TotalMapsNumber) {
                mapNum=0;
                RotateOn++;
                
                if(RotateOn==4){
                    SceneManager.LoadScene("game");

                }
            }
             if(LevelManager.TotalMapsNumber<= mapNum ){
                        mapNum = 1;
                } else{ mapNum++;}
    }

    void SetDustColor() {

        int count = DustColorsTxt.text.Split('#').Length;
        int ColorNum = UnityEngine.Random.Range(1,count);
        string ColorStr = "#"+DustColorsTxt.text.Split('#')[ColorNum].Substring(0,6);
        bool i = ColorUtility.TryParseHtmlString(ColorStr, out DustColor);
        // char[] sep = new char[]{'/','n'};
        //  string ColorStr = DustColorsTxt.text.Split(sep);


        //  Debug.Log (i+"___" +ColorStr+"__" +ColorNum);
    }

}
