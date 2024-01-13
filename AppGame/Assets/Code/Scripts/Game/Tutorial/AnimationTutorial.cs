using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



namespace Tutorial
{
    [Obsolete("Not used any more", true)]
    public class AnimationTutorial : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private List<GameObject> listTutorial = new();

        private void Start()
        {
            foreach (var item in listTutorial)
            {
                item.transform.localScale = Vector3.zero;

                ActiveAnimation();
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
