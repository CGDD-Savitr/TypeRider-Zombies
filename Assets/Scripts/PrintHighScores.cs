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

	public Text leftList;
	public Text rightList;

	// Use this for initialization
	void Start () {
		List<HighScore> scores = new List<HighScore>();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		HighScores data = new HighScores();

		if (File.Exists(Application.persistentDataPath + "/TypeRiderHighScoresTimestamped.dat"))
		{
			file = File.Open(Application.persistentDataPath + "/TypeRiderHighScoresTimestamped.dat", FileMode.Open);
			data = (HighScores)bf.Deserialize(file);
			file.Close();

			scores = data.scores;
		}

		//sorted by score
		scores.Sort((x, y) => -(x.Score.CompareTo(y.Score)));
		leftList.text = "HIGH SCORES:\n" + string.Join("\n", scores.Select(score => score.ToString()).ToArray());
		//sorted by date
		scores.Sort((x, y) => -(x.Timestamp.CompareTo(y.Timestamp)));
		rightList.text = "RECENT SCORES:\n" + string.Join("\n", scores.Select(score => score.ToString()).ToArray());



		CrossSceneRegistry.HighScores = scores.Select(score => score.Score).ToList().GetRange(0, Mathf.Min(scores.Count, 20));
	}
}
