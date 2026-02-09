using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowNewAcc : MonoBehaviour
{
    public GameObject[] Buttons;
    public TMP_InputField InputUsername;
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
        InputUsername.text = "";
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Hide");
    }
}
