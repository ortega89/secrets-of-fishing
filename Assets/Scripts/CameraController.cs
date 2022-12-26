using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTarget;
    public float interpolationFactor;
    public GameObject limitingEnvironment;
    private Rect cameraLimits;
    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();

        if (limitingEnvironment != null)
        {
            SetupCameraLimits();
        }
    }

    private void SetupCameraLimits()
    {
        // ������� ������������� �������������� ����� (��������, ����)
        // � �������� ��� �� ���� ������ �� ������� ������.
        // � ������ ������ ���� ������ ������ ���������� ������ ����������� ��������������.
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        RectTransform env = limitingEnvironment.GetComponent<RectTransform>();
        Rect envRect = env.GetWorldRect();
        cameraLimits = new Rect(
                envRect.x + cameraWidth / 2, envRect.y + cameraHeight / 2,
                envRect.width - cameraWidth, envRect.height - cameraHeight);
        Debug.Log("Env rect: " + envRect);
        Debug.Log("Camera size: " + cameraWidth + " x " + cameraHeight + ", Camera limits: " + cameraLimits);
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

        // ���� ����������� ����������� �� ����������� ������
        if (limitingEnvironment != null)
        {
            // �������� ���������� ������ ��������� ������ � ����� ��������������� ��������������
            float x = Math.Max(cameraLimits.xMin, Math.Min(cameraLimits.xMax, newPosition.x));
            float y = Math.Max(cameraLimits.yMin, Math.Min(cameraLimits.yMax, newPosition.y));
            Vector2 newerPosition = new Vector2(x, y);

            //Debug.Log("Camera would move to " + newPosition + ", clamped " + newerPosition);
            newPosition = newerPosition;
        }

        this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
    }
}

public static class RectTransformExtensions
{
    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        // Get the bottom left corner.
        Vector3 position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        return new Rect(position, size);
    }
}