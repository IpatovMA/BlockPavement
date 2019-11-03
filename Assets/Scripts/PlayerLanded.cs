using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanded : MonoBehaviour
{

    public void Laning(){
        GetComponentInParent<BoxCollider2D>().enabled = true;
    }
}
