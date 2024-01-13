using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Screens.FirstScreen
{
    public class AnimationProduct : MonoBehaviour
    {

        [SerializeField] private float fadeTime = 1f;

        [SerializeField] private List<GameObject> listProductGame = new();

        [SerializeField] private GameObject products;

        private void Awake()
        {
            int child = products.transform.childCount;

            for (int i = 0; i < child; i++)
            {
                var prod = products.transform.GetChild(i).gameObject;

                if (prod != null)
                {
                    listProductGame.Add(prod);
                }
                else
                {
                    Debug.LogWarning("Child transform at index " + i + " is null.");
                }
            }
        }

        private void Start()
        {
            foreach (var item in listProductGame)
            {
                item.transform.localScale = Vector3.zero;
            }
        }

        public void ActiveAnimation()
        {
            foreach (var item in listProductGame)
            {
                item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            }
        }
    }
}
