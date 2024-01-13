using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Tutorial
{
    public class AnimationTutorial : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1f;

        [SerializeField] private List<GameObject> listTutorial = new();

        [HideInInspector] public bool isTutorial;

        private void Start()
        {
            foreach (var item in listTutorial)
            {
                item.transform.localScale = Vector3.zero;

                isTutorial = item.CompareTag("Tutorial");

                if (isTutorial)
                {
                    ActiveAnimation();
                }
            }
        }

        public void ActiveAnimation()
        {

            foreach (var item in listTutorial)
            {
                item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            }
        }
    }
}
