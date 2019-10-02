using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customisator : MonoBehaviour
{
    public string Key;
    public GameObject Model;
    private CustomizationService Srv;
    void Start (){
        Srv =  GetComponentInParent<CustomizationService>();
        SetModel();
        // Debug.Log(Model);

    }
    void SetModel(){
        // Debug.Log(Srv.SetModel("Car"));
        Model = Instantiate(Srv.SetModel(Key), transform.position,Quaternion.identity,transform);
    }
}
