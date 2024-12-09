using UnityEngine;

public class FollowThomas : MonoBehaviour
{
    [SerializeField] float followSpeed = 2f; // Speed at which the camera follows the target
    [SerializeField] public GameObject target; // The target for the camera to follow
    [SerializeField] public GameObject background; // The background GameObject

    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Update()
    {
        // Get the bounds of the background
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();

        if (bgRenderer != null)
        {
            minBounds = bgRenderer.bounds.min;
            maxBounds = bgRenderer.bounds.max;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on the background object!");
            return; // Exit if the background is not valid
        }

        // Get the target's position
        Vector3 posTarget = target.transform.position;

        // Create a new position for the camera, keeping the z-axis at -10 (for 2D)
        Vector3 newPos = new Vector3(posTarget.x, posTarget.y, -10f);

        // Smoothly interpolate the camera's position toward the target
        Vector3 smoothedPos = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        // Adjust the camera bounds based on the camera's size
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize; // Vertical size of the camera view
        float cameraWidth = cameraHeight * camera.aspect; // Horizontal size based on aspect ratio

        // Clamp the camera's position within the adjusted background boundaries
        smoothedPos.x = Mathf.Clamp(smoothedPos.x, minBounds.x + cameraWidth, maxBounds.x - cameraWidth);
        smoothedPos.y = Mathf.Clamp(smoothedPos.y, minBounds.y + cameraHeight, maxBounds.y - cameraHeight);

        // Update the camera's position
        transform.position = smoothedPos;
    }

}
