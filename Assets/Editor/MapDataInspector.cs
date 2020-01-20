using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapData))]
public class MapDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        MapData t = target as MapData;
        GUILayout.Label("Use this buttons only runtime!");
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Start Baloons")) t.StartBaloons();
        if(GUILayout.Button("Recreate Baloons")) t.RecreateBaloons();
        GUILayout.EndHorizontal();
        DrawDefaultInspector();
    }
}
