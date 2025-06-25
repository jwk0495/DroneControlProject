using UnityEngine;

public class Screw : MonoBehaviour
{
    public float rotationSpeed = 250f;

    public GameObject Screw1;
    public GameObject Screw2;
    public GameObject Screw3;
    public GameObject Screw4;


    private bool isScrew = false;
    // Update is called once per frame
    void Update()
    {
        if(isScrew)
        {
            // �� ���� ������Ʈ�� Y���� �߽����� 'rotationSpeed' ��ŭ ȸ����ŵ�ϴ�.
            // Time.deltaTime�� ���Ͽ� ������ �ӵ��� �������� ȸ���� �����մϴ�.
            // Space.Self�� ������Ʈ �ڽ��� ���� Y���� �������� ȸ����ŵ�ϴ�.
            // Space.World�� ���� ��ǥ���� Y���� �������� ȸ����ŵ�ϴ�.
            Screw1.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
            Screw2.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
            Screw3.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
            Screw4.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);

        }

        
    }

    public void ActivateScrew()
    {
        isScrew = true;
    }

    public void DeactivateScrew()
    {
        isScrew = false;
    }

}
