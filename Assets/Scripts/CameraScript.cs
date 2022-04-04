using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects; // �������, ������������� �������

    [SerializeField] private float _speed; // 

    [SerializeField] private Camera _camera;

    private void FixedUpdate()
    {
        Vector3 finalPos = Vector3.zero;

        for(int i = 0; i < _objects.Length; i++) 
        {
            finalPos = (_objects[i].transform.position + finalPos); // ���������� ��� �������
        }

        finalPos.z = -7f; // ����� ���������� z, ����� ������ �� ���� � ����� ��������� � ��������� � ������ ��

        transform.position = Vector3.Lerp(transform.position, finalPos / _objects.Length, _speed * Time.fixedDeltaTime); // ��������� ������� ������

        _camera.orthographicSize = GetCameraSize(); // ������������� ������ ������
    }

    private float GetCameraSize()
    {
        float finalDistance = 0;
        Vector3 lastObjectPosition = _objects[0].transform.position;

        for(int i = 0; i < _objects.Length; i++)
        {
            finalDistance = Vector3.Distance(lastObjectPosition, _objects[i].transform.position); // ������� ��������� ����� ������������ ���������
            lastObjectPosition = _objects[i].transform.position; // ������� �������� �������
        }

        return finalDistance; // ���������� �������� ���������
    }
}
