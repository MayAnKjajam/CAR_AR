using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class LoginAndRegister : MonoBehaviour
{

    public InputField LoginUsernameInput, LoginPasswordInput, RegisterUsernameInput, RegisterPasswordInput, RegisterConfirmPassword;
    public Button Login, Register;
    public Text Message=null;
    ArrayList Credentials;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Message.text = null;
    }

    void Start()
    {
        
        Login.onClick.AddListener(CheckCredentials);
        Register.onClick.AddListener(RegisterCredentials);

        if (File.Exists(Application.persistentDataPath + "/LoginCredentials.txt"))
        {
            Credentials = new ArrayList(File.ReadAllLines(Application.persistentDataPath + "/LoginCredentials.txt"));
           
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/LoginCredentials.txt","");
        }
    }

    // Update is called once per frame
    public void CheckCredentials()
    {
        bool isExist = false;
        Credentials = new ArrayList(File.ReadAllLines(Application.persistentDataPath + "/LoginCredentials.txt"));
        foreach (var I in Credentials)
        {
            string line = I.ToString();
            if (I.ToString().Substring(0, I.ToString().IndexOf(":")).Equals(LoginUsernameInput.text) && I.ToString().Substring(I.ToString().IndexOf(":") + 1).Equals(LoginPasswordInput.text))
            {
                isExist = true;
                break;
            }
        }
        if (isExist)
        {
            Message.text = "Loging in as "+LoginUsernameInput.text;
            StartCoroutine(LoadScene());
        }
        else
        { 
            StartCoroutine(MessagePopup("Incorrect Credentials!"));
        }
    }

    public void RegisterCredentials()
    {
        bool isExist = false;
        Credentials = new ArrayList(File.ReadAllLines(Application.persistentDataPath + "/LoginCredentials.txt"));
        foreach(var I in Credentials)
        {
            if (I.ToString().Contains(RegisterUsernameInput.text))
            {
                isExist = true;
                break;
            }
        }
        if (isExist)
        {
            StartCoroutine(MessagePopup("User already exists!"));
        }
        else
        {
            if (RegisterPasswordInput.text == RegisterConfirmPassword.text)
            {
                Credentials.Add(RegisterUsernameInput.text + ":" + RegisterPasswordInput.text);
                File.WriteAllLines(Application.persistentDataPath + "/LoginCredentials.txt", (String[])Credentials.ToArray(typeof(string)));
                StartCoroutine(MessagePopup("User Registered successfully!"));
            }
            else
            {
                StartCoroutine(MessagePopup("Password mismatch!"));
            }
        }
    }

    IEnumerator MessagePopup(string Text)
    {
        Message.text = Text;
        yield return new WaitForSeconds(3f);
        Message.text = null;
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ARScene");
    }
}
