using UnityEngine;

public class PipeHolders : MonoBehaviour
{
    // ����Ƽ �����Ϳ��� �巡�� �� ������� ������ GameObject ������
    public GameObject PipeHolder1;
    public GameObject PipeHolder2;
    public GameObject PipeHolder3;
    public GameObject PipeHolder4;

    public Screw screwControl;

    private float MoveTime = 5f; // ��� ������ Ȧ���� �̵��ϴ� �� �ɸ��� �ð� (��)

    // �� ������ Ȧ���� Z������ �̵��� �� (���� ���� Z�� ���̳ʽ� �������� �̵�)
    private float MoveAmount1 = 1.0f;
    private float MoveAmount2 = 1.0f;
    private float MoveAmount3 = 1.0f;
    private float MoveAmount4 = 1.0f;

    // �� ������ Ȧ���� ���� ��ġ�� ��ǥ ��ġ ������
    private Vector3 PH1StartPosition;   // PipeHolder1
    private Vector3 PH1TargetPosition;  // PipeHolder1

    private Vector3 PH2StartPosition;   // PipeHolder2
    private Vector3 PH2TargetPosition;  // PipeHolder2

    private Vector3 PH3StartPosition;   // PipeHolder3
    private Vector3 PH3TargetPosition;  // PipeHolder3

    private Vector3 PH4StartPosition;   // PipeHolder4
    private Vector3 PH4TargetPosition;  // PipeHolder4

    private float elapsedTime = 0f;
    private bool isMoving = true; // �̵��� ���� ������ ����

    private bool isPipeHoldersCW = false;
    private bool isPipeHoldersCCW = false;

    void Start()
    {
        // �� PipeHolder GameObject�� ���� ��ġ�� ���� ��ġ�� ����
        // �׸��� �� ��ġ���� MoveAmount��ŭ ���� ��ġ�� ��ǥ ��ġ�� ����

        // PipeHolder1 ����
        PH1StartPosition = PipeHolder1.transform.position;
        PH1TargetPosition = PH1StartPosition + new Vector3(0, 0, -MoveAmount1);
        // PipeHolder2 ����
        PH2StartPosition = PipeHolder2.transform.position;
        PH2TargetPosition = PH2StartPosition + new Vector3(0, 0, MoveAmount2);
        // PipeHolder3 ����
        PH3StartPosition = PipeHolder3.transform.position;
        PH3TargetPosition = PH3StartPosition + new Vector3(MoveAmount3, 0, 0);
        // PipeHolder4 ����
        PH4StartPosition = PipeHolder4.transform.position;
        PH4TargetPosition = PH4StartPosition + new Vector3(-MoveAmount4, 0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPipeHoldersCW)
        {
            if(!isPipeHoldersCCW)
            {

                if (isMoving && screwControl != null)
                {
                    screwControl.ActivateScrew(); // ���� ȸ�� ����
                }

                // �ð� ����
                elapsedTime += Time.deltaTime;

                // 0~1 ���̷� ���� ���� ���
                float t = Mathf.Clamp01(elapsedTime / MoveTime);

                // �� PipeHolder�� ��ġ�� ����
                if (PipeHolder1 != null)
                {
                    PipeHolder1.transform.position = Vector3.Lerp(PH1StartPosition, PH1TargetPosition, t);
                }
                if (PipeHolder2 != null)
                {
                    PipeHolder2.transform.position = Vector3.Lerp(PH2StartPosition, PH2TargetPosition, t);
                }
                if (PipeHolder3 != null)
                {
                    PipeHolder3.transform.position = Vector3.Lerp(PH3StartPosition, PH3TargetPosition, t);
                }
                if (PipeHolder4 != null)
                {
                    PipeHolder4.transform.position = Vector3.Lerp(PH4StartPosition, PH4TargetPosition, t);
                }

                // �̵��� �Ϸ�Ǿ����� ���߱�
                if (t >= 1f)
                {
                    isPipeHoldersCW = false;
                    elapsedTime = 0f;
                    // �ʿ��ϴٸ� �� ��ũ��Ʈ�� ��Ȱ��ȭ�ϰų�, �߰� ������ ���� �� �ֽ��ϴ�.
                    // this.enabled = false; 
                    if (screwControl != null)
                    {
                        screwControl.DeactivateScrew(); // ���� ȸ�� ����
                    }

                }
            }
            
            
        }

        if (isPipeHoldersCCW)
        {
            if(!isPipeHoldersCW)
            {

                if (isMoving && screwControl != null)
                {
                    screwControl.ActivateScrew(); // ���� ȸ�� ����
                }

                // �ð� ����
                elapsedTime += Time.deltaTime;

                // 0~1 ���̷� ���� ���� ���
                float t = Mathf.Clamp01(elapsedTime / MoveTime);

                // �� PipeHolder�� ��ġ�� ����
                if (PipeHolder1 != null)
                {
                    PipeHolder1.transform.position = Vector3.Lerp(PH1TargetPosition, PH1StartPosition, t);
                }
                if (PipeHolder2 != null)
                {
                    PipeHolder2.transform.position = Vector3.Lerp(PH2TargetPosition, PH2StartPosition, t);
                }
                if (PipeHolder3 != null)
                {
                    PipeHolder3.transform.position = Vector3.Lerp(PH3TargetPosition, PH3StartPosition, t);
                }
                if (PipeHolder4 != null)
                {
                    PipeHolder4.transform.position = Vector3.Lerp(PH4TargetPosition, PH4StartPosition, t);
                }

                // �̵��� �Ϸ�Ǿ����� ���߱�
                if (t >= 1f)
                {
                    isPipeHoldersCCW = false;
                    elapsedTime = 0f;
                    // �ʿ��ϴٸ� �� ��ũ��Ʈ�� ��Ȱ��ȭ�ϰų�, �߰� ������ ���� �� �ֽ��ϴ�.
                    // this.enabled = false; 
                    if (screwControl != null)
                    {
                        screwControl.DeactivateScrew(); // ���� ȸ�� ����
                    }

                }
            }
            
            
        }
    }

    public void ActivatePipeHoldersCW()
    {
        isPipeHoldersCW = true;
    }

    public void DeactivatePipeHoldersCW()
    {
        isPipeHoldersCW = false;
    }

    public void ActivatePipeHoldersCCW()
    {
        isPipeHoldersCCW = true;
    }

    public void DeactivatePipeHoldersCCW()
    {
        isPipeHoldersCCW = false;
    }
}