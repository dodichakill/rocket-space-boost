using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;
    
    // Variables to track button press states for mobile
    private bool isLeftPressed = false;
    private bool isRightPressed = false;
    private bool isThrustPressed = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        // First check if mobile thrust button is pressed
        if (isThrustPressed)
        {
            StartThrusting();
            return; // Return to avoid conflicts with keyboard input
        }
        
        // Then check keyboard/gamepad input
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    public void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    public void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        
        // First handle mobile button states - these take priority
        if (isLeftPressed)
        {
            RotateLeft(isPressed: true);
            return; // Return to avoid conflicts with keyboard input
        }
        else if (isRightPressed)
        {
            RotateRight(isPressed: true);
            return; // Return to avoid conflicts with keyboard input
        }
        
        // If no mobile buttons are pressed, handle keyboard/gamepad input
        if (rotationInput < 0)
        {
            RotateRight(isPressed: true);
        }
        else if (rotationInput > 0)
        {
            RotateLeft(isPressed: true);
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    public void RotateLeft(bool isPressed)
    {
        if (isPressed)
        {
            // Apply continuous rotation every frame while pressed
            ApplyRotation(-rotationStrength);
            if (!leftThrustParticles.isPlaying)
            {
                rightThrustParticles.Stop();
                leftThrustParticles.Play();
            }
        }
    }
    
    // Methods for UI button press events
    public void OnLeftButtonDown()
    {
        isLeftPressed = true;
    }
    
    public void OnLeftButtonUp()
    {
        isLeftPressed = false;
        if (!isRightPressed) // Only stop particles if no other button is pressed
        {
            leftThrustParticles.Stop();
        }
    }

    public void RotateRight(bool isPressed)
    {
        if (isPressed)
        {
            // Apply continuous rotation every frame while pressed
            ApplyRotation(rotationStrength);
            if (!rightThrustParticles.isPlaying)
            {
                leftThrustParticles.Stop();
                rightThrustParticles.Play();
            }
        }
    }
    
    // Methods for UI button press events
    public void OnRightButtonDown()
    {
        isRightPressed = true;
    }
    
    public void OnRightButtonUp()
    {
        isRightPressed = false;
        if (!isLeftPressed) // Only stop particles if no other button is pressed
        {
            rightThrustParticles.Stop();
        }
    }
    
    // Methods for Thrust button (B button) press events
    public void OnThrustButtonDown()
    {
        isThrustPressed = true;
    }
    
    public void OnThrustButtonUp()
    {
        isThrustPressed = false;
        StopThrusting();
    }

    /*************  ✨ Windsurf Command ⭐  *************/
    /// <summary>
    /// Applies the rotation to the Rigidbody, avoiding any physics-based rotation.
    /// </summary>
    /// <param name="rotationThisFrame">The amount of rotation to apply this frame, in degrees.</param>
    /*******  baa8a106-1bd5-43c0-824e-9c1cc6c3949a  *******/
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
