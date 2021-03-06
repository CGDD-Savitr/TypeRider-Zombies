﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using TypeRider.Assets.Classes;
using UnityEngine;

public class WordPool : MonoBehaviour {

	public string DefaultWord = "savitr";

	public int ShortestWordLength { get; private set; }

	public int LongestWordLength { get; private set; }

	Dictionary<int, List<string>> wordBuckets;

	void Awake()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("WordBanks/" + CrossSceneRegistry.Difficulty.File);
		wordBuckets = new Dictionary<int, List<string>>();
		List<string> words = new List<string>(textAsset.text.Split(','));
		words.ForEach(word => 
		{
			if (ShortestWordLength == 0 || word.Length < ShortestWordLength)
				ShortestWordLength = word.Length;
			if (word.Length > LongestWordLength)
				LongestWordLength = word.Length;
			registerWord(word);
		});
	}

	public string GetWord(int length)
	{
		List<string> words = null;
		int len = Mathf.Min(length, LongestWordLength);
		if (wordBuckets.TryGetValue(len, out words) && words.Count > 0)
		{
			int index = (int) Random.Range(0, words.Count - 1);
			string word = words[index];
			words.RemoveAt(index);
			wordBuckets[word.Length] = words;
			return word;
		}
		if (len > 0)
			return GetWord(len - 1);
		return DefaultWord; // Should ideally never be reached
	}


	public void ReturnWord(string word)
	{
		registerWord(word);
	}

	void registerWord(string word)
	{
		if (!wordBuckets.ContainsKey(word.Length))
			wordBuckets.Add(word.Length, new List<string>());
		wordBuckets[word.Length].Add(word);
	}
}
