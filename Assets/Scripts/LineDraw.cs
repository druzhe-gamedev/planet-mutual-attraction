using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{

    [SerializeField] private LineRenderer _lineRenderer; // �����

    [SerializeField] private Transform _targetGameobject; // ������, �� �������� �������� �����

    void Update()
    {
        _lineRenderer.SetPosition(0, transform.position); // ��������� ������� �����
        _lineRenderer.SetPosition(1, _targetGameobject.position); // �������� ������� �����
    }
}
