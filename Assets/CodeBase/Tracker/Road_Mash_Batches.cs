using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        if (meshFilters.Length == 0)
        {
            Debug.LogError("Нет объектов для объединения!");
            return;
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false); // Отключаем исходные объекты
        }

        // Создание нового объекта в иерархии
        GameObject combinedRoad = new GameObject("CombinedRoad");
        combinedRoad.transform.position = Vector3.zero;

        MeshFilter meshFilter = combinedRoad.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);

        // Проверка создания меша
        if (meshFilter.mesh.vertexCount > 0)
        {
            Debug.Log("Новый объединённый меш создан, количество вершин: " + meshFilter.mesh.vertexCount);
        }
        else
        {
            Debug.LogError("Ошибка создания меша, меш не содержит вершин.");
        }

        MeshRenderer meshRenderer = combinedRoad.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = meshFilters[0].GetComponent<Renderer>().sharedMaterial;

        // Добавляем коллайдер
        combinedRoad.AddComponent<MeshCollider>();

        // Активируем объект и добавляем его в иерархию
        combinedRoad.SetActive(true);
    }
}