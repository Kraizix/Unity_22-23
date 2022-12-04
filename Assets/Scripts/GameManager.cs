using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public KeyCode Forward { get; set;}
    public KeyCode Backward { get; set;}
    public KeyCode Left { get; set;}
    public KeyCode Right { get; set;}
    // public EnemyController[] mobs;
    public GameObject ExpOrb;
    public bool isEnded;
    public bool win;

    void Awake()
    {
        if (gm == null)
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
        }
        else if(gm != this)
        {
            Destroy(gameObject);
        }
        ExpOrb = Resources.Load("Prefabs/Exp") as GameObject;

        // mobs = FindObjectsOfType(typeof(EnemyController)) as EnemyController[];
        Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "Z"));
        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }
}
