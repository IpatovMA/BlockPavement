using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="CustomUnit", menuName="Scriptable Objects/CustomUnit")]
public class CustomUnit : ScriptableObject
{
    public string Key;
    public GameObject[] Models;
}
