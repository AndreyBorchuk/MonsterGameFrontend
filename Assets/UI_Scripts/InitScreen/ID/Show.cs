using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    public GameObject[] Buttons;
    public TMP_InputField InputEmail;
    public TMP_InputField InputPassword;
    public void ShowObject()
    {
        foreach (GameObject Button in Buttons)
        {
            Button.GetComponent<Button>().interactable = false;
        }
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Show");
    }

    public void Disable()
    {
        foreach (GameObject Button in Buttons)
        {
            Button.GetComponent<Button>().interactable = true;
        } 
        InputEmail.text = "";
        InputPassword.text = "";
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Hide");
    }
}
