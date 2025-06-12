using UnityEngine;

public class Leg : MonoBehaviour
{
    public GameObject Leg2;

    private float rotationDuration = 5f;
    private float rotationAmount1 = 40f; // Leg�� ���� ȸ����
    private float rotationAmount2 = -40f; // Leg2�� ���� ȸ���� (Leg�� �����)
    private float elapsedTime = 0f;
    private bool isRotating = true;

    private Quaternion initialRotationLeg1; // Leg�� �ʱ� ���� ȸ��
    private Quaternion initialRotationLeg2; // Leg2�� �ʱ� ���� ȸ��

    void Start()
    {
        // �� ������Ʈ�� �ʱ� ���� ȸ���� ����
        initialRotationLeg1 = transform.rotation;
        initialRotationLeg2 = Leg2.transform.rotation;
    }

    void Update()
    {
        if (isRotating)
        {
            // �ð� ����
            elapsedTime += Time.deltaTime;

            // 0~1 ���̷� ���� ���� ���
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);

            // Leg�� ȸ�� ��� (�ʱ� ȸ������ rotationAmount1��ŭ Z������ ȸ��)
            Quaternion targetRotationLeg1 = initialRotationLeg1 * Quaternion.Euler(0, 0, rotationAmount1);
            transform.rotation = Quaternion.Slerp(initialRotationLeg1, targetRotationLeg1, t);

            // Leg2�� ȸ�� ��� (Leg�� ȸ���� ��������� rotationAmount2��ŭ Z������ ȸ��)
            // Leg2�� �ʱ� ���� ȸ���� �������� rotationAmount2��ŭ Z������ ȸ���ϴ� ��Ÿ ȸ���� ����
            Quaternion deltaRotationLeg2 = Quaternion.Euler(0, 0, rotationAmount2);
            // Leg2�� �θ�(Leg)�� ���� ȸ���� Leg2�� �ʱ� ���� ȸ���� ��Ÿ ȸ���� ���Ͽ� ���� ���� ȸ�� ���
            // �� �κ��� Leg2�� Leg�� �ڽ��� ��� Leg2�� ���� ȸ���� �������� ����ؾ� �մϴ�.
            // ���⼭�� Leg2�� ���� ȸ���� ���� �����ϵ�, Leg1�� ȸ������ ���������� �����մϴ�.
            // ���� Leg2�� Leg�� �ڽ��̰� Leg�� ȸ���� ���� ȸ���ϴ� ���� ��ǥ��� ���� ���� �޶����� �մϴ�.
            // ���� �ڵ忡���� Leg1�� Leg2�� ���� ���������� ��ǥ Z�� ȸ������ �����˴ϴ�.
            Quaternion targetRotationLeg2 = initialRotationLeg2 * Quaternion.Euler(0, 0, rotationAmount2);
            Leg2.transform.rotation = Quaternion.Slerp(initialRotationLeg2, targetRotationLeg2, t);


            
            if (t >= 1f)
            {
                isRotating = false;
            }
        }
    }
}