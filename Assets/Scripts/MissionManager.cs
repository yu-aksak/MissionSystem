using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public int currentCoins;
    public int level;
    public int coins;
    public string status;
    private GameManager _gameManager;
    private void Start()
    {
        level = int.Parse(gameObject.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
        string[] str = gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Split('/');
        string[] str1 = str[0].Split(' ');
        currentCoins = int.Parse(str1[1]);
        coins = int.Parse(str[1]);
        
        Sprite _status = gameObject.GetComponentInChildren<Button>().image.sprite;
        if (_status.name.Equals("3"))
            status = "active";
        else if (_status.name.Equals("2"))
            status = "passed";
        else
            status = "unavailable";
        
        gameObject.GetComponentInChildren<Button>().GetComponent<Button>().onClick.AddListener(ButtonClick);
       
        if(gameObject.activeInHierarchy)
            OnInteractable();
    }

    public void OnInteractable()
    {
        GameManager.currentObject = gameObject;
        GameManager._level = level;
        GameManager.currentCoins = currentCoins;
        GameManager.coins = coins;
        GameManager.status = status;
        
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(currentCoins != coins)
            _gameManager.FillMission(true);
        else
            _gameManager.FillMission(false);
    }

    private void ButtonClick()
    {
        OnInteractable();
    }
}
