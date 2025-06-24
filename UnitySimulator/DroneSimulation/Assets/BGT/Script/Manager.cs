using ActUtlType64Lib;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private ActUtlType64 mxComponent;
    private bool currentY0State;
    private bool currentY1State;

    public Chain1 chainInstance ;
    public Chain1 chainInstance12;
    void Start()
    {
        mxComponent = new ActUtlType64Lib.ActUtlType64();
        mxComponent.ActLogicalStationNumber = 1;
        int iRet = mxComponent.Open(); // PLC ���� �õ�
        if (iRet == 0)
        {
            Debug.Log("Manager.cs: PLC ���� ����!");
        }
        else
        {
            Debug.LogError($"Manager.cs: PLC ���� ����! ���� �ڵ�: {iRet}");
        }
    }

    void Update()
    {
       
        ReadDevice();
        
    }
    private void ReadDevice()
    {
        // blockCnt�� 1�� �ϸ� startDevice(Y0)���� 16��Ʈ(1����)�� �о�ɴϴ�.
        // �� 1���� �ȿ� Y0, Y1, Y2, ..., YF������ ��Ʈ�� ���Ե˴ϴ�.
        int blockCnt = 1;
        int[] data = new int[blockCnt]; // ũ�⸦ blockCnt�� ����ϴ�.

        // Y0���� �����ϴ� 1���带 �о�ɴϴ�.
        int iRet = mxComponent.ReadDeviceBlock("Y0", blockCnt, out data[0]);

        if (iRet == 0)
        {
            int y0ToYF = data[0]; // Y0���� YF������ ��Ʈ ���¸� ���� ���� ��

            // Y0 ��Ʈ ����
            bool newY0State = ((y0ToYF >> 0) & 1) == 1; // 0��° ��Ʈ
            if (newY0State != currentY0State) // ���� ���� ����
            {
                currentY0State = newY0State;
                if (chainInstance != null)
                {
                    if (currentY0State)
                        chainInstance.ActivateChain();
                    else
                        chainInstance.DeactivateChain();
                }
            }
            // Y1 ��Ʈ ����
            bool newY1State = ((y0ToYF >> 1) & 1) == 1; // 1��° ��Ʈ
            if (newY1State != currentY1State) // ���� ���� ����
            {
                currentY1State = newY1State;
                if (chainInstance12 != null) // chainInstance12�� Y1�� �����Ѵٰ� ����
                {
                    if (currentY1State)
                        chainInstance12.ActivateChain();
                    else
                        chainInstance12.DeactivateChain();
                }
            }
        }
        else
        {
            Debug.LogWarning($"Manager.cs: ���� �б� ����! ���� �ڵ�: {iRet}.");
        }
    }
    private void WirteDevice(int X0ToXF)
    {
        // blockCnt�� 1�� �ϸ� startDevice(X0)���� 16��Ʈ(1����)�� �о�ɴϴ�.
        int blockCnt = 1;
        int[] data = new int[blockCnt]; // ũ�⸦ blockCnt�� ����ϴ�.

        // X0���� �����ϴ� 1���带 �о�ɴϴ�.
        int iRet = mxComponent.WriteDeviceBlock("X0", blockCnt, data[0]);

        if(iRet == 0)
        {
            X0ToXF = data[0];
        }
    }
        void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� PLC ���� ����
        if (mxComponent != null)
        {
            int iRet = mxComponent.Close();
            if (iRet == 0)
            {
                Debug.Log("Manager.cs: PLC ���� ���� ����.");
            }
            else
            {
                Debug.LogError($"Manager.cs: PLC ���� ���� ����! ���� �ڵ�: {iRet}");
            }
        }
    }
}