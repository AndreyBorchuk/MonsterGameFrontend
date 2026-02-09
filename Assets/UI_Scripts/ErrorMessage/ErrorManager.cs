using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public GameObject errorMessage;
    public void SpawnErrorMessage(string header, string message, bool hasButton)
    {
        GameObject newError = Instantiate(errorMessage, new Vector3(0f, 0f, 0f), new Quaternion());
        Button okButton = newError.GetComponentInChildren<Button>();
        TMP_Text[] Params = newError.GetComponent<ErrorMessage>().GetTextFields();
        Params[0].text = header;
        Params[1].text = message;
        okButton.gameObject.SetActive(hasButton);
    }
}
