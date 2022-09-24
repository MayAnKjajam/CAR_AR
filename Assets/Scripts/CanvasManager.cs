using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject ScrollView, SearchPanel;
    public Button Screenshot;
    public ListAllItemsAsButtons objectManager;
    // Start is called before the first frame update
    void Start()
    {
        Screenshot.onClick.AddListener(takeScreenshot);
           objectManager = GameObject.FindObjectOfType<ListAllItemsAsButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in objectManager.AllActiveObjects)
        {
            if (g.GetComponent<ScaleMoveRotate>().Canvas.activeInHierarchy == true)
            {
                ScrollView.SetActive(false);
                SearchPanel.SetActive(false);
            }
            else
            {
                ScrollView.SetActive(true);
                SearchPanel.SetActive(true);
            }
        }
    }

    public void takeScreenshot()
    {
        ScreenshotHandler.TakeScreenshot_static(Screen.width, Screen.height);
    }
}
