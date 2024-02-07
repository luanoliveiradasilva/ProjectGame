using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Screens.FirstScreen
{
    public class AnimationTutorial : MonoBehaviour
    {

        [Header("Animations")]

        [Tooltip("Fade time to animation")]
        [SerializeField] private float fadeTime = 1.0f;

        [Tooltip("Get all gameobjects to transform scale")]
        [SerializeField] private List<GameObject> ObjectsToScale = new();

        /*         [Tooltip("Get gameobject logo of tutorial")]
                [SerializeField] private GameObject logoMenu; */

        [Tooltip("Get gameobject button continue")]
        [SerializeField] private GameObject buttonIniciar;


        private void Start()
        {
            buttonIniciar.transform.localScale = Vector3.zero;

            GetAllGameobjects();

            /* AnimationLogo(); */
        }

        private void GetAllGameobjects()
        {
            foreach (var item in ObjectsToScale)
            {
                item.transform.localScale = Vector3.zero;

                if (item.name == "Basket")
                {
                    item.transform.DOScale(1.3f, fadeTime).SetEase(Ease.OutBounce);
                }

                item.transform.DOScale(1.0f, fadeTime).SetEase(Ease.OutBounce);
            }
        }
        public void AnimationButton()
        {

            buttonIniciar.transform.DOScale(1.0f, fadeTime).SetEase(Ease.OutSine);

        }

        /*  private void AnimationLogo()
         {
             var spaceWidthScreen = Screen.width;
             var widthScreen = spaceWidthScreen / 4;
             logoMenu.transform.DOLocalMoveX(-widthScreen, 2.0f).SetEase(Ease.OutCubic);
         } */
    }
}
