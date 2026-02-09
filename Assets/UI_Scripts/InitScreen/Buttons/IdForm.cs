using UnityEngine;

public class IdForm : MonoBehaviour
{
    public GameObject form;
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            form.GetComponent<Animator>().SetTrigger("Next");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
