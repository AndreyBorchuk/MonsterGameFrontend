using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoggedOut
{
    public static void Logout()
    {
        DataHolder.Username = "";
        PlayerPrefs.SetString("user_id", "");
        PlayerPrefs.SetString("token", "");
        SceneManager.LoadScene("InitScene");
    }
}
