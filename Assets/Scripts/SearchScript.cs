using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SearchScript : MonoBehaviour
{

    public GameObject ContentHolder;
    public GameObject[] elements;
    public GameObject SearchBar;
    public int totalelements;
   
    public void Search()
    {
        string SearchText = SearchBar.GetComponent<TMP_InputField>().text;
        int SearchLength = SearchText.Length;

        int SearchedElements = 0;
        foreach (GameObject obj in elements)
        {
            SearchedElements += 1;
            if (obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length >= SearchLength)
            {
                if (SearchText.ToLower() == obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(0, SearchLength).ToLower())
                {
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(false);
                }
            }
        }
    }
  
}
