using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BundleLoader : MonoBehaviour
{
    private int version = 0;
    private static Dictionary<string, Sprite> _cachedMonsters = new Dictionary<string, Sprite>();
    private static HashSet<string> _loadingBundles = new HashSet<string>();

    public void LoadMonster(string monster_id, GameObject monster)
    {
        StartCoroutine(DownloadAndCacheMonster(monster_id, monster));
    }

    IEnumerator DownloadAndCacheMonster(string monster_id, GameObject monster)
    {
        Debug.Log($"Loading {monster_id}");
        while (!Caching.ready)
        {
            yield return null;
        }

        if (_cachedMonsters.Count > 100)
        {
            _cachedMonsters.Clear();
        }

        string bundleUrl = $"{DataHolder.ResourcesURL}/Assets/{monster_id}";

        // Если спрайт уже закеширован — сразу применяем
        if (_cachedMonsters.ContainsKey(monster_id) && _cachedMonsters[monster_id] != null)
        {
            ApplySprite(monster, _cachedMonsters[monster_id]);
            yield break;
        }

        // Если кто-то уже загружает этот бандл — ждём
        if (_loadingBundles.Contains(monster_id))
        {
            while (_loadingBundles.Contains(monster_id))
                yield return null;

            // После загрузки другим корутином — применяем закешированный спрайт
            if (_cachedMonsters.ContainsKey(monster_id) && _cachedMonsters[monster_id] != null)
            {
                ApplySprite(monster, _cachedMonsters[monster_id]);
            }
            yield break;
        }

        _loadingBundles.Add(monster_id);

        var response = WWW.LoadFromCacheOrDownload(bundleUrl, version);
        yield return response;

        if (!string.IsNullOrEmpty(response.error))
        {
            Debug.Log($"Failed download {monster_id}");
            _loadingBundles.Remove(monster_id);
            yield break;
        }

        var assetBundle = response.assetBundle;
        string texture = $"{monster_id}.png";
        var spriteRequest = assetBundle.LoadAssetAsync(texture, typeof(Sprite));
        yield return spriteRequest;

        var tex = spriteRequest.asset as Sprite;
        if (tex == null)
        {
            Debug.Log($"Texture not found: {texture}");
            assetBundle.Unload(false);
            _loadingBundles.Remove(monster_id);
            yield break;
        }

        // Кешируем спрайт (не бандл)
        _cachedMonsters[monster_id] = tex;

        ApplySprite(monster, tex);

        assetBundle.Unload(false);
        _loadingBundles.Remove(monster_id);
    }

    private void ApplySprite(GameObject monster, Sprite sprite)
    {
        var imageTransform = monster.transform.Find("MonsterImage");
        var image = imageTransform != null ? imageTransform.GetComponent<Image>() : null;
        if (image != null)
        {
            image.sprite = sprite;
            image.preserveAspect = true;
        }
    }
}