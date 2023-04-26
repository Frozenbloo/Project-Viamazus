using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LocalFileSaveHandler
{
    private string saveDirPath = "";
    private string saveFileName = "";
    private bool encryptFile = false;
    private readonly string encryptionKey = "Frozenbloo";

    public LocalFileSaveHandler(string saveDirPath, string saveFileName, bool encryptFile) 
    {
        this.saveDirPath = saveDirPath;
        this.saveFileName = saveFileName;
        this.encryptFile = encryptFile;
    }

    public GameSave Load()
    {
		string fullPath = Path.Combine(saveDirPath, saveFileName);
        GameSave loadedSave = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string saveToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        saveToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptFile) 
                {
                    saveToLoad = EncryptDecryptSave(saveToLoad);
                }

                //deserializeData
                loadedSave = JsonUtility.FromJson<GameSave>(saveToLoad);
            } catch (Exception e)
            {
				Debug.LogError("Error in local file reading!\n" + e);
			}
        }
        return loadedSave;
	}

    public void Save(GameSave data)
    {
        // Ensures that the file saves on different operating systems
        string fullPath = Path.Combine(saveDirPath, saveFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serailize into json
            string saveToStore = JsonUtility.ToJson(data, true);

            if (encryptFile)
            {
                saveToStore = EncryptDecryptSave(saveToStore);
            }

            //write
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(saveToStore);
                }
            }
        } catch (Exception e)
        {
            Debug.LogError("Error in local file saving!\n" + e);
        }
    }

    //Extremely Simple Version of Xor Encryption
    private string EncryptDecryptSave(string save)
    {
        string modifiedSave = "";
        for (int i = 0; i < save.Length; i++)
        {
            modifiedSave += (char)(save[i] ^ encryptionKey[i % encryptionKey.Length]);
        }
        return modifiedSave;
    }
}
