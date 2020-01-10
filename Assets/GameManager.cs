using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {


    public static GameManager Instance { get; private set; }
    public static int UncollectedGems = 0;
    public Canvas menu;
    public TMP_Text text;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RegisterGem()
    {
        UncollectedGems++;
        UpdateCount(UncollectedGems);
    }
    public void CollectGem()
    {
        UncollectedGems--;
        UpdateCount(UncollectedGems);
        if (UncollectedGems == 0)
            LevelComplete();
        else
            AudioManager.Instance.Play("GemCollected");
    }
    public void LevelComplete()
    {

        AudioManager.Instance.Play("LevelComplete");
        menu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void LevelStarted()
    {
        Time.timeScale = 1;
    }

    public void UpdateCount(int count)
    {
        text.text = count.ToString();
    } 
}
