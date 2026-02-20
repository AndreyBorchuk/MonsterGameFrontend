using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public ErrorManager errorManager;
    public GetInventory getInv;
    public GameObject updateMessage;
    private string validJoinUrl = "/api/valid_join";
    private class Payload
    {
        public string version;
        public string user_id;
        public string token;
    }
    private class ErrorResponse
    {
        public string error;
        public int status_code;
    }
    private class SuccessResponseValid
    {
        public string username;
        public int status_code;
    }
    public void SendValidJoin()
    {
        StartCoroutine(SendRequestCoroutine());
    }
    private void ActionAfter()
    {
        SceneManager.LoadScene("RoomScene");
    }
    public IEnumerator SendRequestCoroutine()
    {
        var payload = new Payload
        {
            version = DataHolder.Version, // получаем данные из сохранений. version записывается в init
            user_id = PlayerPrefs.GetString("user_id"),
            token = PlayerPrefs.GetString("token")
        };

        var json = JsonUtility.ToJson(payload); // преобразуем структуру в json
        var request = new UnityWebRequest(DataHolder.ApiURL + validJoinUrl, UnityWebRequest.kHttpVerbPOST);
        
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json"); // подготавливаем запрос к отправке

        yield return request.SendWebRequest(); //отправка

        if (request.isNetworkError || request.isHttpError) // ошибка сети
        {
            errorManager.SpawnErrorMessage("network_error_header", "network_error", true);
            yield break;
        }

        var responseText = request.downloadHandler.text; // текст ответа
        if (!responseText.Contains("\"status_code\"")) // проверка наличия важного параметра status_code
        {
            errorManager.SpawnErrorMessage("server_error_header", "server_error", true);
            yield break;
        }

        var response = JsonUtility.FromJson<ErrorResponse>(responseText); // преобразуем json в структуру ошибки, если она есть
        if (response != null && response.status_code == 6)
        {
            errorManager.SpawnErrorMessage("create_error_header", response.error, true);
            yield break;
        }

        if (response != null && response.status_code == 10) // если требуется обновление
        {
            GameObject newError = Instantiate(updateMessage, new Vector3(0f, 0f, 0f), new Quaternion());
            yield break;
        }

        if (response != null && response.status_code == 1)
        {
            LoggedOut.Logout();
            errorManager.SpawnErrorMessage("logged_out", response.error, true);
            yield break;
        }
        

        var responseSuccess = JsonUtility.FromJson<SuccessResponseValid>(responseText); // успешный ответ
        if (responseSuccess != null && responseSuccess.status_code == 0)
        {
            DataHolder.Username = responseSuccess.username;
            getInv.SendGetInventory(errorManager, updateMessage, ActionAfter);
            yield break;
        }

        errorManager.SpawnErrorMessage("create_error_header", "server_error", true); // неизвестная ошибка
        yield break;
    }
}
