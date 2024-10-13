using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class SortingLayer : MonoBehaviour
{
    private GameObject _playerObject;
    private TilemapRenderer _tRr;
    private SpriteRenderer _sRr;
    private SpriteRenderer[] _sRrs;
    private Collider2D _cldr;
    [SerializeField] private string lowSortingTag = "Walls";
    [SerializeField] private string middleSortingTag = "FOW";
    [SerializeField] private string highSortingTag = "FOW";
    float playerBoundLow, objBoundLow, objBoundHi;
    public bool activeRoom = true;

    private void OnValidate()
    {
        _cldr ??= GetComponent<Collider2D>();
        _tRr ??= GetComponent<TilemapRenderer>();
        _sRr ??= GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _cldr ??= GetComponent<Collider2D>();
        _tRr ??= GetComponent<TilemapRenderer>();
        _sRr ??= GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _sRrs = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (activeRoom)
        {
            playerBoundLow = _playerObject.GetComponent<Collider2D>().bounds.min.y /*- _playerObject.transform.position.y*/;
            objBoundLow = /*_cldr.transform.position.y -*/ _cldr.bounds.min.y;
            objBoundHi = /*_cldr.transform.position.y -*/ _cldr.bounds.max.y;

            if (_tRr)
            {
                // игрок перед
                if (playerBoundLow < objBoundLow)
                {
                    _tRr.sortingLayerName = lowSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = lowSortingTag;
                    }
                }
                // игрок рядом
                else if (playerBoundLow > objBoundLow && playerBoundLow < objBoundHi)
                {
                    _tRr.sortingLayerName = middleSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = middleSortingTag;
                    }
                }
                // игрок за
                else
                {
                    _tRr.sortingLayerName = highSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = highSortingTag;
                    }
                }
            }
            else if (_sRr)
            {
                // игрок перед
                if (playerBoundLow < objBoundLow)
                {
                    _sRr.sortingLayerName = lowSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = lowSortingTag;
                    }
                }
                // игрок рядом
                else if (playerBoundLow > objBoundLow && playerBoundLow < objBoundHi)
                {
                    _sRr.sortingLayerName = middleSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = middleSortingTag;
                    }
                }
                // игрок за
                else
                {
                    _sRr.sortingLayerName = highSortingTag;
                    foreach (var srr in _sRrs)
                    {
                        srr.sortingLayerName = highSortingTag;
                    }
                }
            }
        }
    }
}

