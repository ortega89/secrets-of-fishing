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
        // При помощи линейной интерполяции вычисляем новое положение камеры как точку
        // на отрезке между ее текущим положением и положением цели, к которой камера должна стремиться.
        // Мы умножаем фактор на прошедшее время, чтобы его влияние было менее существенным.
        // Фактор задает положение новой точки на отрезке:
        // * 0 - исходное положению камеры,
        // * 1 - цель,
        // * 0,5 - посередине между камерой и целью, и так далее.

        Vector2 newPosition = Vector2.Lerp(this.transform.position,
                cameraTarget.transform.position, interpolationFactor * Time.fixedDeltaTime);
        this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
    }
}
