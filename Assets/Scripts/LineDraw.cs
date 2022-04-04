using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{

    [SerializeField] private LineRenderer _lineRenderer; // линия

    [SerializeField] private Transform _targetGameobject; // объект, до которого проводим линию

    void Update()
    {
        _lineRenderer.SetPosition(0, transform.position); // начальная позиция линии
        _lineRenderer.SetPosition(1, _targetGameobject.position); // конечная позиция линии
    }
}
