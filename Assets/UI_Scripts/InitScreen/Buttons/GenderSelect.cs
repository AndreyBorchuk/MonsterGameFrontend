using UnityEngine;
using UnityEngine.UI;

public class GenderSelected : MonoBehaviour
{
    public GameObject ManModel;
    public GameObject WomanModel;
    public GameObject OtherButton;
    public byte Gender;
    public void Selected()
    {
        PlayerPrefs.SetInt("gender", Gender);

        Image buttonImage = OtherButton.GetComponent<Image>();

        Color color = buttonImage.color;
        color.a = 0.7f;
        buttonImage.color = color;

        
        Image thisButtonImage = GetComponent<Image>();

        Color thisColor = thisButtonImage.color;
        thisColor.a = 1f;
        thisButtonImage.color = thisColor;


        if (Gender == 1)
        {
            ManModel.SetActive(false);
            WomanModel.SetActive(true);
            return;
        }

        ManModel.SetActive(true);
        WomanModel.SetActive(false);
    }
}
