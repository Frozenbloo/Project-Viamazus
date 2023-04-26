using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    void LoadData(GameSave data);
    // ref allows implementing script to modify data
    void SaveData(ref GameSave data);
}
