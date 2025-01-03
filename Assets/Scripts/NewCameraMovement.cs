using UnityEngine;

public class NewCameraMovement : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    private float mouseSensitivity = 1500f;

    [SerializeField]
    private float distanceFromPlayer = 5.0f;

    //obracanie góra-dó³
    private float pitch = 0.0f;

    //obracanie lewo-prawo
    private float yaw = 0.0f;   

    void LateUpdate()
    {
        //sterowanie kamer¹ za pomoc¹ myszki
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //ograniczenie k¹ta kamery
        pitch = Mathf.Clamp(pitch, -30f, 60f); 

        // bliczanie nowej pozycji i rotacji kamery
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 position = player.position - (rotation * Vector3.forward * distanceFromPlayer);

        transform.position = position;
        transform.LookAt(player);
    }
}
