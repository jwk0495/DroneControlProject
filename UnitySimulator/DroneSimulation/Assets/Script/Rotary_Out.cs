using UnityEngine;

public class Rotary_Out : MonoBehaviour
{
    private float rotationLeftAngle = -30f; // �� �� ȸ���� ����
    private float rotationRightAngle = 90f;
    private float rotationDuration = 0.5f; // ȸ���� �ɸ��� �ð�

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public float CurrentLocalRotationX { get; private set; }

    private float elapsedTime = 0f;
    private bool isRotating = false;

    // ���� ���� X�� ȸ�� ������ �ܺο��� ���� �� �ֵ��� Public �Ӽ� �߰�
    public float LocalRotationX { get; private set; }

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
        CurrentLocalRotationX = WrapAngle(transform.localEulerAngles.x); // �ʱ� ���� ������Ʈ
    }

    void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            CurrentLocalRotationX = WrapAngle(transform.localEulerAngles.x); // ȸ�� �� ���� ������Ʈ

            if (t >= 1f)
            {
                isRotating = false;
                transform.rotation = targetRotation;
                CurrentLocalRotationX = WrapAngle(transform.localEulerAngles.x); // ���� ���� ������Ʈ
            }
        }
        else
        {
            // ȸ�� ���� �ƴ� ���� ���� ������Ʈ (��Ȯ�� ���� ������ ����)
            CurrentLocalRotationX = WrapAngle(transform.localEulerAngles.x);
        }
    }

    public void OnRightB() // ������ ��ư Ŭ�� �� ȣ��
    {
        if (!isRotating)
        {
            initialRotation = transform.rotation;
            targetRotation = initialRotation * Quaternion.Euler(rotationRightAngle, 0, 0);
            elapsedTime = 0f;
            isRotating = true;
        }
    }

    public void OnLeftB() // ���� ��ư Ŭ�� �� ȣ��
    {
        if (!isRotating)
        {
            initialRotation = transform.rotation;
            targetRotation = initialRotation * Quaternion.Euler(rotationLeftAngle, 0, 0);
            elapsedTime = 0f;
            isRotating = true;
        }
    }

    // ���Ϸ� ������ -180 ~ 180 ������ �����ϴ� ���� �Լ�
    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }
}