using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button enterButton;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Sprite passed;
    [SerializeField] private Sprite active;

    public static GameObject currentObject;
    
    public static int coins;
    public static int currentCoins = 0;

    public static int _level;
    public static string status;
    
    // Start is called before the first frame update
    void Start()
    {
        plusButton.GetComponent<Button>().onClick.AddListener(PlusButton);
        minusButton.GetComponent<Button>().onClick.AddListener(MinusButton);
        enterButton.GetComponent<Button>().onClick.AddListener(EnterButton);
        reloadButton.GetComponent<Button>().onClick.AddListener(ReloadButton);
    }

    private void PlusButton()
    {
        if (currentCoins < coins)
        {
            currentCoins++;
            currentObject.GetComponent<MissionManager>().currentCoins = currentCoins;
            coinsText.text = " " + currentCoins + "/" + coins;
            currentObject.GetComponentInChildren<TextMeshProUGUI>().text = "x " + currentCoins + "/" + coins;
        }

        if (currentCoins > 0)
        {
            minusButton.interactable = true;
            reloadButton.interactable = true;
        }

        if (currentCoins == coins)
        {
            enterButton.interactable = true;
            plusButton.interactable = false;
        }
    }

    private void MinusButton()
    {
        if (currentCoins > 0)
        {
            currentCoins--;
            currentObject.GetComponent<MissionManager>().currentCoins = currentCoins;
            coinsText.text = "" + currentCoins + "/" + coins;
            currentObject.GetComponentInChildren<TextMeshProUGUI>().text = "x " + currentCoins + "/" + coins;
        }

        if (currentCoins == 0)
        {
            minusButton.interactable = false;
            reloadButton.interactable = false;
        }

        if (currentCoins != coins)
        {
            enterButton.interactable = false;
            plusButton.interactable = true;
        }
    }

    private void EnterButton()
    {
        currentObject.GetComponentInChildren<Button>().image.sprite = passed;
        _level++;
        status = "passed";
        currentObject.GetComponent<MissionManager>().status = status;
        NextMission();
    }

    private void NextMission()
    {
        GameObject _gameObject = GameObject.Find("Mission (" + _level + ")");
        _gameObject.GetComponentInChildren<Button>().interactable = true;
       
        MissionManager missionManager = _gameObject.GetComponent<MissionManager>();
        missionManager.OnInteractable();
        //Debug.Log(_level);
        FillMission(true);
        if (status.Equals("unavailable"))
            _gameObject.GetComponentInChildren<Button>().image.sprite = active;
    }

    public void FillMission(bool b)
    {
        levelText.text = "Level: " + _level;
        coinsText.text = "" + currentCoins + "/" + coins;
        minusButton.interactable = false;
        enterButton.interactable = false;
        plusButton.interactable = b;
        reloadButton.interactable = !b;
    }
    
    private void ReloadButton()
    {
        coinsText.text = "0/" + coins;
        currentCoins = 0;
        minusButton.interactable = false;
        reloadButton.interactable = false;
        enterButton.interactable = false;
        plusButton.interactable = true;
        status = "active";
        currentObject.GetComponent<MissionManager>().status = status;
        currentObject.GetComponentInChildren<Button>().image.sprite = active;
    }
}
