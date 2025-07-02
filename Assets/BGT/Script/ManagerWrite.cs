using UnityEngine;

public class ManagerWrite : Manager
{
    // ======= Gameobject �ҷ�����======
    public GameObject Cube;
    public GameObject Carriage;
   

    public void WriteDevice()
    {
        short value1 = 1;
        short value0 = 0;

        if (Cube.GetComponent<Trigger>().TriggerSensor)
        {
            mxComponent.SetDevice("X0", value1);
        }
        else if (!Cube.GetComponent<Trigger>().TriggerSensor)
        {
            mxComponent.SetDevice("X0", value0);
        }

        if (Carriage.GetComponent<Trigger>().TriggerSensor)
        {
            mxComponent.SetDevice("X1", value1);
        }
        else if (!Carriage.GetComponent<Trigger>().TriggerSensor)
        {
            mxComponent.SetDevice("X1", value0);
        }
    }
    void OnApplicationQuit()
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
