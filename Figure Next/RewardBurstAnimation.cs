using UnityEngine;
using System.Collections;
using Random = System.Random;

namespace UserEngagementKit
{
    public class RewardBurstAnimation : MonoBehaviour
    {
        public Animation popAnimation;
        public AudioSource brustsound;
        public AudioSource PickSound;
        public GameObject[] Coins;
        public Transform Target;
        private Random random;
        public int burstRadius;
        private int iterator;
        public Sprite icon;

        private void Awake() => random ??= new Random();

        private void OnEnable() => BurstAnimation();

        private void BurstAnimation()
        {
            iterator = 0;
            float burstTime = 0.5f;
            //SoundManager.instance.PlaySound(SoundManager.SoundType.goldBurst);
            brustsound.Play();
            for (int i = 0; i < Coins.Length; i++)
            {
                var item = Coins[i];
                item.SetActive(true);
                item.transform.localPosition = Vector3.zero;
                iTween.MoveTo(item,
                iTween.Hash("position", GetRandom2DPositionInRadius(burstRadius),
                            "time", burstTime,
                            "islocal", true,
                            "oncomplete", nameof(TransferAnimation),
                           "oncompleteparams", item,
                           "oncompletetarget", gameObject
                           ));
            }

            StartCoroutine(CloseCoinAnim(3));
        }
        private Vector3 GetRandom2DPositionInRadius(int radius) => new(random.Next(-radius, radius), random.Next(-radius, radius), 0);
        public void TransferAnimation(GameObject item)
        {
            iterator++;

            float delay = 0.05f;
            float moveTime = 0.25f;
            item.SetActive(true);
            iTween.MoveTo(item,
            iTween.Hash("position", Target.position,
                        "time", moveTime,
                        "delay", delay * iterator,
                        "easetype", "linear",
                        "oncomplete", nameof(ResetChildObject),
                        "oncompleteparams", item,
                        "oncompletetarget", gameObject));

        }

        private void ResetChildObject(GameObject obj)
        {
            iTween.Stop(obj);
            obj.SetActive(false);
            PickSound.Play();
            popAnimation.Play();
            //SoundManager.instance.PlaySound(SoundManager.SoundType.goldAdd);
        }

        internal IEnumerator CloseCoinAnim(float Delay)
        {
            yield return new WaitForSeconds(Delay);
            gameObject.SetActive(false);
        }
    }
}