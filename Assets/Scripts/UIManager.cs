using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text _animalAssignedTxt;
    public TMP_Text _animalLeftTxt;

    //ButtonRef
    public Button _correctAnswerDispBtn;
    public Button _incorrectAnswerDispBtn;

    public Color _selectedColor;
    public Color _defaultColor;
    public Color _groupedColor;


    //PannelRef
    public GameObject _animalMoreInfoPannel;
    public GameObject _gameOverPannel;


    //Result Display
    public Transform _correctAnswersSpotHolder;
    public Transform _incorrectAnswersSpotHolder; 
    public GameObject _AnswersSpotDisplay; //the spot on which the cards will be visible

    private void Awake()
    {
        _correctAnswerDispBtn.onClick.AddListener(OnShowCorrectAnswersBtnClick);
        _incorrectAnswerDispBtn.onClick.AddListener(OnShowinCorrectAnswersBtnClick);
    }

    private void Start()
    {
        GameManager.instance.onGameOver += GameOver;
        GameManager.instance.onGameOver += OnShowinCorrectAnswersBtnClick;
        GameManager.instance.onGameOver += OnShowCorrectAnswersBtnClick;

        //Setting up on display
        _animalAssignedTxt.text = "Animals Assigned: " + (AnimalsManager.instance.totalAnimal - AnimalsManager.instance.animalsLeft).ToString();
        _animalLeftTxt.text = "Animals Left: " + (AnimalsManager.instance.animalsLeft).ToString();
    }

    public void AnimalAssigned()
    {
        AnimalsManager.instance.animalsLeft--;
        _animalAssignedTxt.text = "Animals Assigned: "+ (AnimalsManager.instance.totalAnimal- AnimalsManager.instance.animalsLeft).ToString();
        _animalLeftTxt.text = "Animals Left: "+ (AnimalsManager.instance.animalsLeft).ToString();

        if(AnimalsManager.instance.animalsLeft <= 0)
        {
            GameManager.instance.onGameOver?.Invoke();
        }

    }

    public void ShowMoreInfo(string _animalName,string _detailInfo)
    {

        _animalMoreInfoPannel.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = _animalName;
        _animalMoreInfoPannel.transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().text = _detailInfo;
        _animalMoreInfoPannel.SetActive(true);
    }
    void GameOver()
    {
        _gameOverPannel.SetActive(true);
        _gameOverPannel.transform.GetChild(1).GetComponent<TMP_Text>().text = "Score: " + (GameManager.instance._yourScore).ToString() + "/"+ (AnimalsManager.instance.totalAnimal).ToString();
    }


    //Buttons
    public void OnShowCorrectAnswersBtnClick()
    {
        
        _correctAnswerDispBtn.GetComponent<Image>().color = _selectedColor;
        _incorrectAnswerDispBtn.GetComponent<Image>().color = _defaultColor;

        foreach (Transform answetrs in _correctAnswersSpotHolder)
        {
           Destroy(answetrs.gameObject);
        }

        foreach (GameObject answers in GameManager.instance._correctAnswers)
        {
            GameObject spawnedCard = Instantiate(_AnswersSpotDisplay, _correctAnswersSpotHolder);
            spawnedCard.GetComponent<Image>().sprite = answers.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void OnShowinCorrectAnswersBtnClick()
    {
        _correctAnswerDispBtn.GetComponent<Image>().color = _defaultColor;
        _incorrectAnswerDispBtn.GetComponent<Image>().color = _selectedColor;

        foreach (Transform answetrs in _incorrectAnswersSpotHolder)
        {
            Destroy(answetrs.gameObject);
        }

        foreach (GameObject answers in GameManager.instance._wrongAnswers)
        {
            GameObject spawnedCard = Instantiate(_AnswersSpotDisplay, _incorrectAnswersSpotHolder);
            spawnedCard.GetComponent<Image>().sprite = answers.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void OnSceneChangebtnClick(int Sceneid)
    {
        SceneManager.LoadScene(Sceneid);
    }



}
