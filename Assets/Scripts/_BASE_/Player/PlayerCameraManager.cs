using UnityEngine;
using System.Collections;

namespace Nutbusterz.Calamitus
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("CAMERA DATA")]
        public Transform playerTransform; // Reference to the player's transform
        public float followSpeed = 5f; // Speed at which the camera follows the player
        public float rotationSpeed = 5f; // Speed at which the camera rotates to match player's rotation
        public float maxFOVChange = 1f; // Maximum FOV change when moving or turning
        public float maxCameraDistance = 0.2f; // Maximum distance between camera and player
        public float cameraTiltAmount = 0.1f; // Adjusted camera tilt amount
        public float mouseSensitivity = 2.0f; // Mouse sensitivity for camera rotation

        private Vector3 offset; // Offset between the camera and player
        private Camera mainCamera; // Reference to the camera component
        private float originalFOV; // Original FOV of the camera
        private float xRotation = 0.0f; // Current rotation around the X-axis (up and down)

        [Header("CAMERA SHAKE DATA")]
        public float shakeDuration = 0.1f;
        public float shakeMagnitude = 0.1f;

        [Header("PICKUP & HOLD DATA")]
        public Transform holdPos; // Reference to the hold position
        public float throwForce = 500f; // Rorce at which the object is thrown at
        public float pickUpRange = 5f; // How far the player can pickup the object from
        private float rotationSensitivity = 1f; // How fast/slow the object is rotated in relation to mouse movement
        private GameObject heldObj; // Object which we pick up
        private Rigidbody heldObjRb; // Rigidbody of object we pick up
        private bool canDrop = true; // This is needed so we don't throw/drop object when rotating the object
        private int LayerNumber; // Layer index
        private float originalMouseSensitivityValue;

        private void Start()
        {
            player = playerTransform.GetComponent<PlayerManager>();
            LayerNumber = LayerMask.NameToLayer("Hold");
            originalMouseSensitivityValue = player.playerCameraManager.mouseSensitivity;

            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
            offset = transform.position - playerTransform.position;
            mainCamera = GetComponentInChildren<Camera>();
            originalFOV = mainCamera.fieldOfView;
        }

        public void UseAllCameraMovement()
        {
            UseCameraMovement();
            UsePickupCameraMovement();
        }

        private void UseCameraMovement()
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Follow the player smoothly
            Vector3 targetPosition = playerTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotate to match the player's rotation smoothly
            Quaternion targetRotation = playerTransform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Calculate FOV change based on input and apply it
            float fovChange = Mathf.Lerp(0, maxFOVChange, input.magnitude);
            mainCamera.fieldOfView = originalFOV + fovChange;

            // Adjust camera distance based on input
            float cameraDistance = Mathf.Lerp(0, maxCameraDistance, input.magnitude);
            Vector3 newCameraPosition = transform.position + transform.forward * cameraDistance;
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, followSpeed * Time.deltaTime);

            // Handle mouse rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation to avoid flipping

            // Rotate the player and camera separately
            playerTransform.Rotate(Vector3.up * mouseX);

            // Apply clamped camera tilt based on player input
            float tiltAngle = Mathf.Clamp(input.x * cameraTiltAmount, -cameraTiltAmount, cameraTiltAmount);
            Quaternion targetTiltRotation = Quaternion.Euler(0, 0, -tiltAngle);

            // Smoothly interpolate to the target tilt rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(xRotation, 0f, 0f) * targetTiltRotation, rotationSpeed * Time.deltaTime);
        }
        private void UsePickupCameraMovement()
        {
            if (Input.GetKeyDown(KeyCode.E)) // Change E to whichever key you want to press to pick up
            {
                if (heldObj == null) // If currently not holding anything
                {
                    // Perform raycast to check if player is looking at object within pickuprange
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                    {
                        // Make sure pickup tag is attached
                        if (hit.transform.gameObject.tag == "Pickup")
                        {
                            // Pass in object hit into the PickUpObject function
                            PickUpObject(hit.transform.gameObject);
                        }
                    }
                }
                else
                {
                    if (canDrop == true)
                    {
                        StopClipping(); // Prevents object from clipping through walls
                        DropObject();
                    }
                }
            }
            if (heldObj != null) // If player is holding object
            {
                MoveObject(); // Keep object position at holdPos
                RotateObject();
                if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) // Mous0 (leftclick) is used to throw, change this if you want another button to be used)
                {
                    StopClipping();
                    ThrowObject();
                }

            }
        }
        public IEnumerator ShakeCamera()
        {
            float elapsedTime = 0f;
            Vector3 originalCameraPosition = mainCamera.transform.localPosition;

            while (elapsedTime < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeMagnitude;
                float y = Random.Range(-1f, 1f) * shakeMagnitude;

                mainCamera.transform.localPosition = new Vector3(x, y, originalCameraPosition.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            mainCamera.transform.localPosition = originalCameraPosition;
        }

        void PickUpObject(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) // Make sure the object has a RigidBody
            {
                heldObj = pickUpObj; // Assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); // Assign Rigidbody
                heldObjRb.isKinematic = true;
                heldObjRb.transform.parent = holdPos.transform; // Parent object to holdposition
                heldObj.layer = LayerNumber; // Change the object layer to the holdLayer
                                             // Make sure object doesnt collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            }
        }
        void DropObject()
        {
            // Re-enable collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0; // Object assigned back to default layer
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null; // Unparent object
            heldObj = null; // Undefine game object
        }
        void MoveObject()
        {
            // BELOW CODE: Keep object position the same as the holdPosition position
            heldObj.transform.position = holdPos.transform.position;
        }
        void RotateObject()
        {
            if (Input.GetKey(KeyCode.R)) // Hold R key to rotate, change this to whatever key you want
            {
                canDrop = false; // Make sure throwing can't occur during rotating

                // BELOW CODE: Disable player being able to look around
                player.playerCameraManager.mouseSensitivity = 0f;

                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
                float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

                // BELOW CODE: rotate the object depending on mouse X-Y Axis
                heldObj.transform.Rotate(Vector3.down, XaxisRotation);
                heldObj.transform.Rotate(Vector3.right, YaxisRotation);
            }
            else
            {
                // BELOW CODE: Re-enable player being able to look around
                player.playerCameraManager.mouseSensitivity = originalMouseSensitivityValue;
                canDrop = true;
            }
        }
        void ThrowObject()
        {
            // same as drop function, but add force to object before undefining it
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObjRb.AddForce(transform.forward * throwForce);
            heldObj = null;
        }
        void StopClipping() //function only called when dropping/throwing
        {
            var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
                                                                                              //have to use RaycastAll as object blocks raycast in center screen
                                                                                              //RaycastAll returns array of all colliders hit within the cliprange
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
            //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
            if (hits.Length > 1)
            {
                //change object position to camera position 
                heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
                                                                                              //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
            }
        }

        //private void UseOldCameraMovement()
        //{
        //    Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //    // Follow the player smoothly
        //    Vector3 targetPosition = playerTransform.position + offset;
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        //    // Rotate to match the player's rotation smoothly
        //    Quaternion targetRotation = playerTransform.rotation;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //    // Calculate FOV change based on input and apply it
        //    float fovChange = Mathf.Lerp(0, maxFOVChange, input.magnitude);

        //    mainCamera.fieldOfView = originalFOV + fovChange;

        //    // Adjust camera distance based on input
        //    float cameraDistance = Mathf.Lerp(0, maxCameraDistance, input.magnitude);
        //    Vector3 newCameraPosition = transform.position + transform.forward * cameraDistance;
        //    transform.position = Vector3.Lerp(transform.position, newCameraPosition, followSpeed * Time.deltaTime);

        //    // Handle mouse rotation
        //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //    xRotation -= mouseY;
        //    xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation to avoid flipping

        //    // Rotate the player and camera separately
        //    playerTransform.Rotate(Vector3.up * mouseX);

        //    // Apply clamped camera tilt based on player input
        //    float tiltAngle = Mathf.Clamp(input.x * cameraTiltAmount, -cameraTiltAmount, cameraTiltAmount);
        //    Quaternion tiltRotation = Quaternion.Euler(0, 0, -tiltAngle);
        //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f) * tiltRotation;
        //}
    }
}
