using UnityEngine;

// Librairie found on internet
public class TerrainDestructible : MonoBehaviour
{
    private SpriteRenderer sr;
    private float widthWorld;
    private float heightWorld;
    private int widthPixel;
    private int heightPixel;

    private Color transp;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        transp = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        InitSpriteDimensions();
    }



    public void DestroyGround(Vector2 _pos, float _radius)
    {
        Vector2 center = World2Pixel(_pos);

        float radius = (_radius * widthPixel / widthWorld);

        int x, y, px, nx, py, ny, d;

        Texture2D newTex = new Texture2D(widthPixel, heightPixel);
        newTex = (Texture2D)Instantiate(sr.sprite.texture);

        for (x = 0; x <= radius; x++)
        {
            d = (int)Mathf.RoundToInt(Mathf.Sqrt(radius * radius - x * x));

            for (y = 0; y <= d; y++)
            {
                px = (int)center.x + x;
                nx = (int)center.x - x;
                py = (int)center.y + y;
                ny = (int)center.y - y;


                newTex.SetPixel(px, py, transp);
                newTex.SetPixel(nx, py, transp);
                newTex.SetPixel(px, ny, transp);
                newTex.SetPixel(nx, ny, transp);
            }
        }

        newTex.Apply();
        sr.sprite = Sprite.Create(newTex, new Rect(0f, 0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 100f);


        // Worst case
        for (int i = 0; i < GetComponents<PolygonCollider2D>().Length; i++)
        {
            Destroy(GetComponents<PolygonCollider2D>()[i]);
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private Vector2 World2Pixel(Vector2 _pos)
    {
        Vector2 v = new Vector2();

        float dx = _pos.x - transform.position.x;
        v.x = Mathf.RoundToInt(0.5f * widthPixel + dx * widthPixel / widthWorld);

        float dy = _pos.y - transform.position.y;
        v.y = Mathf.RoundToInt(0.5f * heightPixel + dy * heightPixel / heightWorld);

        return v;
    }
    private void InitSpriteDimensions()
    {
        widthWorld = sr.bounds.size.x;
        heightWorld = sr.bounds.size.y;

        widthPixel = sr.sprite.texture.width;
        heightPixel = sr.sprite.texture.height;
    }

}


