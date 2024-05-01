using UnityEngine;

namespace CodeFirst.Gameplay
{
    public static class SaveLoad
	{
		public static Board Load(Board board)
		{
			var result = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/LastBoardData.json");
			board = null;
			return JsonUtility.FromJson<Board>(result);
		}

		public static void Save(Board board)
		{
			string dataToSave = JsonUtility.ToJson(board);
			System.IO.File.WriteAllText(Application.streamingAssetsPath + "/LastBoardData.json", dataToSave);
		}
	}
}