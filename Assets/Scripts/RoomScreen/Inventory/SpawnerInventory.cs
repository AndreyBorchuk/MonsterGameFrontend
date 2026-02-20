using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerInventory : MonoBehaviour
{
    public GameObject ParentTeam;
    public GameObject InventoryMonster;
    public GameObject ParentInventory;
    public BundleLoader bLoader;

    Vector3 getTeamPosition(byte position)
    {
        switch (position)
        {
            case 0: return new Vector3(-363f, 178f);
            case 1: return new Vector3(-14f, 178f);
            case 2: return new Vector3(335f, 178f);
            case 3: return new Vector3(-363f, -160f);
            case 4: return new Vector3(-14f, -160f);
            default: return new Vector3(-363f, 178f);
        }
    }

    Vector3 getMonsterPosition(int position)
    {
        float stepX = 349;
        float stepY = 338;
        float startX = -363;
        float startY = 780;

        return new Vector3(startX + (position % 3) * stepX, startY - (position / 3) * stepY);
    }

    Color getElementColor(string elementType)
    {
        switch (elementType)
        {
            case "fire": return new Color(1f, 0f, 0f);
            case "leaf": return new Color(0f, 0.5f, 0f);
            case "water": return new Color(0f, 0f, 1f);
            case "wind": return new Color(0.99f, 0.91f, 0.06f);
            case "rock": return new Color(0.53f, 0.5f, 0.46f);
            default: return Color.white;
        }
    }

    Monster getMonsterById(string id)
    {
        return PlayerData.Inventory[id];
    }

    void SetupMonsterCard(GameObject spawnedMonster, Monster monsterData, Vector3 localPos, bool inTeam, GameObject parent)
    {
        spawnedMonster.transform.SetParent(parent.transform, false);
        spawnedMonster.transform.localPosition = localPos;

        var elementTransform = spawnedMonster.transform.Find("Element");
        if (elementTransform != null)
        {
            var elementImage = elementTransform.GetComponent<UnityEngine.UI.Image>();
            if (elementImage != null)
                elementImage.color = getElementColor(monsterData.element);
        }

        bLoader.LoadMonster(monsterData.monster_id, spawnedMonster);

        var data = spawnedMonster.GetComponent<InventoryMonster>();
        if (data != null)
        {
            data.InTeam = inTeam;
            data.MonsterData = monsterData;
        }
    }

    public void SpawnInventory()
    {
        var monsterTeam = PlayerData.Team;
        var monsters = PlayerData.Inventory;

        if (monsters == null)
        {
            return;
        }

        // Команда
        byte teamIdx = 0;
        foreach (string monsterId in monsterTeam)
        {
            Monster asMonster = getMonsterById(monsterId);
            var spawnedMonster = Instantiate(InventoryMonster);
            SetupMonsterCard(spawnedMonster, asMonster, getTeamPosition(teamIdx), true, ParentTeam);
            teamIdx++;
        }

        // Инвентарь (без монстров, уже стоящих в команде)
        int invIdx = 0;
        foreach (var pair in monsters)
        {
            Monster asMonster = pair.Value;

            bool isInTeam = false;
            foreach (string teamId in monsterTeam)
            {
                if (teamId == asMonster.id)
                {
                    isInTeam = true;
                    break;
                }
            }

            if (isInTeam) continue;

            var prefab = InventoryMonster;
            var parent = ParentInventory;

            var spawnedMonster = Instantiate(prefab);
            SetupMonsterCard(spawnedMonster, asMonster, getMonsterPosition(invIdx), false, parent);
            invIdx++;
        }
    }
}
