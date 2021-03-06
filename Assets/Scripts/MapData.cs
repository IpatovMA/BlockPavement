﻿using System.Collections;
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


    private GameObject MapPrefab;
    public GameObject player;

    [Space]
    public int lvlPart;
    private Camera cam;
    private CameraFollow camFollow;
    private float high = -0.0000f;
    public static int mapNum;

    private Animator DS;
    private Transform rt;
    [Space]
    [SerializeField] private TextAsset DustColorsTxt;
    public Color DustColor;
    [Space]
    public bool customMap = false;
    public int setMapNum;
    public int setRotateOn;
    [Space] [SerializeField] private GameObject baloonsPrefab;
    [SerializeField] private int creatableCellsFactor = 2;
    [SerializeField] private int maxBaloonsNumber = 25;
    [SerializeField] private float baloonRateFactor = 3;
    [SerializeField] private List<Color> baloonColors = new List<Color>();
    private List<ParticleSystem> baloonsList = new List<ParticleSystem>();
    private List<Transform> baloonParentsList = new List<Transform>();

    private void Start()
    {
        //подписка на событие.
        PlayerControl.onSquareCleared += CreateBaloons;

        mapNum = System.Math.Max(1, SaveLoad.savedGame.map % (LevelManager.TotalMapsNumber + 1));
        RotateOn = SaveLoad.savedGame.rotateOn;

        cam = Camera.main;
        camFollow = cam.GetComponent<CameraFollow>();
        lvlPart = 1;
        DS = GetComponentInParent<LevelManager>().DarkScreen;

    }

    private void Update()
    {

        if (LevelManager.State != LevelManager.lvlState.Play)
            return;

        if (DS.GetComponent<DarkScreenControl>().Dark && Map != null && player == null)
        {
            DestroyMap();
        }
        if (Map == null)
        {
            LoadMap();
            player.SetActive(false);
            lvlPart++;
        }

        if (player == null)
            return;
        rt = player.transform.Find("playeralign");

        if (player.activeSelf == false)
        {
            player.SetActive(true);
        }
        if (TotalBlockCount == GetComponentInChildren<PlayerControl>().DoneBlockCount)
        {
            SwipeControl.BlockSwipeInput();
            Destroy(player);
            StartBaloons();
            if ((LevelManager.State != LevelManager.lvlState.Fin) && (lvlPart == 3))
            {
                LevelManager.State = LevelManager.lvlState.Fin;
                lvlPart = 1;

            }
            else
            {

                DS.SetTrigger("Disappear");

            }


        }



    }

    private void CreateBaloons(Transform p)
    {
        if (lvlPart == 3)
        {
            baloonParentsList.Add(p);
            // baloonsList.Add(Instantiate(baloonsPrefab,p).GetComponent<ParticleSystem>());
        }
    }

    public void StartBaloons()
    {
        StartCoroutine(BaloonGenerator(baloonRateFactor));
        // for(int i=0;i<baloonsList.Count;i++)
        // {
        //     if(i%baloonRateFactor==0) baloonsList[i].Play();
        // }
    }

    private IEnumerator BaloonGenerator(float rate)
    {
        int baloonsCounter = 0;
        List<int> indexes = new List<int>();
        for (int i = 0; i < baloonParentsList.Count; i++)
        {
            indexes.Add(i);
        }
        while (indexes.Count > 0)
        {
            int randomIndex = indexes[Random.Range(0, indexes.Count)];
            if (randomIndex > baloonParentsList.Count - 1)
                break;
            if (randomIndex % creatableCellsFactor == 0)
            {
                Color randColor = baloonColors[Random.Range(0, baloonColors.Count)];
                foreach (MeshRenderer mr in
                    Instantiate(baloonsPrefab, baloonParentsList[randomIndex])
                    .GetComponentsInChildren<MeshRenderer>())
                {
                    mr.material.color = randColor;
                };
            }
            indexes.Remove(randomIndex);
            baloonsCounter++;
            if (baloonsCounter >= maxBaloonsNumber)
                yield break;
            yield return new WaitForSeconds(1f / rate);
        }
    }

    public void RecreateBaloons()
    {
        for (int i = 0; i < baloonsList.Count; i++)
        {
            ParticleSystem newPS = Instantiate(baloonsPrefab, baloonsList[i].transform.parent)
                .GetComponent<ParticleSystem>();
            Destroy(baloonsList[i].gameObject);
            baloonsList[i] = newPS;
        }
    }


    public void LoadMap()
    {
        if (customMap)
        {
            RotateOn = setRotateOn;
            mapNum = setMapNum;
        }

        Debug.Log("Loading map " + mapNum);
        MapPrefab = Resources.Load("maps/" + mapNum) as GameObject;

        Map = Instantiate(MapPrefab, Vector3.zero, Quaternion.identity, transform);

        TotalBlockCount = 0;
        var clearBlocks = new List<Transform>();

        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
            if (coll.gameObject.tag == "clear")
            {
                clearBlocks.Add(coll.transform);

            }
        }
        TotalBlockCount = clearBlocks.Count;

        MapWidth = -1;
        MapHeight = -1;

        RaycastHit2D[] WidthRays = Physics2D.RaycastAll(new Vector3(0, 0.5f, high), Vector2.right);
        foreach (var ray in WidthRays)
        {
            if (ray.collider != null && (ray.collider.tag == "border" || ray.collider.tag == "clear"))
            {
                MapWidth++;
                // Debug.DrawLine(new Vector3(0,0.5f,0), ray.point,Color.red,20);
                // Destroy(ray.collider.gameObject);
            }
        }

        RaycastHit2D[] HeightRays = Physics2D.RaycastAll(new Vector3(0.5f, 0, high), Vector2.up);
        foreach (var ray in HeightRays)
        {
            if (ray.collider != null && (ray.collider.tag == "border" || ray.collider.tag == "clear"))
            {
                MapHeight++;
                // Debug.DrawLine(new Vector3(0.5f,0,0), ray.point,Color.red,20);
                // Destroy(ray.collider.gameObject);

            }
        }
        Debug.Log("Load");


        GetComponentInChildren<SetOutlineBorder>().UpdateBorder(this);

        GetComponentInChildren<BushMaker>().UpdateBushes(this);
        GetComponentInParent<LevelManager>().Grass.UpdateGrass();

        camFollow.target = transform;
        transform.eulerAngles = new Vector3(0, 0, 90 * RotateOn);
        Map.transform.localPosition = new Vector3(-MapWidth / 2.0f, -MapHeight / 2.0f, 0);
        Map.transform.localEulerAngles = new Vector3(0, 0, 0);

        // GetComponent<SetInsideBorders>().Set();

        int rotateRnd = Random.Range(0, 4);
        rotateRnd = 0;
        int[] rotateAngs = new int[4] { 0, 90, 180, 270 };
        Vector3 rotateFix;
        switch (rotateRnd)
        {
            case 1:
                rotateFix = new Vector3(1, 0, 0);
                break;
            case 2:
                rotateFix = new Vector3(1, 1, 0);
                break;
            case 3:
                rotateFix = new Vector3(0, 1, 0);
                break;
            default:
                rotateFix = new Vector3(0, 0, 0);
                break;
        }
        PlayerPos = clearBlocks[Random.Range(0, TotalBlockCount)].position - new Vector3(0.5f, 0.5f, 0) + rotateFix;



        player = Instantiate(PlayerPrafab, PlayerPos, Quaternion.Euler(0, 0, rotateAngs[rotateRnd]), transform);
        // player.GetComponent<PlayerControl>().startPos = PlayerPos
        player.SetActive(false);

        DS.SetTrigger("Appear");
        SetDustColor();

    }

    public void DestroyMap()
    {

        if (baloonParentsList.Count > 0)
            baloonParentsList = new List<Transform>();

        Destroy(Map, 0);
        Debug.Log("Dest");

        Resources.UnloadUnusedAssets();
        if (mapNum == LevelManager.TotalMapsNumber)
        {
            mapNum = 0;
            RotateOn++;

            if (RotateOn == 4)
            {
                SceneManager.LoadScene("game");

            }
        }
        if (LevelManager.TotalMapsNumber <= mapNum)
        {
            mapNum = 1;
        }
        else
        { mapNum++; }
    }

    private void SetDustColor()
    {

        int count = DustColorsTxt.text.Split('#').Length;
        int ColorNum = UnityEngine.Random.Range(1, count);
        string ColorStr = "#" + DustColorsTxt.text.Split('#')[ColorNum].Substring(0, 6);
        bool i = ColorUtility.TryParseHtmlString(ColorStr, out DustColor);
        // char[] sep = new char[]{'/','n'};
        //  string ColorStr = DustColorsTxt.text.Split(sep);


        //  Debug.Log (i+"___" +ColorStr+"__" +ColorNum);
    }

}
