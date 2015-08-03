using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData
{
	static GameData s_Instance;

	public static GameData Instance
	{
		get
		{
			if(s_Instance == null)
			{
				s_Instance = new GameData();
			}

			return s_Instance;
		}
	}

	public List<WeaponType> UnlockedWeapons
	{
		get;
		protected set;
	}

	// Use this for initialization
	GameData()
	{
		Load ();

		if(UnlockedWeapons == null)
		{
			UnlockedWeapons = new List<WeaponType>();
		}
	}

	public void UnlockWeapon(WeaponType type)
	{
		if(!UnlockedWeapons.Contains (type))
		{
			UnlockedWeapons.Add (type);
		}
	}

	public void Save() 
	{
		BinaryFormatter bf = new BinaryFormatter();

		FileStream file = File.Create (Application.persistentDataPath + FileConstants.GAME_DATA);
		bf.Serialize(file, UnlockedWeapons);
		file.Close();
	}   
	
	public void Load() 
	{
		if(File.Exists(Application.persistentDataPath + FileConstants.GAME_DATA)) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FileConstants.GAME_DATA, FileMode.Open);
			UnlockedWeapons = (List<WeaponType>)bf.Deserialize(file);
			file.Close();
		}
	}
}
