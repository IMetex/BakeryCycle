using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using ObjectPooler;
using Singleton;
using UnityEngine;

public class CoreUtility : Singleton<CoreUtility>
{
    public void InstantiateObject(Transform parentPos, GameObject intantiatePrefab, float newHeight)
    {
        Vector3 newPosition = new Vector3(parentPos.position.x, parentPos.position.y + newHeight,
            parentPos.position.z);
        var newObject = Instantiate(intantiatePrefab, newPosition, Quaternion.identity);
        newObject.transform.SetParent(parentPos);
    }

    public void DestroyChildren(GameObject parent)
    {
        if (parent == null)
            return;

        Transform[] children = parent.GetComponentsInChildren<Transform>();
        for (int i = children.Length - 1; i >= 0; i--)
        {
            if (children[i] != parent.transform)
            {
                Destroy(children[i].gameObject);
            }
        }

        /* foreach (Transform child in children) {
            if (child != parent.transform) {
                Destroy(child.gameObject); } */
    }

    public IEnumerator SpawnObjectsCoroutine(
        List<GameObject> objectsToSpawn, List<GameObject> targetObjectsList,
        Transform spawnTransform,
        float initialHeight, float heightIncrement, float delay)
    {
        if (objectsToSpawn == null)
            yield break;

        float currentHeight = initialHeight;

        while (objectsToSpawn.Count > 0)
        {
            GameObject prefabToInstantiate = objectsToSpawn[0];
            InstantiateObject(spawnTransform, prefabToInstantiate, currentHeight);
            objectsToSpawn.RemoveAt(0);
            targetObjectsList.Add(prefabToInstantiate);
            currentHeight += heightIncrement;
            yield return new WaitForSeconds(delay);
        }
    }

    public void TakeTweenAnimaton(GameObject obj)
    {
        var scale = new Vector3(4.8f, 4.8f, 4.8f);
        Vector3 doScale = scale * 1.5f;

        obj.transform.DOScale(doScale, 0.06f).OnComplete(() =>
            obj.transform.DOScale(scale, 0.06f));
    }
}