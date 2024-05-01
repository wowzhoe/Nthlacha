using System.IO;
using UnityEngine;

namespace CodeFirst.Gameplay
{
    public static class SaveLoad
	{
		public static string fileToSavePath = Application.streamingAssetsPath + "/LastBoardData.json";

		public static Board Load(Board board)
		{
			board = null;
			var result = File.ReadAllText(fileToSavePath);
			return JsonUtility.FromJson<Board>(result);
		}

		public static void Save(Board board)
		{
			File.Delete(fileToSavePath);
			string dataToSave = JsonUtility.ToJson(board);
			File.WriteAllText(fileToSavePath, dataToSave);
		}
	}
}