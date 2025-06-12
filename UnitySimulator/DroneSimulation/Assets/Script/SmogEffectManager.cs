using UnityEngine;

public class SmogEffectManager : MonoBehaviour
{
    public GameObject smog; // Unity Inspector���� Smog �������� ���⿡ �����մϴ�.

    // SmogEffectManager�� �ν��Ͻ��� ��𼭵� ���� ������ �� �ֵ��� �̱��� ���� ���
    public static SmogEffectManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ �ı����� �ʰ� �Ϸ��� �ּ� ����
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// ������ ��ġ�� ����� ����Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="position">����װ� ������ ���� ��ǥ</param>
    /// <param name="rotation">����װ� ������ ���� ȸ�� ��</param>
    public void CreateSmog(Vector3 position, Quaternion rotation)
    {
        if (smog != null)
        {
            Instantiate(smog, position, rotation);
            Debug.Log($"Smog created at: {position}");
        }
       
    }
}