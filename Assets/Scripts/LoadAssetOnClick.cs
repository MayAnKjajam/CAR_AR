using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoadAssetOnClick : MonoBehaviour
{
    public string AssetName;
    public string ButtonName;
    public Button btn;
    public ListAllItemsAsButtons objectManager;
    public GameObject Object;
    // Start is called before the first frame update
    public void Start()
    {
        Object = null;
        btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(EnableObject);
        transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ButtonName;
        objectManager = GameObject.FindObjectOfType<ListAllItemsAsButtons>();
    }

    public void EnableObject()
    {
        if (Object!=null)
        {
            if(!Object.activeInHierarchy)
                Object.SetActive(true);
        }
        else
        {
            bool NewObject = true;
            try
            {
                foreach (GameObject g in objectManager.AllActiveObjects)
                {
                    if (g.name == AssetName + "(Clone)")
                    {
                        NewObject = false;
                    }
                }
            }
            catch (Exception) { NewObject = true; }
            if (NewObject)
            {
                Transform Cursor = GameObject.FindObjectOfType<CursorPlacement>().transform;
                GameObject objectToPlace = Resources.Load<GameObject>(AssetName);
                GameObject obj = GameObject.Instantiate(objectToPlace, Cursor.transform.position, Cursor.transform.rotation);
                obj.GetComponent<ScaleMoveRotate>().CarName.text = ButtonName;
                objectManager.AllActiveObjects.Add(obj);
                Object = obj;
            }
        }
    }
}
