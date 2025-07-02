/*
--- �ٸ� ��ũ��Ʈ������ ��� ���� (��: DroneController.cs) ---

// 1. DroneController�� Ư�� ������ �������� �� (��: �ӹ� �Ϸ� �� �����̼� ����)
void OnLandingCompleteAtStation()
{
    // WildfireManager�� ��ȭ ���� ��������Ʈ�� true�� ��ȯ�ϴ� �Լ�(�Ǵ� ���ٽ�)�� �Ҵ��մϴ�.
    // �̷��� �ϸ� ���� Update �����ӿ��� WildfireManager�� ���� ���� �˴ϴ�.
    if (WildfireManager.Instance != null)
    {
        Debug.Log("��ȭ ���� ����! WildfireManager�� ���� ��ȣ�� �����ϴ�.");
        WildfireManager.Instance.ExtinguishConditionCheck = () => true;
    }
}

// 2. �ٸ� ��ũ��Ʈ���� ȭ�� �߻��� ���� Ʈ�����ϰ� ���� ��
void StartSomeEvent()
{
    if (WildfireManager.Instance != null)
    {
        WildfireManager.Instance.GenerateFires();
    }
}
*/

using UnityEngine;
using System.Collections.Generic;
using System; // Action, Func ����� ���� �߰�

public class WildfireManager : MonoBehaviour
{
    // --- �̱��� �ν��Ͻ� ---
    // �ٸ� ��ũ��Ʈ���� WildfireManager.Instance �� ���� ������ �� �ֵ��� �մϴ�.
    public static WildfireManager Instance { get; private set; }


    [Header("ȭ�� ����")]
    [Tooltip("ȭ�� ��ƼŬ�� ������ Terrain Object")]
    public Terrain targetTerrain;
    [Tooltip("ȭ�� ȿ���� Particle System Object")]
    public GameObject fireParticlePrefab;
    [Tooltip("�̸� ������ �� ȭ�� ��ƼŬ�� �ִ� �����Դϴ� (������Ʈ Ǯ ũ��).")]
    public int poolSize = 20;
    [Tooltip("�� ���� ������ ȭ���� �����Դϴ�.")]
    public int numberOfFiresToSpawn = 10;
    
    [Header("ȭ�� �߻� ���� ����")]
    [Tooltip("ȭ�簡 �߻��� ������ �߽� ��ǥ(���� ��ǥ).")]
    public Vector3 spawnCenter = new Vector3(500, 0, 500);
    [Tooltip("ȭ�簡 �߻��� ������ ����, ���� ũ��")]
    public Vector2 spawnAreaSize = new Vector2(1000, 1000);

    [Header("ȭ�� �߻� ����")]
    [Tooltip("�����Ϳ��� �� ���� üũ�ϸ� ȭ�簡 ��� �߻� (�׽�Ʈ��)")]
    public bool generateFireNow = false;

    // --- ���� ��������Ʈ �� �̺�Ʈ ---
    [Tooltip("�ٸ� ��ũ��Ʈ���� �� ��������Ʈ�� 'true'�� ��ȯ�ϴ� �Լ��� �Ҵ��ϸ� ȭ�簡 ���е�")]
    public Func<bool> ExtinguishConditionCheck; // ȭ�� ���� ������ üũ�� ��������Ʈ


    // --- ���� ���� ---
    private List<GameObject> fireParticlePool; // �̸� ������ ��ƼŬ ������Ʈ�� ��Ƶδ� Ǯ(Pool)
    private List<GameObject> activeFires;      // ���� Ȱ��ȭ��(��������) ȭ�� ������Ʈ ���
    private bool hasFireBeenGenerated = false;

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        
        else
            Instance = this;
    }

    void Start()
    {
        InitializeObjectPool(); // ���� ���� �� ������Ʈ Ǯ �ʱ�ȭ
    }

    /// <summary>
    /// ������ ũ��(poolSize)��ŭ ȭ�� ��ƼŬ ������Ʈ�� �̸� �����Ͽ� Ǯ�� �־��
    /// </summary>
    void InitializeObjectPool()
    {
        fireParticlePool = new List<GameObject>();
        activeFires = new List<GameObject>();

        if (fireParticlePrefab == null)
        {
            Debug.LogError("Fire Particle Prefab�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject fireInstance = Instantiate(fireParticlePrefab, Vector3.zero, Quaternion.identity, this.transform);
            fireInstance.SetActive(false); // ��Ȱ��ȭ ���·� ����
            fireParticlePool.Add(fireInstance);
        }
    }


    void Update()
    {
        // --- 1. ȭ�� �߻� ���� ---
        if (generateFireNow && !hasFireBeenGenerated)
        {
            GenerateFires();
            generateFireNow = false;
        }

        // --- 2. ȭ�� ���� ���� (��������Ʈ ȣ��) ---
        // ExtinguishConditionCheck ��������Ʈ�� �Լ��� �Ҵ�Ǿ� �ְ�, �� �Լ��� ���� ����� true�̸� ȭ�縦 �����մϴ�.
        if (ExtinguishConditionCheck != null && ExtinguishConditionCheck())
        {
            Debug.Log("��ȭ ������ �����Ͽ� ��� ���� ���ϴ�.");
            ExtinguishAllFires();
            ExtinguishConditionCheck = null; // ������ �� �� �����ϸ� ��������Ʈ�� �ʱ�ȭ�Ͽ� �ݺ� ���� ����
        }
    }

    /// <summary>
    /// ������ ������ ȭ�縦 �����ϴ� �Լ��Դϴ�. (������Ʈ Ǯ�� ���)
    /// </summary>
    public void GenerateFires()
    {
        if (hasFireBeenGenerated)
        {
            Debug.LogWarning("ȭ�簡 �̹� �߻��߽��ϴ�. ���� ȭ�縦 ���� �����ϼ���.");
            return;
        }
        if (!targetTerrain)
        {
            Debug.LogError("Terrain�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }
        if (numberOfFiresToSpawn > poolSize)
            Debug.LogWarning($"�����Ϸ��� ȭ�� ����({numberOfFiresToSpawn})�� Ǯ ũ��({poolSize})���� Ů�ϴ�. Ǯ ũ�⸦ �ø��� ���� �����մϴ�.");

        Debug.Log($"������ ������ {numberOfFiresToSpawn}���� ȭ�縦 �����մϴ�...");

        Vector3 areaStartCorner = spawnCenter - new Vector3(spawnAreaSize.x / 2, 0, spawnAreaSize.y / 2);

        for (int i = 0; i < numberOfFiresToSpawn; i++)
        {
            GameObject fireInstance = GetPooledFireObject(); // Ǯ���� ��Ȱ��ȭ�� ������Ʈ ��������
            if (!fireInstance)
            {
                Debug.LogWarning("��� ������ ȭ�� ��ƼŬ�� Ǯ�� �����ϴ�. ȭ�� ������ �ߴ��մϴ�.");
                break; // Ǯ�� ���� á���� �� �̻� �������� ����
            }

            // ���� ��ġ ���
            float randomX = UnityEngine.Random.Range(0, spawnAreaSize.x);
            float randomZ = UnityEngine.Random.Range(0, spawnAreaSize.y);
            Vector3 spawnPos = areaStartCorner + new Vector3(randomX, 0, randomZ);
            float terrainHeight = targetTerrain.SampleHeight(spawnPos);
            Vector3 finalSpawnPosition = new Vector3(spawnPos.x, terrainHeight, spawnPos.z);

            // ������ ������Ʈ�� ��ġ�� ȸ���� �����ϰ� Ȱ��ȭ
            fireInstance.transform.position = finalSpawnPosition;
            fireInstance.transform.rotation = Quaternion.identity;
            fireInstance.SetActive(true);

            activeFires.Add(fireInstance); // Ȱ��ȭ�� ��Ͽ� �߰�
        }

        hasFireBeenGenerated = true;
    }

    /// <summary>
    /// ���� �߻��� ��� ȭ�縦 ��Ȱ��ȭ�Ͽ� Ǯ�� �ǵ����ϴ�.
    /// </summary>
    public void ExtinguishAllFires()
    {
        if (activeFires.Count == 0) return;

        Debug.Log("��� ȭ�縦 �����մϴ�...");
        foreach (GameObject fire in activeFires)
            fire.SetActive(false); // Destroy ��� ��Ȱ��ȭ�Ͽ� Ǯ�� �ݳ�
        
        activeFires.Clear(); // Ȱ��ȭ ��ϸ� ��� (Ǯ���� ������Ʈ�� �״�� ��������)
        hasFireBeenGenerated = false; // �ٽ� ȭ�縦 �߻���ų �� �ֵ��� �÷��� �ʱ�ȭ
    }

    /// <summary>
    /// ������Ʈ Ǯ���� ��Ȱ��ȭ ������ ȭ�� ������Ʈ�� ã�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns>��� ������ ���� ������Ʈ �Ǵ� null</returns>
    private GameObject GetPooledFireObject()
    {
        // Ǯ�� �ִ� ��� ������Ʈ�� Ȯ��
        foreach (GameObject fire in fireParticlePool)
        {
            if (!fire.activeInHierarchy) // ��Ȱ��ȭ�� ������Ʈ�� ã����
                return fire; // ��ȯ
        }
        
        // ���� ��� ������Ʈ�� ��� ���̶�� null�� ��ȯ (�Ǵ� ���⼭ Ǯ�� �������� Ȯ���� ���� ����)
        Debug.LogWarning("������Ʈ Ǯ�� ���� á���ϴ�! ��� ��ƼŬ�� ��� ���Դϴ�.");
        return null;
    }

    // �������� Scene �信�� ȭ�� �߻� ������ �ð������� �����ִ� Gizmo
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0, 0.4f);
        Vector3 gizmoCenter = spawnCenter;
        
        if(targetTerrain != null)
            gizmoCenter.y = targetTerrain.SampleHeight(spawnCenter) + 1f;
        
        Gizmos.DrawCube(gizmoCenter, new Vector3(spawnAreaSize.x, 2, spawnAreaSize.y));
    }
}
