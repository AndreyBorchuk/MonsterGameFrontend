using UnityEngine;
using UnityEngine.XR;

public class Init : MonoBehaviour
{
    public bool LoggedIn = false;
    public string Version = "1.0.0";
    public LoadGame loadGame;
    public GameObject LoggedInScene;
    public GameObject NotLoggedInScene;
    private string[] keys = {"user_id", "token"};
    public bool CheckUser()
    {
        if (PlayerPrefs.GetString("user_id") == "" || PlayerPrefs.GetString("token") == "")
        {
            return false;
        }
        return true;
    }
    public void ChangeState(bool Logged)
    {
        LoggedInScene.SetActive(Logged);
        NotLoggedInScene.SetActive(!Logged);
        if (Logged)
        {
            loadGame.SendValidJoin();
        }
    }
    private void CheckKeys()
    {
        foreach(string key in keys)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetString(key, "");
            }
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        CheckKeys();
        LoggedIn = CheckUser();
        ChangeState(LoggedIn); // добавить проверку на обновление
    }
}
