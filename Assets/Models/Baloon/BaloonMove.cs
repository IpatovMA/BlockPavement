using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonMove : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float lifetime = 5;

    // public void Settings(Vector3)

    void Start()
    {
        StartCoroutine(DelayedAction(lifetime, ()=>
        {
            Destroy(gameObject);
        }));
    }

    void Update()
    {
        transform.Translate(-Vector3.forward*speed*Time.deltaTime,Space.World);
    }

    IEnumerator DelayedAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
