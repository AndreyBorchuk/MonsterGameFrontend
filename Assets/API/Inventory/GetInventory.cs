using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class GetInventory : MonoBehaviour
{    
    private string inventoryUrl = "/api/player/inventory";
    private class ErrorResponse
    {
        public string error;
        public int status_code;
    }
    [System.Serializable]
    private class Wallet
    {
        public int energy;
        public int coins_default;
        public int coins_expensive;
        public int level;
    }

    [System.Serializable]
    private class InventoryItem
    {
        public string monster_id;
        public MonsterStats stats;
    }

    [System.Serializable]
    private class MonsterStats
    {
        public string monster_id;
        public string name;
        public string rarity;
        public int level;
        public int xp;
        public int atk;
        public int hp;
        public int recovery;
        public string element;
        public string[] weapons;
        public string active;
        public string passive;
        public string village;
    }
    private class Payload
    {
        public string version;
        public string user_id;
        public string token;
    }

    [System.Serializable]
    private class SuccessResponse
    {
        public InventoryItem[] inventory;
        public int status_code;
        public string[] team;
        public Wallet wallet;
    }
    public void SendGetInventory(ErrorManager errorManager, GameObject updateMessage, Action action)
    {
        StartCoroutine(GetInventoryCoroutine(errorManager, updateMessage, action));
    }
    public IEnumerator GetInventoryCoroutine(ErrorManager errorManager, GameObject updateMessage, Action action)
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
            DataHolder.Inventory = new Dictionary<string, Monster>(); // ВЫГРУЖАЕМ ДАННЫЕ, ЧТОБ ИСПОЛЬЗОВАТЬ ВЕЗДЕ
            foreach (var item in responseSuccess.inventory)
            {
                Monster monster = new Monster
                {
                    id = item.monster_id,
                    monster_id = item.stats.monster_id,
                    name = item.stats.name,
                    rarity = item.stats.rarity,
                    level = item.stats.level,
                    xp = item.stats.xp,
                    atk = item.stats.atk,
                    hp = item.stats.hp,
                    recovery = item.stats.recovery,
                    element = item.stats.element,
                    weapons = item.stats.weapons != null ? new List<string>(item.stats.weapons) : new List<string>(),
                    active = item.stats.active,
                    passive = item.stats.passive,
                    village = item.stats.village
                };
                DataHolder.Inventory.Add(item.monster_id, monster); 
            }
            DataHolder.Team = responseSuccess.team;
            DataHolder.Energy = responseSuccess.wallet.energy;
            DataHolder.Level = responseSuccess.wallet.level;
            DataHolder.CoinsDefault = responseSuccess.wallet.coins_default;
            DataHolder.CoinsExpensive = responseSuccess.wallet.coins_expensive;
            action();
            yield break;
        }

        errorManager.SpawnErrorMessage("create_error_header", "server_error", true); // неизвестная ошибка
        yield break;
    }
}
