using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SearchObjectButtonScript : MonoBehaviour
{

    public string AssetName;
    public ListAllItemsAsButtons objectManager;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = GameObject.FindObjectOfType<ListAllItemsAsButtons>();
        transform.GetComponent<Button>().onClick.AddListener(DisableAllEnableCurrent);
    }

    public void DisableAllEnableCurrent()
    {
        foreach (GameObject g in objectManager.AllActiveObjects)
        {
            try
            {
                if (g.gameObject.name != AssetName)
                {
                    g.SetActive(false);
                }
            }
            catch (System.Exception)
            { }
        }
      
    }
}
