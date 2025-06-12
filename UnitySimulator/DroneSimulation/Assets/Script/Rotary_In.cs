using UnityEngine;

public class Rotary_In : MonoBehaviour
{
    private float rotationAngle = 60f; // �� �� ȸ���� ����
    private float rotationDuration = 0.5f; // ȸ���� �ɸ��� �ð�

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float elapsedTime = 0f;
    private bool isRotating = false;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            if (t >= 1f)
            {
                isRotating = false;
                transform.rotation = targetRotation;
            }
        }
    }

    public void OnM6() // ������ ��ư Ŭ�� �� ȣ��
    {
        if (!isRotating)
        {
            initialRotation = transform.rotation;
            targetRotation = initialRotation * Quaternion.Euler(rotationAngle, 0, 0);
            elapsedTime = 0f;
            isRotating = true;
        }
    }

    public void OnM4() // ���� ��ư Ŭ�� �� ȣ��
    {
        if (!isRotating)
        {
            initialRotation = transform.rotation;
            targetRotation = initialRotation * Quaternion.Euler(-rotationAngle, 0, 0);
            elapsedTime = 0f;
            isRotating = true;
        }
    }
}