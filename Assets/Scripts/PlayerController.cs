using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public float speed = 3.0f;
    void Update()
    {
        if (Input.GetKey(GameManager.GM.Forward))
        {
            transform.position += Vector3.up * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.GM.Backward))
        {
            transform.position += Vector3.down * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.GM.Right))
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.GM.Left))
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}