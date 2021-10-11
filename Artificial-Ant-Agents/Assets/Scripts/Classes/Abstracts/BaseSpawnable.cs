using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class BaseSpawnable : MonoBehaviour
{
    public bool spoilable;
    public float lifeTime = 20.0f;
    public float spoilTime = 30.0f;

    private Cluster cluster;
    private SpriteRenderer sr;

    public void Init(Cluster cluster)
    {
        this.cluster = cluster;
        if (spoilable) StartSpoiling();
    }

    public virtual void Awake() => sr = GetComponent<SpriteRenderer>();

    public void Drop()
    {
        Destroy(gameObject);
        cluster.totalAmount -= 1;
    }

    private void StartSpoiling()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(lifeTime);
        sequence.Append(sr.DOColor(Color.yellow, spoilTime / 2.0f)).Join(sr.DOFade(0.5f, spoilTime / 2.0f));
        sequence.Append(sr.DOColor(Color.green, spoilTime / 2.0f)).Join(sr.DOFade(0.0f, spoilTime / 2.0f));
        sequence.AppendCallback(() => Drop());
    }
}
