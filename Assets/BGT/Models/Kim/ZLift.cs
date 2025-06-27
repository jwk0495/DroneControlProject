using UnityEngine;

public class ZLift : MonoBehaviour
{
    // ����Ƽ �����Ϳ��� �巡�� �� ������� ������ GameObject ������
    public GameObject LiftWeight;
    public GameObject CarriageFrame;
   
    public ZLiftRotation ROT;
    public ChainMove CHM;

    private float MoveTime = 5f; // ��� ������ Ȧ���� �̵��ϴ� �� �ɸ��� �ð� (��)

    // �� ������ Ȧ���� Z������ �̵��� �� (���� ���� Z�� ���̳ʽ� �������� �̵�)
    private float MoveAmount1 = 1.0f;
    private float MoveAmount2 = 1.0f;
    

    // �� Lift ���� ��ġ�� ��ǥ ��ġ ������
    private Vector3 LWStartPosition;   // LiftWeight
    private Vector3 LWTargetPosition;  // LiftWeight

    private Vector3 CFStartPosition;   // CarriageFrame
    private Vector3 CFTargetPosition;  // CarriageFrame



    private float elapsedTime = 0f;
    private bool isMoving = true; // �̵��� ���� ������ ����

    private bool isZLiftCW = false;
    private bool isZLiftCCW = false;

    void Start()
    {
        // �� PipeHolder GameObject�� ���� ��ġ�� ���� ��ġ�� ����
        // �׸��� �� ��ġ���� MoveAmount��ŭ ���� ��ġ�� ��ǥ ��ġ�� ����

        // LiftWeight ����
        LWStartPosition = LiftWeight.transform.position;
        LWTargetPosition = LWStartPosition + new Vector3(0, -MoveAmount1, 0);
        // CarriageFrame ����
        CFStartPosition = CarriageFrame.transform.position;
        CFTargetPosition = CFStartPosition + new Vector3(0, MoveAmount2, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isZLiftCW && !isZLiftCCW)
        {
           
            ROT.ActivateZLiftRotationCW();
            CHM.ActiveChainCW();
            // �ð� ����
            elapsedTime += Time.deltaTime;

            // 0~1 ���̷� ���� ���� ���
            float t = Mathf.Clamp01(elapsedTime / MoveTime);

            // �� PipeHolder�� ��ġ�� ����
            if (LiftWeight != null)
            {
                LiftWeight.transform.position = Vector3.Lerp(LWStartPosition, LWTargetPosition, t);
            }
            if (CarriageFrame != null)
            {
                CarriageFrame.transform.position = Vector3.Lerp(CFStartPosition, CFTargetPosition, t);
            }
            

            // �̵��� �Ϸ�Ǿ����� ���߱�
            if (t >= 1f)
            {
                isZLiftCW = false;
                elapsedTime = 0f;
                ROT.DeactivateZLiftRotationCW(); // ���� ȸ�� ����
                CHM.DeActiveChainCW();
            }
        }

        if (!isZLiftCW && isZLiftCCW)
        {
            ROT.ActivateZLiftRotationCCW(); // ���� ȸ�� ����
            CHM.ActiveChainCCW();
            // �ð� ����
            elapsedTime += Time.deltaTime;

            // 0~1 ���̷� ���� ���� ���
            float t = Mathf.Clamp01(elapsedTime / MoveTime);

            // �� PipeHolder�� ��ġ�� ����
            if (LiftWeight != null)
            {
                LiftWeight.transform.position = Vector3.Lerp(LWTargetPosition, LWStartPosition, t);
            }
            if (CarriageFrame != null)
            {
                CarriageFrame.transform.position = Vector3.Lerp(CFTargetPosition, CFStartPosition, t);
            }
            

            // �̵��� �Ϸ�Ǿ����� ���߱�
            if (t >= 1f)
            {
                isZLiftCCW = false;
                elapsedTime = 0f;
                ROT.DeactivateZLiftRotationCCW(); // ���� ȸ�� ����
                CHM.DeActiveChainCCW();
            }
        }
    }

    public void ActivateZLiftUp()
    {
        isZLiftCW = true;
    }

    public void DeactivateZLiftUp()
    {
        isZLiftCW = false;
        ROT.DeactivateZLiftRotationCW(); // ���� ȸ�� ����
        CHM.DeActiveChainCW();
    }

    public void ActivateZLiftDown()
    {
        isZLiftCCW = true;
    }

    public void DeactivateZLiftDown()
    {
        isZLiftCCW = false;
        ROT.DeactivateZLiftRotationCCW(); // ���� ȸ�� ����
        CHM.DeActiveChainCCW();
    }
}