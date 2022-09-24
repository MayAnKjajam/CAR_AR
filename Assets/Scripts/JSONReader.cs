using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{
    public TextAsset ConfigFile;
    // Start is called before the first frame update
    [System.Serializable]
    public class varients
    {
        public string carNumber;
        public string carName;
        public string prefabName;
    }


    [System.Serializable]
    public class ItemList
    {
        public varients[] varients;
    }

    public ItemList AllItems = new ItemList();
   
    public void Awake()
    {
        ConfigFile = Resources.Load<TextAsset>("CarVarients");
        AllItems = JsonUtility.FromJson<ItemList>(ConfigFile.text);

    }

}
