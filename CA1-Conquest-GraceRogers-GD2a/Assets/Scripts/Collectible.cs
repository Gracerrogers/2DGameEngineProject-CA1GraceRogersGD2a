using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Player Player;
    public int direction = 1;
    private float height = 0.4f;
    private float timePassed = 0;
    void Update()
    {
        Vector2 position = transform.position;
        position.y += (height * Time.deltaTime * direction);
        transform.position = position;

        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            timePassed = 0;
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "collectible" && collision.gameObject.tag == "player")
        {
            Player.AddCollectible();
            Destroy(this.gameObject);
        }
    }
}
