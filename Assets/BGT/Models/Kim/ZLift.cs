using UnityEngine;

public class ZLift : MonoBehaviour
{
    // ����Ƽ �����Ϳ��� �巡�� �� ������� ������ GameObject ������
    public GameObject LiftWeight;
    public GameObject CarriageFrame;
   
    public ZLiftRotation ROT;
    public ChainMove CHM;
    public ChainMove CHM1;

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

    private bool isZLiftCW = false;
    private bool isZLiftCCW = false;

    void Start()
    {
    }

    // Update is called once per frame
    private void InitializeZLiftCW()
    {
        // �̹� �ٸ� �������� �����̰ų� ���� �������� �����̰� �ִٸ� ��������� ����
        if (isZLiftCW || isZLiftCCW) return;

        isZLiftCW = true;
        elapsedTime = 0f; // ���⼭�� �ʱ�ȭ!

        LWStartPosition = LiftWeight.transform.position;
        LWTargetPosition = LWStartPosition + new Vector3(0, -MoveAmount1, 0);

        CFStartPosition = CarriageFrame.transform.position;
        CFTargetPosition = CFStartPosition + new Vector3(0, MoveAmount2, 0);

        ROT.ActivateZLiftRotationCW();
        CHM.ActiveChainCW();
        CHM1.ActiveChainCW();
    }

    private void InitializeZLiftCCW() // �̸��� private�� �����Ͽ� Update���� ���� ȣ����� �ʰ� �մϴ�.
    {
        // �̹� �ٸ� �������� �����̰ų� ���� �������� �����̰� �ִٸ� ��������� ����
        if (isZLiftCW || isZLiftCCW) return;

        isZLiftCCW = true;
        elapsedTime = 0f; // ���⼭�� �ʱ�ȭ!

        LWStartPosition = LiftWeight.transform.position;
        LWTargetPosition = LWStartPosition + new Vector3(0, MoveAmount1, 0); 

        CFStartPosition = CarriageFrame.transform.position;
        CFTargetPosition = CFStartPosition + new Vector3(0, -MoveAmount2, 0);

        ROT.ActivateZLiftRotationCCW();
        CHM.ActiveChainCCW();
        CHM1.ActiveChainCCW();
    }

    // Update�� �� ������ ����� �̵� ������ ���
    void Update()
    {
        if (isZLiftCW && !isZLiftCCW)
        {
            elapsedTime += Time.deltaTime; // �ð� ����
            float t = Mathf.Clamp01(elapsedTime / MoveTime);

            if (LiftWeight != null)
            {
                LiftWeight.transform.position = Vector3.Lerp(LWStartPosition, LWTargetPosition, t);
            }
            if (CarriageFrame != null)
            {
                CarriageFrame.transform.position = Vector3.Lerp(CFStartPosition, CFTargetPosition, t);
            }
            bool reachedLWTarget = Vector3.Distance(LiftWeight.transform.position, LWTargetPosition) < 0.01f; // ��� ����
            bool reachedCFTarget = Vector3.Distance(CarriageFrame.transform.position, CFTargetPosition) < 0.01f; // ��� ����
            if (t >= 1f || (reachedLWTarget && reachedCFTarget))
            {
                isZLiftCW = false;
                elapsedTime = 0f; // ���� �̵��� ���� �ʱ�ȭ
                ROT.DeactivateZLiftRotationCW();
                CHM.DeActiveChainCW();
                CHM1.DeActiveChainCW();
            }
        }
        else if (isZLiftCCW && !isZLiftCW)
        {
            elapsedTime += Time.deltaTime; // �ð� ����
            float t = Mathf.Clamp01(elapsedTime / MoveTime);

            if (LiftWeight != null)
            {
                LiftWeight.transform.position = Vector3.Lerp(LWStartPosition, LWTargetPosition, t);
            }
            if (CarriageFrame != null)
            {
                CarriageFrame.transform.position = Vector3.Lerp(CFStartPosition, CFTargetPosition, t);
            }
            bool reachedLWTarget = Vector3.Distance(LiftWeight.transform.position, LWTargetPosition) < 0.01f;
            bool reachedCFTarget = Vector3.Distance(CarriageFrame.transform.position, CFTargetPosition) < 0.01f;

            
            if (t >= 1f || (reachedLWTarget && reachedCFTarget)) 
            {
                isZLiftCCW = false;
                elapsedTime = 0f;
                ROT.DeactivateZLiftRotationCCW();
                CHM.DeActiveChainCCW();
                CHM1.DeActiveChainCCW();
            }
        }
    }
    public void ActivateZLiftUp()
    {
        InitializeZLiftCW(); // �� �Լ��� ȣ��� �� �̵� �ʱ�ȭ �� ����
    }

    public void DeactivateZLiftUp()
    {
        isZLiftCW = false;
        ROT.DeactivateZLiftRotationCW();
        CHM.DeActiveChainCW();
        CHM1.DeActiveChainCW();
    }

    public void ActivateZLiftDown()
    {
        InitializeZLiftCCW(); // �� �Լ��� ȣ��� �� �̵� �ʱ�ȭ �� ����
    }

    public void DeactivateZLiftDown()
    {
        isZLiftCCW = false;
        ROT.DeactivateZLiftRotationCCW();
        CHM.DeActiveChainCCW();
        CHM1.DeActiveChainCCW();
    }
}