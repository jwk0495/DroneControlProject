using UnityEngine;
using System.Collections.Generic;

public class FirebombDropper : MonoBehaviour
{
   
    public Rotary_Out rotaryOutScript;
    public GameObject[] firebombs;
    public float[] DropAngles;

    private bool[] hasBombDropped;
    private int nextBombIndexToDrop = 0;
    private bool isTargetAngle = false;
    private float prvRotaryOutXAngle; // previousRotaryOutXAngle


    void Start()
    {
        hasBombDropped = new bool[firebombs.Length];
        isTargetAngle = false;

        prvRotaryOutXAngle = rotaryOutScript.CurrentLocalRotationX;

        // FirebombDropper������ Rigidbody/Collider �ʱ� ������ ���� �ʽ��ϴ�.
        // �� �κ��� �� Firebomb.cs ��ũ��Ʈ�� Awake���� ����մϴ�.
    }

    void Update()
    {
        if (rotaryOutScript == null) return;
        if (nextBombIndexToDrop >= firebombs.Length) return;

        float RotaryOutXAngle = rotaryOutScript.CurrentLocalRotationX;
        float angleDelta = WrapAngle(RotaryOutXAngle - prvRotaryOutXAngle);

        float targetAbsoluteAngle = DropAngles[nextBombIndexToDrop];
        float angleDifference = Mathf.Abs(RotaryOutXAngle - targetAbsoluteAngle);
        bool currentAngleMeetsCondition = (angleDifference <= 5f);

        // ����: ��ǥ ���� ������ '����'���� �� AND '�������� ȸ�� ���� ��'
        if (currentAngleMeetsCondition && !isTargetAngle && angleDelta < -0.1f) // �������� ȸ���ϴ� ���ȸ� ����
        {
            isTargetAngle = true;

            if (!hasBombDropped[nextBombIndexToDrop])
            {
                DropFirebomb(nextBombIndexToDrop); // bomb ����߸� �Լ� ȣ��
                hasBombDropped[nextBombIndexToDrop] = true;
                nextBombIndexToDrop++;
            }
        }
        else if (!currentAngleMeetsCondition && isTargetAngle)
        {
            isTargetAngle = false;
        }

        prvRotaryOutXAngle = RotaryOutXAngle;
    }

    // Ư�� �ε����� Firebomb�� ����߸��� �Լ�
    public void DropFirebomb(int index)
    {
        if (index < 0 || index >= firebombs.Length) return;
        if (hasBombDropped[index]) return;

        GameObject bomb = firebombs[index];
        if (bomb == null) return;

        bomb.transform.SetParent(null); // �θ� ���� ���� (���������� ������Ű�� ����)

        // Firebomb�� �پ��ִ� Firebomb.cs ��ũ��Ʈ�� ������ ���� Ȱ��ȭ �� �Ҹ� �����ٸ� �Լ��� ȣ��
        Firebomb firebombScript = bomb.GetComponent<Firebomb>();
        if (firebombScript != null)
        {
            firebombScript.Destruction(); // �Լ��� ȣ��
           
        }
        else
        {
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
            }
        }
    }

    // ���Ϸ� ������ -180 ~ 180 ������ �����ϴ� ���� �Լ�
    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        if (angle < -180)
            return angle + 360;
        return angle;
    }
}