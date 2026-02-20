using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class GetClothes : MonoBehaviour
{    
    private string inventoryUrl = "/api/player/get_clothes";
    private class ErrorResponse
    {
        public string error;
        public int status_code;
    }
    private class Payload
    {
        public string version;
        public string user_id;
        public string token;
    }

    [Serializable]
    private class SuccessResponse
    {
        public ClothesItem[] clothes;
        public int status_code;
    }
    public void SendGetClothes(ErrorManager errorManager, GameObject updateMessage, Action action)
    {
        StartCoroutine(GetClothesCoroutine(errorManager, updateMessage, action));
    }
    public IEnumerator GetClothesCoroutine(ErrorManager errorManager, GameObject updateMessage, Action action)
    {
        var payload = new Payload
        {
            version = DataHolder.Version, // получаем данные из сохранений. version записывается в init
            user_id = PlayerPrefs.GetString("user_id"),
            token = PlayerPrefs.GetString("token")
        };

        var json = JsonUtility.ToJson(payload); // преобразуем структуру в json
        var request = new UnityWebRequest(DataHolder.ApiURL + inventoryUrl, UnityWebRequest.kHttpVerbPOST); 
        
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
        Debug.Log(responseText);

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

        var responseSuccess = JsonUtility.FromJson<SuccessResponse>(responseText); // успешный ответ
        if (responseSuccess != null && responseSuccess.status_code == 0)
        {
            PlayerData.Clothes = responseSuccess.clothes;
            action();
            yield break;
        }

        errorManager.SpawnErrorMessage("create_error_header", "server_error", true); // неизвестная ошибка
        yield break;
    }
}
