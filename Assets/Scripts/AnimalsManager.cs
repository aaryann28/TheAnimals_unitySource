using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalsManager : MonoBehaviour
{
    public static AnimalsManager instance;

    public UIManager _uiManager;

    public AnimalsData _animalsData;
    public GameObject _currentSelectedAnimal;


    //SpawnPropertied
    public float _xSpacing;
    public float _ySpacing;
    public int columns = 5; // Number of columns in the grid
    [SerializeField] Transform animalsHolder;

    public int totalAnimal;
    public int animalsLeft;

    //SortPositions
    [SerializeField] Transform _flyingContainer;
    [SerializeField] Transform _nonFlyingContainer;

    public List<GameObject> _selectedAnimalsGroup01 = new List<GameObject>();
    public List<GameObject> _selectedAnimalsGroup02 = new List<GameObject>();



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        CountTotalAnimals();
        animalsLeft = totalAnimal;


    }


    private void Start()
    {
        SpawnAnimals(totalAnimal);
    }

    void CountTotalAnimals()
    {
        foreach(var item in _animalsData.allAnimalsData)
        {
            totalAnimal++;
        }


    }
    void SpawnAnimals(int totalAnimalsCount)
    {
        int totalAnimals = totalAnimalsCount;
        int rows = Mathf.CeilToInt((float)totalAnimals / columns); 

        float gridWidth = (columns - 1) * _xSpacing;
        float gridHeight = (rows - 1) * _ySpacing;

        Vector3 centerOffset = new Vector3(gridWidth / 2f, gridHeight / 2f, 0);

        int row = 0;
        int column = 0;

        foreach (var item in _animalsData.allAnimalsData)
        {
            //Debug.Log("Animal Name: " + item.animalName);

            // Calculate position based on row and column, offset by centerOffset
            Vector3 position = new Vector3(animalsHolder.position.x + (column * _xSpacing) - centerOffset.x, animalsHolder.position.y + (row * _ySpacing) - centerOffset.y,animalsHolder.position.z
            );

            GameObject animalSpawned = Instantiate(item.objToSpawn, position, Quaternion.Euler( animalsHolder.rotation.x, animalsHolder.rotation.y, animalsHolder.rotation.z +(Random.Range(-15f,15f))));
            animalSpawned.transform.parent = animalsHolder;
            animalSpawned.name = item.animalName + " item";
            animalSpawned.GetComponent<AnimalInteraction>()._animalName = item.animalName;
            animalSpawned.GetComponent<AnimalInteraction>()._animalDetails = item.animalDetail;

            animalSpawned.GetComponent<SpriteRenderer>().sprite = item.animalDisplay;
            animalSpawned.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.animalDisplay;

            column++;
            if (column >= columns)
            {
                column = 0;
                row++;
            }
        }
    }


    //public void AnimalDistanceChecker()
    //{
    //    if(Vector2.Distance( _currentSelectedAnimal.transform.position , _flyingContainer.position) < 1f)
    //    {
    //        Debug.Log("You Put the animan in Flying Container");
    //    }
    //    if (Vector2.Distance(_currentSelectedAnimal.transform.position, _nonFlyingContainer.position) < 1f)
    //    {
    //        Debug.Log("You Put the animan in Non Flying Container");
    //    }
    //}

}
