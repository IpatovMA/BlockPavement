using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] List<string> phrases = new List<string>();
    void OnEnable()
    {
        textMeshPro.text = phrases[Random.Range(0,phrases.Count)];
    }
}
