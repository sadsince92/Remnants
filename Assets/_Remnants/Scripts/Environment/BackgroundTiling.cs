using UnityEngine;

public class BackgroundTiling : MonoBehaviour
{
    #region Component References
    private SpriteRenderer sr;
    private Camera mainCamera;
    #endregion

    #region Tiling Parameters
    private float spriteWidth;
    private GameObject rightTile;
    private GameObject leftTile;
    private Transform[] tiles;
    private float cameraHalfWidth;
    #endregion

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;

        spriteWidth = sr.bounds.size.x;
        Debug.Log($"{gameObject.name}: Sprite Width = {spriteWidth}, Camera Half Width = {cameraHalfWidth}");

        rightTile = Instantiate(gameObject, transform.parent);
        rightTile.GetComponent<BackgroundTiling>().enabled = false; // Disable the script on the new tile
        rightTile.transform.position = transform.position + Vector3.right * spriteWidth;

        leftTile = Instantiate(gameObject, transform.parent);
        leftTile.GetComponent<BackgroundTiling>().enabled = false; // Disable the script on the new tile
        leftTile.transform.position = transform.position + Vector3.left * spriteWidth;

        tiles = new Transform[3];

        tiles[0] = leftTile.transform;
        tiles[1] = transform;
        tiles[2] = rightTile.transform;
    }

    void LateUpdate()
    {
        float cameraX = mainCamera.transform.position.x;
        float recycleDistance = spriteWidth - cameraHalfWidth;
        while (cameraX > tiles[1].position.x + recycleDistance)
        {
            tiles[0].position = tiles[2].position + Vector3.right * spriteWidth;

            Transform recycledTile = tiles[0];

            tiles[0] = tiles[1];
            tiles[1] = tiles[2];
            tiles[2] = recycledTile;
        }
        while (cameraX < tiles[1].position.x - recycleDistance)
        {
            tiles[2].position = tiles[0].position + Vector3.left * spriteWidth;

            Transform recycledTile = tiles[2];

            tiles[2] = tiles[1];
            tiles[1] = tiles[0];
            tiles[0] = recycledTile;

        }

    }
}