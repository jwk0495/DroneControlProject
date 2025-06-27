using UnityEngine;

public class ManagerWrite : Manager
{
    /// <summary>
    /// PLC 'X' ����̽�(����)�� �� ����
    /// </summary>
    /// <param name="valueToWrite">X0�� �� 16��Ʈ(1����) ���� ��</param>
    public void WriteDevice()
    {
        short value1 = 1;
        short value0 = 0;

        if (Cube.GetComponent<Trigger>().TriggerSensor)
        {
            mxComponent.WriteDeviceRandom2("X0", 1, ref value1);
        }
        else
        {
            mxComponent.WriteDeviceRandom2("X0", 1, ref value0);
        }
    }
    public void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� PLC ���� ����
        if (mxComponent != null)
        {
            int iRet = mxComponent.Close();
            if (iRet == 0)
                Debug.Log("Manager.cs: PLC ���� ���� ����.");
            else
                Debug.LogError($"Manager.cs: PLC ���� ���� ����! ���� �ڵ�: {iRet}");
        }
    }
}
