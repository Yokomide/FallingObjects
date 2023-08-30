using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class HealthBarContoller : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> HealthHearts = new List<GameObject>();

    [SerializeField]
    private GameObject _heart;

    [SerializeField]
    private GameObject _heartsHolder;

    [SerializeField]
    private float distanceBetweenHearts = 5;

    public void SetHealthHearts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddHeart();
        }
    }

    private void SetHeartPosition()
    {
        RectTransform currentHeart = HealthHearts[HealthHearts.Count - 1].GetComponent<RectTransform>();
        if (HealthHearts.Count - 1 > 0)
        {
            RectTransform previousHeart = HealthHearts[HealthHearts.Count - 2].GetComponent<RectTransform>();
            Vector2 previousPosition = previousHeart.anchoredPosition;

            currentHeart.anchoredPosition = new Vector2(previousPosition.x - 10f * distanceBetweenHearts, 0);
        }
        else
        {
            currentHeart.anchoredPosition = Vector2.zero;
        }
    }

    public void AddHeart()
    {
        var newHeart = Instantiate(_heart);
        newHeart.transform.SetParent(_heartsHolder.transform);
        HealthHearts.Add(newHeart);
        SetHeartPosition();
        PlayAddHeartAnimation();
    }

    public void RemoveHeart()
    {
        if (HealthHearts.Count > 0)
        {
            StartCoroutine(DestroyCoroutine());
        }
    }

    private void PlayAddHeartAnimation()
    {
        var currentHeartTransform = HealthHearts.Last().GetComponent<RectTransform>();

        currentHeartTransform.localScale = Vector3.zero;
        currentHeartTransform.DOScale(1, 2f);
    }

    private IEnumerator DestroyCoroutine()
    {
        RectTransform tweeningObject = HealthHearts.Last().GetComponent<RectTransform>();
        HealthHearts.Remove(HealthHearts.Last());

        yield return DOTween.Sequence()
            .Append(tweeningObject.DOShakeAnchorPos(2f, 10f))
            .Join(tweeningObject.DOScale(0, 2f)).WaitForCompletion();

        Destroy(tweeningObject.gameObject);
    }
}