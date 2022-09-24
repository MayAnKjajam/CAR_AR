using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleMoveRotate : MonoBehaviour
{
    public bool Rotate, Scale, Move;
    public float rotatespeed = 100f, momentspeed = 0.01f;
    private float startingPosition;
    private float initialDistance;
    private Vector3 initialScale;
    public GameObject Canvas;
    public Button ScaleBtn, RotateBtn, MoveBtn,close;
    public MeshRenderer Car;
    public ListAllItemsAsButtons objectManager;
    public Text CarName;

    private void Start()
    {
        ScaleBtn.onClick.AddListener(delegate { UpdateBooleans("Scale", ScaleBtn); });
        RotateBtn.onClick.AddListener(delegate { UpdateBooleans("Rotate", RotateBtn); });
        MoveBtn.onClick.AddListener(delegate { UpdateBooleans("Move", MoveBtn); });
        close.onClick.AddListener(ResetAllBools);
        Canvas.SetActive(false);
        Car = transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>();
        objectManager = GameObject.FindObjectOfType<ListAllItemsAsButtons>();
    }

    private void OnMouseDown()
    {
        if (!Scale && !Rotate && !Move && !CameraOverUIObject.obstacle)
        {
            if (Canvas.activeInHierarchy)
            {
                Canvas.SetActive(false);
            }
            else
            {
                foreach (GameObject g in objectManager.AllActiveObjects)
                {
                    g.GetComponent<ScaleMoveRotate>().Move = false;
                    g.GetComponent<ScaleMoveRotate>().Scale = false;
                    g.GetComponent<ScaleMoveRotate>().Rotate = false;
                    g.GetComponent<ScaleMoveRotate>().ScaleBtn.interactable = true;
                    g.GetComponent<ScaleMoveRotate>().RotateBtn.interactable = true;
                    g.GetComponent<ScaleMoveRotate>().MoveBtn.interactable = true;
                    g.GetComponent<ScaleMoveRotate>().Canvas.SetActive(false);
                }
                Canvas.SetActive(true);
            }
        }
    }
    void Update()
    {
        
        if (Input.touchCount > 0 && Rotate && !CameraOverUIObject.obstacle)
        {
            RotateObject();
        }
        if (Input.touchCount > 0 && Scale && !CameraOverUIObject.obstacle)
        {
            ScaleObject();
        }
        if (Input.touchCount > 0 && Move && !CameraOverUIObject.obstacle)
        {
            MoveObject();
        }
    }

    public void UpdateBooleans(string action, Button btn)
    {
        ScaleBtn.interactable = true;
        RotateBtn.interactable = true;
        MoveBtn.interactable = true;
        btn.interactable = false;
        Move = false;
        Scale = false;
        Rotate = false;
        if (action == "Scale")
        {
            Scale = true;
        }
        else if (action == "Rotate")
        {
            Rotate = true;
        }
        else if (action == "Move")
        { Move = true; }
    }
    public void RotateObject()
    {
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startingPosition = touch.position.x;
                break;
            case TouchPhase.Moved:
                if (startingPosition > touch.position.x)
                {
                    transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);
                }
                else if (startingPosition < touch.position.x)
                {
                    transform.Rotate(Vector3.up, -rotatespeed * Time.deltaTime);
                }
                break;
            case TouchPhase.Ended:
                Debug.Log("Touch Phase Ended.");
                break;
            case TouchPhase.Stationary:
                startingPosition = touch.position.x;
                break;
        }
    }

    public void ScaleObject()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = transform.localScale;
            }
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; 
                }

                var factor = currentDistance / initialDistance;
                transform.localScale = initialScale * factor;
            }
        }
    }

    public void MoveObject()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved)
        {
            transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * momentspeed, transform.position.y, transform.position.z + touch.deltaPosition.y * momentspeed);
        }
    }
 

    public void ColorChangeOnClick(GameObject Button)
    {
        Car.material.color = Button.GetComponent<Image>().color;
    }

    private void ResetAllBools()
    {
        Move = false;
        Scale = false;
        Rotate = false;
        ScaleBtn.interactable = true;
        RotateBtn.interactable = true;
        MoveBtn.interactable = true;
        Canvas.SetActive(false);
    }
}


