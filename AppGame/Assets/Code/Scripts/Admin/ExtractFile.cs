using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Scripts.Admin
{
    public class ExtractFile : MonoBehaviour
    {
        [Header("Name file CSV")]
        [Tooltip("csv file name")]
        [SerializeField] private string fileName;
        private string filePath;
        private readonly List<object[]> dataList = new();

        [Header("Button Extract File")]
        [Tooltip("Button Extract File")]
        [SerializeField] private GameObject getComponentInButtonExtractFile;

        private readonly float waitTime = 1.0f;


        private void OnDisable()
        {
            int child = getComponentInButtonExtractFile.transform.childCount;

            for (int i = 0; i < child; i++)
            {
                var getArrow = getComponentInButtonExtractFile.transform.GetChild(i).gameObject;

                getArrow.transform.DOKill();
            }
        }

        public void ExportDataToCSV()
        {

            SetDataPlayers();

            StreamWriter outStream = File.CreateText(CombinePathDocumentWithFileNameCSV());

            //TODO Alterar o header para enum
            string header = "Nome;Level;Tela;Acertos;Erros;Tempo";

            outStream.WriteLine(header);

            outStream.WriteLine(SetOutputData());

            outStream.Close();

            StartCoroutine(ExtractFileSuccess());
        }

        IEnumerator ExtractFileSuccess()
        {
            int child = getComponentInButtonExtractFile.transform.childCount;

            for (int i = 0; i < child; i++)
            {
                var getArrow = getComponentInButtonExtractFile.transform.GetChild(i).gameObject;

                var getScaleX = getArrow.transform.localScale.x;
                var getScaleY = getArrow.transform.localScale.y;
                var getPosArrow = getArrow.transform.position;

                yield return new WaitForSeconds(0.5f);

                getArrow.transform.DOMoveY(-50, waitTime).SetEase(Ease.OutCubic);

                yield return new WaitForSeconds(0.5f);

                getArrow.transform.localScale = Vector3.zero;
                getArrow.transform.DOMoveY(150, waitTime);

                yield return new WaitForSeconds(waitTime);

                getArrow.transform.position = getPosArrow;

                getArrow.transform.DOScaleX(getScaleX, waitTime).SetEase(Ease.OutBounce);
                getArrow.transform.DOScaleY(getScaleY, waitTime).SetEase(Ease.OutBounce);
            }
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

}
