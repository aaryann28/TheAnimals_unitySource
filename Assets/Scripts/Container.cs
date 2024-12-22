using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Container : MonoBehaviour
{
    public string containerName;

    public void CheckAnimalGroup(string animalName, GameMode _gameMode,GameObject gameObj)
    {
        AnimalsInfo animalInfo = FindAnimalInfo(animalName);
        if (animalInfo == null)
        {
            Debug.Log("Animal daata not found");
            return;
        }

        bool isCorrect = false;

        switch (_gameMode)
        {
            case GameMode.Ability:
                isCorrect = containerName == animalInfo.animalAbility.ToString();
                break;

            case GameMode.Food:
                isCorrect = containerName == animalInfo.animalFoodType.ToString();
                break;

            case GameMode.Group:
                isCorrect = containerName == animalInfo.animalGroupType.ToString();
                break;

            case GameMode.Birth:
                isCorrect = containerName == animalInfo.animalBirthType.ToString();
                break;

            case GameMode.Insect:
                isCorrect = containerName == animalInfo.animalInsectType.ToString();
                break;
        }

        if (isCorrect)
        {
            Debug.Log("Correct Answer!!!");
            GameManager.instance._correctAnswers.Add(gameObj);
            SoundManager.instance.playSound("CorrectGroup");
            GameManager.instance._yourScore++;
        }
        else
        {
            GameManager.instance._wrongAnswers.Add(gameObj);
            Debug.Log("Wrong Answer!!!");
            SoundManager.instance.playSound("IncorrectGroup");

        }
    }

    private AnimalsInfo FindAnimalInfo(string animalName)
    {
        foreach (var item in AnimalsManager.instance._animalsData.allAnimalsData)
        {
            if (item.animalName == animalName)
            {
                return item;
            }
        }
        return null;
    }
}
