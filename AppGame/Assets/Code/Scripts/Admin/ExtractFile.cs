using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ExtractFile : MonoBehaviour
{
    [Header("Name file CSV")]
    [Tooltip("csv file name")]
    [SerializeField] private string fileName;

    private readonly List<object[]> dataList = new();

    public void ExportDataToCSV()
    {

        SetDataPlayers();

        StreamWriter outStream = File.CreateText(CombinePathDocumentWithFileNameCSV());

        outStream.WriteLine(SetOutputData());
        outStream.Close();
    }

    private void SetDataPlayers()
    {
        foreach (var item in AdminNetworkManager.instance.playerDataList)
        {
            object[] playerDataListArray =
            {
                    item.player,
                    item.game,
                    item.hit,
                    item.error,
                    item.time.ToString()
            };

            dataList.Add(playerDataListArray);
        }
    }

    private string CombinePathDocumentWithFileNameCSV()
    {
        // Search path name document
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //Combines local address plus file name
        string filePath = Path.Combine(documentsPath, fileName);

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

