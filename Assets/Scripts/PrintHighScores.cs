using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PrintHighScores : MonoBehaviour {

	public Text list;

	// Use this for initialization
	void Start () {
		List<int> scores = new List<int>();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		HighScores data = new HighScores();

		if (File.Exists(Application.persistentDataPath + "/TypeRiderHighScores.dat"))
		{
			file = File.Open(Application.persistentDataPath + "/TypeRiderHighScores.dat", FileMode.Open);
			data = (HighScores)bf.Deserialize(file);
			file.Close();

			scores = data.scores;
		}

		scores.Sort();
		scores.Reverse();

		foreach (int score in scores)
		{
			list.text += "\n" + score;
		}
	}
}
