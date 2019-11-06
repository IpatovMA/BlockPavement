using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizator : MonoBehaviour
{
    [SerializeField]
    public string Key;


    void Start()
    {
      Text txt = GetComponentInChildren<Text>();
    // LocalizationService.Instance.SetFormat(txt);
		txt.text = LocalizationService.Instance.GetString(Key);
    }

}
