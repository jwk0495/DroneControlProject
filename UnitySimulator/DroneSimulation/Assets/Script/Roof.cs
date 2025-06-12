using UnityEngine;

public class Roof : MonoBehaviour
{
    public GameObject Roof1;
    public GameObject Roof2;

    private float moveDuration = 5f; // �̵��� �ɸ��� �ð�
    private float roof1MoveAmount = -2f; // Roof1�� �̵��� �Ÿ� 
    private float roof2MoveAmount = -1f; // Roof2�� �̵��� �Ÿ� 

    private Vector3 roof1StartPosition;
    private Vector3 roof1TargetPosition;
    private Vector3 roof2StartPosition;
    private Vector3 roof2TargetPosition;

    private float elapsedTime = 0f;
    private bool isMoving = true; // �̵��� ���� ������ ����

    void Start()
    {
       
        // �� ������Ʈ�� �ʱ� ��ġ ����
        roof1StartPosition = Roof1.transform.position;
        roof2StartPosition = Roof2.transform.position;

        // �� ������Ʈ�� ��ǥ ��ġ ���
        // ���⼭�� Y������ �̵��Ѵٰ� �����մϴ�.
        roof1TargetPosition = roof1StartPosition + new Vector3(0, 0, roof1MoveAmount);
        roof2TargetPosition = roof2StartPosition + new Vector3(0, 0, roof2MoveAmount);
    }

    void Update()
    {
        if (isMoving)
        {
            // �ð� ����
            elapsedTime += Time.deltaTime;

            // 0~1 ���̷� ���� ���� ���
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            // Roof1 �̵� ����
            Roof1.transform.position = Vector3.Lerp(roof1StartPosition, roof1TargetPosition, t);

            // Roof2 �̵� ����
            Roof2.transform.position = Vector3.Lerp(roof2StartPosition, roof2TargetPosition, t);

            // �̵��� �Ϸ�Ǿ����� ���߱�
            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}