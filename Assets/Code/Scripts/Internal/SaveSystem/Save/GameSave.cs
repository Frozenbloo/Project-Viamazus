using System;

[Serializable]
public class GameSave
{
	public float masterVolume;
	public float musicVolume;
	public float SFXVolume;
	public float UIVolume;

	public bool tutorialCompleted;
	public int mazeAttemps;
	public int goldCount;
	public float weaponDmg;
	public int playerLvl;
	public float playerMaxHP;

	// Default values for when a new game is started
	public GameSave()
	{
		masterVolume = 80.0f;
		musicVolume = 80.0f;
		SFXVolume = 80.0f;
		UIVolume = 80.0f;

		tutorialCompleted = false;
		mazeAttemps = 0;
		goldCount = 0;
		weaponDmg = 1;
		playerLvl = 0;
		playerMaxHP = 10;
	}
}
