using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class SortingLayer : MonoBehaviour
{
    private GameObject _playerObject;
    private TilemapRenderer _tRr;
    private SpriteRenderer _sRr;
    private Collider2D _cldr;
    [SerializeField] private string middleSortingTag = "FOW";
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
                    _tRr.sortingLayerName = "Walls";
                }
                // игрок рядом
                else if (playerBoundLow > objBoundLow && playerBoundLow < objBoundHi)
                {
                    _tRr.sortingLayerName = middleSortingTag;
                }
                // игрок за
                else
                {
                    _tRr.sortingLayerName = "FOW";
                }
            }
            else if (_sRr)
            {
                // игрок перед
                if (playerBoundLow < objBoundLow)
                {
                    _sRr.sortingLayerName = "Walls";
                }
                // игрок рядом
                else if (playerBoundLow > objBoundLow && playerBoundLow < objBoundHi)
                {
                    _sRr.sortingLayerName = middleSortingTag;
                }
                // игрок за
                else
                {
                    _sRr.sortingLayerName = "FOW";
                }
            }
        }
    }
}

