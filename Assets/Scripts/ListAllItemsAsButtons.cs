using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListAllItemsAsButtons : MonoBehaviour
{
    public int ButtonCount;
    public JSONReader Manager;
    public GameObject ButtonPrefab,SearchButtonPrefab;
    public Transform ButtonParent,SearchButtonParent;
    [SerializeField]
    public List<GameObject> AllActiveObjects;
    public SearchScript search;
    // Start is called before the first frame update
    void Start()
    {
        ButtonCount = Manager.AllItems.varients.Length;
        search = GameObject.FindObjectOfType<SearchScript>();
        Debug.Log(ButtonCount);
        GenrateButtons();
        CreatSearchElements();
    }

    // Update is called once per frame
    public void GenrateButtons()
    {
        for (int i = 0; i < ButtonCount; i++)
        {
            GameObject ButtonObj = Instantiate(ButtonPrefab as GameObject, ButtonParent);
            GameObject ButtonObj1 = Instantiate(SearchButtonPrefab as GameObject, SearchButtonParent);
            JSONReader.varients Item = Manager.AllItems.varients[i];
            ButtonObj.GetComponent<LoadAssetOnClick>().AssetName = Item.prefabName;
            ButtonObj.GetComponent<LoadAssetOnClick>().ButtonName = Item.carName;
            ButtonObj1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Item.carName;
            ButtonObj1.GetComponent<SearchObjectButtonScript>().AssetName = Item.prefabName+"(Clone)";
        }
    }
    public void CreatSearchElements()
    {
            search.totalelements = SearchButtonParent.transform.childCount;
            search.elements = new GameObject[search.totalelements];
            for (int i = 0; i < search.totalelements; i++)
            {

             search.elements[i] = search.ContentHolder.transform.GetChild(i).gameObject;
            }
    }    

}
