public interface ISave
{
	void LoadData(GameSave data);
	// ref allows implementing script to modify data
	void SaveData(ref GameSave data);
}
