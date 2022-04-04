using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects; // объекты, захватываемые камерой

    [SerializeField] private float _speed; // 

    [SerializeField] private Camera _camera;

    private void FixedUpdate()
    {
        Vector3 finalPos = Vector3.zero;

        for(int i = 0; i < _objects.Length; i++) 
        {
            finalPos = (_objects[i].transform.position + finalPos); // складываем все вектора
        }

        finalPos.z = -7f; // задаём координату z, чтобы камера не была в одной плоскости с объектами и видела их

        transform.position = Vector3.Lerp(transform.position, finalPos / _objects.Length, _speed * Time.fixedDeltaTime); // установка позиции камеры

        _camera.orthographicSize = GetCameraSize(); // устанавливаем размер камеры
    }

    private float GetCameraSize()
    {
        float finalDistance = 0;
        Vector3 lastObjectPosition = _objects[0].transform.position;

        for(int i = 0; i < _objects.Length; i++)
        {
            finalDistance = Vector3.Distance(lastObjectPosition, _objects[i].transform.position); // рассчёт дистанции между космическими объектами
            lastObjectPosition = _objects[i].transform.position; // позиция текущего объекта
        }

        return finalDistance; // возвращаем конечную дистанцию
    }
}
