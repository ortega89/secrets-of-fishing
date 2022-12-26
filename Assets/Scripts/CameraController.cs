using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTarget;
    public float interpolationFactor;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // ��� ������ �������� ������������ ��������� ����� ��������� ������ ��� �����
        // �� ������� ����� �� ������� ���������� � ���������� ����, � ������� ������ ������ ����������.
        // �� �������� ������ �� ��������� �����, ����� ��� ������� ���� ����� ������������.
        // ������ ������ ��������� ����� ����� �� �������:
        // * 0 - �������� ��������� ������,
        // * 1 - ����,
        // * 0,5 - ���������� ����� ������� � �����, � ��� �����.

        Vector2 newPosition = Vector2.Lerp(this.transform.position,
                cameraTarget.transform.position, interpolationFactor * Time.fixedDeltaTime);
        this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
    }
}
