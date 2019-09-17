using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizator : MonoBehaviour
{
    [SerializeField]
    public string Key;


    // Start is called before the first frame update
    void Start()
    {
		GetComponentInChildren<Text>().text = LocalizationService.Instance.GetString(Key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
