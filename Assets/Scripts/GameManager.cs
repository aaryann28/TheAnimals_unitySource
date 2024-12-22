using System;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]AnimalsManager _animalManager;
    public UIManager _uiManager;


    public GameMode _gameMode;
    // Object References
    public Transform _group01HolderRef;
    public Transform _group02HolderRef;

    public Action onGameOver;
    public Action onAnimalDetail;

    public int _yourScore;
    public List<GameObject> _correctAnswers;
    public List<GameObject> _wrongAnswers;


    private void Awake()
    {
        if (instance == null)
            instance = this;


        // Selecting a random game mode
        _gameMode = (GameMode)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(GameMode)).Length);

        Debug.Log("Assigned Game Mode: " + _gameMode);
        onGameOver += playSoundGameOVer;
    }
    


    private void Start()
    {
        // Configure the game based on the selected game mode
        ConfigureGame();
    }

    public enum GameMode
    {
        Ability,
        Food,
        Group,
        Birth,
        Insect
    }

    void ConfigureGame()
    {
        if (_animalManager == null || _animalManager._animalsData == null)
        {
            return;
        }

        switch (_gameMode)
        {
            case GameMode.Ability:
                SetGroupText<AbilityType>();
                SetUpDisplayIcon(_animalManager._animalsData.animalAbilityDisplay);
                break;

            case GameMode.Food:
                SetGroupText<FoodType>();
                SetUpDisplayIcon(_animalManager._animalsData.animalFoodDisplay);
                break;

            case GameMode.Group:
                SetGroupText<GroupType>();
                SetUpDisplayIcon(_animalManager._animalsData.animalGroupDisplay);
                break;

            case GameMode.Birth:
                SetGroupText<BirthType>();
                SetUpDisplayIcon(_animalManager._animalsData.animalBirthDisplay);
                break;
                
            case GameMode.Insect:
                SetGroupText<InsectType>();
                SetUpDisplayIcon(_animalManager._animalsData.animalInsectDisplay);
                break;
        }

    }

    void SetGroupText<GameType>() where GameType : System.Enum
    {
        GameType[] values = (GameType[])System.Enum.GetValues(typeof(GameType));
        if (_group01HolderRef && _group02HolderRef)
        {
            _group01HolderRef.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = values[0].ToString();
            _group02HolderRef.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = values[1].ToString();

            _group01HolderRef.GetComponent<Container>().containerName= values[0].ToString();
            _group02HolderRef.GetComponent<Container>().containerName= values[1].ToString();
        }
    
    }

    void SetUpDisplayIcon(Sprite[] displayIcons)
    {
        if (displayIcons == null || displayIcons.Length < 2)
        {
            return;
        }

        if (_group01HolderRef && _group01HolderRef.GetComponent<SpriteRenderer>())
            _group01HolderRef.GetComponent<SpriteRenderer>().sprite = displayIcons[0];

        if (_group02HolderRef && _group02HolderRef.GetComponent<SpriteRenderer>())
            _group02HolderRef.GetComponent<SpriteRenderer>().sprite = displayIcons[1];
    }


   

    public void playSoundGameOVer()
    {
        SoundManager.instance.playSound("GameOver");
    }

}
