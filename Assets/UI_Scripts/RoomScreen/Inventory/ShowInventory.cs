using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public SpawnerInventory spawnerInventory;
    public GetInventory getInventory;
    public ErrorManager errorManager;
    public GameObject updateMessage;
    public void action()
    {
        spawnerInventory.SpawnInventory();
    }   

    public void ShowInventoryPanel()
    {
        GetComponent<Animator>().SetTrigger("Show");
        getInventory.SendGetInventory(errorManager, updateMessage, action);
    }
    public void HideInventoryPanel()
    {
        GetComponent<Animator>().SetTrigger("Hide");
    }
}
