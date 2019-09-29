using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizator : MonoBehaviour
{
    [SerializeField]
    public string Key;


    // Start is called before the first frame update
    void Awake()
    {
      Text txt = GetComponentInChildren<Text>();
    LocalizationService.Instance.SetFormat(txt);
		txt.text = LocalizationService.Instance.GetString(Key);
    // GetComponentInChildren<Text>().font = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
