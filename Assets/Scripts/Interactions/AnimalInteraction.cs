using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AnimalInteraction : MonoBehaviour
{

    public string _animalName;
    [HideInInspector]public string _animalDetails;

    // References
    SpriteRenderer _spRenderer;

    [SerializeField] float _resetSpeed;
    [SerializeField] float _holdThreshold = 0.15f; // drag Threshold

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    Vector3 _offset;

    // State
    bool _isHolding;
    bool _containerAssigned;
    bool _isDragging;
    float _holdTime;

    private void Start()
    {
        _defaultPosition = transform.position;
        _defaultRotation = transform.rotation;

        _spRenderer = GetComponent<SpriteRenderer>();
        _spRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (IsPointerOverUIElement())
            return;

        if (_containerAssigned)
            return;

        if (AnimalsManager.instance._currentSelectedAnimal != null)
            return;

        _spRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        if (AnimalsManager.instance._currentSelectedAnimal != null)
            return;

        if (!_containerAssigned)
        {
            _spRenderer.enabled = false;

        }
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUIElement())
            return;


        if (_containerAssigned)
            return;

        _isHolding = true;
        _isDragging = false;
        _holdTime = 0;

        AnimalsManager.instance._currentSelectedAnimal = this.gameObject;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        _offset = transform.position - worldPosition;
    }

    private void Update()
    {
        if (_isHolding && !_containerAssigned)
        {
            _holdTime += Time.deltaTime;

            if (_holdTime >= _holdThreshold)
            {
                _isDragging = true;
            }

            if (_isDragging)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0f;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + _offset;

                transform.position = Vector3.Lerp(transform.position, new Vector3(worldPosition.x, worldPosition.y, transform.position.z),_resetSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnMouseUp()
    {

        _isHolding = false;

        if (!_isDragging)
        {
            if (!IsPointerOverUIElement())
            {
                Debug.Log("Click on Object: Show Info");
                SoundManager.instance.playSound("MoreInfo");
                GameManager.instance._uiManager.ShowMoreInfo(_animalName, _animalDetails);

            }
        }

        CheckContainers();
        if (!_containerAssigned)
        {
            StartCoroutine(ResetAnimalPosition());
        }

        AnimalsManager.instance._currentSelectedAnimal = null;
    }

    void CheckContainers()
    {
        Collider2D[] nearByColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in nearByColliders)
        {
            if (collider.tag == "Group01" && !_containerAssigned)
            {
                //Debug.Log("Animal assigned to Group01");
                collider.GetComponent<Container>().CheckAnimalGroup(_animalName,GameManager.instance._gameMode, gameObject);

                AnimalsManager.instance._selectedAnimalsGroup01.Add(gameObject);
                AnimalsManager.instance._uiManager.AnimalAssigned();

                //Setting up thr color to display it is grouped
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().color= AnimalsManager.instance._uiManager._groupedColor;

                //Setting Up animation
                GetComponent<Animator>().SetTrigger("answered");

                _containerAssigned = true;
            }
            if (collider.tag == "Group02" && !_containerAssigned)
            {
                //Debug.Log("Animal assigned to Group02");
                collider.GetComponent<Container>().CheckAnimalGroup(_animalName,GameManager.instance._gameMode,gameObject);

                AnimalsManager.instance._selectedAnimalsGroup02.Add(gameObject);
                AnimalsManager.instance._uiManager.AnimalAssigned();

                //Setting up thr color to display it is grouped
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().color = AnimalsManager.instance._uiManager._groupedColor;

                //Setting Up animation
                GetComponent<Animator>().SetTrigger("answered");

                _containerAssigned = true;
            }
        }
    }

    IEnumerator ResetAnimalPosition()
    {
        while (Vector2.Distance(transform.position, _defaultPosition) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, _defaultPosition, _resetSpeed * Time.fixedDeltaTime);
            yield return null;
        }
        transform.position = _defaultPosition;
    }


    private bool IsPointerOverUIElement()
    { 
      return EventSystem.current.IsPointerOverGameObject(); }
    }
