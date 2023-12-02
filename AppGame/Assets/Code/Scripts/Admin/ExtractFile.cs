using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class ExtractFile : MonoBehaviour
{
    [Header("Name file CSV")]
    [Tooltip("csv file name")]
    [SerializeField] private string fileName;
    private string filePath;
    private readonly List<object[]> dataList = new();

    public void ExportDataToCSV()
    {

        SetDataPlayers();

        StreamWriter outStream = File.CreateText(CombinePathDocumentWithFileNameCSV());

        string header = "Nome;Level;Tela;Acertos;Erros;Tempo";

        outStream.WriteLine(header);

        outStream.WriteLine(SetOutputData());

        outStream.Close();
    }

    private void SetDataPlayers()
    {

        HashSet<string> hashSet = new(dataList.Select(item => $"{item[0]}_{item[1]}_{item[2]}_{item[3]}_{item[4]}_{item[5]}"));

        foreach (var item in AdminNetworkManager.instance.playerDataList)
        {
            string keyHash = $"{item.player}_{item.game}_{item.screen}_{item.hit}_{item.error}_{item.time}";

            if (hashSet.Add(keyHash))
            {
                object[] playerDataListArray =
                {
                    item.player,
                    item.game,
                    item.screen,
                    item.hit,
                    item.error,
                    item.time.ToString()
                };

                dataList.Add(playerDataListArray);
            }
        }
    }

    private string CombinePathDocumentWithFileNameCSV()
    {
        // Search path name document
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //Combines local address plus file name
        filePath = Path.Combine(documentsPath, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return filePath;
    }

    private StringBuilder SetOutputData()
    {
        object[][] output = new object[dataList.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = dataList[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder stringBuilder = new();

        for (int index = 0; index < length; index++)
            stringBuilder.AppendLine(string.Join(delimiter, output[index]));


        return stringBuilder;
    }


}

