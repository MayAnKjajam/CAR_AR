using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointOfIntrests : MonoBehaviour
{

    public GameObject CanvasPrefab;
    public Transform PanelParent;
    public string BodyPartName;
    public string Information;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = null;
        transform.GetComponent<Button>().onClick.AddListener(EnablePoI);    
    }

    public void EnablePoI()
    {
        if (obj == null)
        {
            obj = GameObject.Instantiate(CanvasPrefab, PanelParent);
            obj.transform.position = transform.position;
            obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = BodyPartName;
            obj.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Information;
        }
        else 
        {
            GameObject.Destroy(obj);
        }
    
    }
}
