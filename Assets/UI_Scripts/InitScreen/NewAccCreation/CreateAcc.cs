using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting.FullSerializer;

public class CreateAcc : MonoBehaviour
{
    public TMP_InputField Username;
    public ErrorManager errorManager;
    public Toggle Agree;
    public GameObject mainGameScript;
    private string Gender = "male";
    private string createAccountUrl = "/api/create_acc";

    public void SetGender(string gender)
    {
        Gender = gender;
    }

    public void SendCreateAccountRequest()
    {
        StartCoroutine(SendRequestCoroutine());
    }

    public IEnumerator SendRequestCoroutine()
    {
        var payload = new CreateAccountPayload
        {
            version = DataHolder.Version,
            username = Username.text,
            agree = Agree.isOn,
            gender = Gender
        };

        var json = JsonUtility.ToJson(payload);
        var request = new UnityWebRequest(DataHolder.ApiURL + createAccountUrl, UnityWebRequest.kHttpVerbPOST);
        
        Debug.Log(json);

        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Username.interactable = true;
        Agree.interactable = true;

        var submitButton = GetComponent<Button>();
        if (submitButton != null)
        {
            submitButton.interactable = true;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            errorManager.SpawnErrorMessage("network_error_header", "network_error", true);
            yield break;
        }

        var responseText = request.downloadHandler.text;
        if (!responseText.Contains("\"status_code\""))
        {
            errorManager.SpawnErrorMessage("server_error_header", "server_error", true);
            yield break;
        }
        Debug.Log(responseText);

        var response = JsonUtility.FromJson<CreateResponse>(responseText);
        if (response != null && response.status_code == 6)
        {
            errorManager.SpawnErrorMessage("create_error_header", response.error, true);
            yield break;
        }

        var responseSuccess = JsonUtility.FromJson<CreateSuccessResponse>(responseText);
        if (responseSuccess != null && responseSuccess.status_code == 0)
        {
            PlayerPrefs.SetString("token", responseSuccess.token);
            PlayerPrefs.SetString("user_id", responseSuccess.user_id);
            mainGameScript.GetComponent<Init>().ChangeState(true);
            
            yield break;
        }

        errorManager.SpawnErrorMessage("create_error_header", response != null ? response.error : "server_error", true);
        yield break;
    }


    [System.Serializable]
    private class CreateSuccessResponse
    {
        public int status_code;
        public string user_id;
        public string token;
    }

    [System.Serializable]
    private class CreateResponse
    {
        public int status_code;
        public string error;
    }

    [System.Serializable]
    private class CreateAccountPayload
    {
        public string version;
        public string username;
        public bool agree;
        public string gender;
    }
    private static bool hasSpecialChar(string input)
    {
        string specialChar = @"*\|!#$%&/()=?»«@£§€{}.-;'<>_,";
        foreach (var item in specialChar)
        {
            if (input.Contains(item)) return true;
        }

        return false;
    }
    public void Create()
    {
        string username = Username.text;
        if (hasSpecialChar(username) || username.Length < 4 || username.Length > 14)
        {
            errorManager.SpawnErrorMessage("username_error", "bad_username", true);
            Username.text = "";
            return;
        }

        if (!Agree.isOn)
        {
            errorManager.SpawnErrorMessage("terms_error", "must_agree", true);
            return;
        }
        SendCreateAccountRequest();
        Username.interactable = false;
        Agree.interactable = false;

        var submitButton = GetComponent<Button>();
        if (submitButton != null)
        {
            submitButton.interactable = false;
        }
    }
}
