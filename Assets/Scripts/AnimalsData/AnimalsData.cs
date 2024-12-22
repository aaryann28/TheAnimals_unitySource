using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Animals", menuName = "All Items / AnimalsData")]
public class AnimalsData : ScriptableObject
{
    public Sprite[] animalAbilityDisplay;
    public Sprite[] animalFoodDisplay;
    public Sprite[] animalGroupDisplay;
    public Sprite[] animalBirthDisplay;
    public Sprite[] animalInsectDisplay;


    public AnimalsInfo[] allAnimalsData;



}

[System.Serializable]
public class AnimalsInfo
{
    public string animalName;

    public GameObject objToSpawn;
    public Sprite animalDisplay;
    public string animalDetail;

    public AbilityType animalAbility;
    public FoodType animalFoodType;
    public GroupType animalGroupType;
    public BirthType animalBirthType;
    public InsectType animalInsectType;

}

//Chatacterstics
public enum AbilityType
{
    CanFly,
    NotFly
}
public enum FoodType
{
    Herbivorous,
    Omnivorous

}
public enum GroupType
{
    Group,
    Solo

}

public enum BirthType
{
    Mammal,
    Reptile
}

public enum InsectType
{
    Insect,
    NonInsect
}