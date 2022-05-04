using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class WeaponItem : MonoBehaviour
{
    public WeaponItemData data;
    [SerializeField] Transform thumbailPosition;

    CancellationTokenSource tokenSource;
    private float startPosition;

    enum Direction
    {
        up,
        down
    }
    Direction direction = Direction.down;

    private void Awake()
    {
        tokenSource = new CancellationTokenSource();
        startPosition = transform.position.y;
        Instantiate(data.Thumbnail, thumbailPosition.position, Quaternion.identity, thumbailPosition);

        Animation();
    }

	private void OnDisable()
    {
        tokenSource.Cancel();
    }

    public Weapon PickUpWeapon()
    {
        if(data.isDestructible)
            Destroy(gameObject);
        return data.weapontoEquip;
    }

    async void Animation()
    {
        Vector3 currentPosition = new Vector3();

        while (true)
        {
            if (tokenSource.IsCancellationRequested)
                break;

            currentPosition = transform.position;

            if (direction == Direction.up)
            {
                currentPosition.y += data.animationSpeed * Time.deltaTime;
                if (currentPosition.y >= startPosition + data.bounceRange)
                {
                    direction = Direction.down;
                }
            }

            if (direction == Direction.down)
            {
                currentPosition.y -= data.animationSpeed * Time.deltaTime;
                if (currentPosition.y <= startPosition)
                {
                    direction = Direction.up;
                }
            }

            transform.position = currentPosition;
            transform.Rotate(0.0f, data.rotationSpeed * Time.deltaTime, 0.0f);
            await Task.Yield();
        }
    }
}
