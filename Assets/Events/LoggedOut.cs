using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoggedOut
{
    public static void Logout()
    {
        PlayerData.Username = "";
        PlayerPrefs.SetString("user_id", "");
        PlayerPrefs.SetString("token", "");
        SceneManager.LoadScene("InitScene");
    }
}
