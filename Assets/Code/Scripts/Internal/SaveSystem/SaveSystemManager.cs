using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveSystemManager : MonoBehaviour
{
	[Header("Local File Config")]
	[SerializeField] string fileName;
	[SerializeField] bool Encrypt;

	[Header("Debugging Tools")]
	[SerializeField] bool DebugSavingByInitIfNull = false;

	private GameSave gameSave;

    public static SaveSystemManager instance { get; private set; }
	private List<ISave> saveObjects;
	private LocalFileSaveHandler localFileSaveHandler;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("There is more than one SaveManager in the scene, therefore its been Destroyed");
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		localFileSaveHandler = new LocalFileSaveHandler(Application.persistentDataPath, fileName, Encrypt);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		saveObjects = FindAllSaveDataObjects();
		LoadGame();
	}

	public void OnSceneUnloaded(Scene scene)
	{
		SaveGame();
	}

	public void NewGame()
	{
		gameSave = new GameSave();
	}

	public void LoadGame()
	{
		gameSave = localFileSaveHandler.Load();

		if (gameSave == null && DebugSavingByInitIfNull)
		{
			NewGame();
		}

		if (gameSave == null)
		{
			Debug.Log("No data found. Initialising Defaults");
			return;
		}

		foreach (ISave saveDataObject in saveObjects)
		{
			saveDataObject.LoadData(gameSave);
		}
	}

	public void SaveGame()
	{
		if (gameSave == null)
		{
			Debug.LogError("Save is null");
			return;
		}

		foreach (ISave saveDataObject in saveObjects)
		{
			saveDataObject.SaveData(ref gameSave);
		}
		
		localFileSaveHandler.Save(gameSave);
	}

	public GameSave GetSaveGameForNullCheckingOnly()
	{
		return gameSave;
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}

	private List<ISave> FindAllSaveDataObjects()
	{
		IEnumerable<ISave> saveObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();

		return new List<ISave>(saveObjects);
	}

	public bool HasSaveData()
	{
		return gameSave != null;
	}
}
