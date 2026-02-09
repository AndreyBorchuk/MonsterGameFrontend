using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    public TMP_Text Header;
    public TMP_Text ErrorText;
    public void KillErrorBox()
    {
        Destroy(gameObject);
    }
    public TMP_Text[] GetTextFields()
    {
        return new TMP_Text[] {Header, ErrorText};
    }
}
