using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cam;
    void Update()
    {
        if (Input.GetKey(GameManager.GM.Forward))
        {
            transform.position += Vector3.up / 60;
        }
        if (Input.GetKey(GameManager.GM.Backward))
        {
            transform.position += Vector3.down / 60;
        }
        if (Input.GetKey(GameManager.GM.Right))
        {
            transform.position += Vector3.right / 60;
        }
        if (Input.GetKey(GameManager.GM.Left))
        {
            transform.position += Vector3.left / 60;
        }
        var target = transform.position;
        target.z = -10;
        cam.transform.position = target;
    }
}