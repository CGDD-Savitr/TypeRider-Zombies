using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TypeRider.Assets.Classes;
using System.Linq;

public class PrintHighScores : MonoBehaviour {

	public Text list;

	// Use this for initialization
	void Start () {
		List<HighScore> scores = new List<HighScore>();
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
		list.text = string.Join("\n", scores.Select(score => score.ToString()).ToArray());

		CrossSceneRegistry.HighScores = scores.Select(score => score.Score).ToList().GetRange(0, Mathf.Min(scores.Count, 20));
	}
}
