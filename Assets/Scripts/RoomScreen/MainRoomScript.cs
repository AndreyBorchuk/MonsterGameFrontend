using UnityEngine;

public class MainRoomScript : MonoBehaviour
{
    public GameObject SpawnInvOwner;
    public SpawnerInventory spawner;
    void Start()
    {
        spawner.SpawnInventory();
    }
}
