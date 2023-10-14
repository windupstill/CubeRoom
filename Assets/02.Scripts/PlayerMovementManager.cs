using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour

{

    public Keyboard Keycodes;

    [System.Serializable]
    public class Keyboard

    {

        public KeyCode Jump = KeyCode.Space;
        public KeyCode Crouch = KeyCode.C;

    }

    CharacterController CharacterController;

    public bool CanControl;

    public bool UseGravity;

    public bool IsRunning;
    public bool IsWalking;

    public GameObject Camera;
    public GameObject CameraStandPosition;
    public GameObject CameraCrouchPosition;

    public bool CanRun;

    public GameObject AttachedBody;

    public bool IsCrouching;

    public bool CanStand;
    public float CanStandDistance;

    public float HalfScale;
    public float FullScale;

    public float WalkSpeed;
    public float RunSpeed;

    public float Speed;

    public float SpeedBoostTime;
    public float JumpBoostTime;

    public bool CanJump;

    public float JumpStamina;

    public float JumpSpeed;

    public float Gravity;

    public bool IsFalling;

    public float Stamina;

    public Image JumpBoostIcon;
    public Image SpeedBoostIcon;

    public float MaxStamina;
    public float MinStamina;

    public float StaminaRegenerationRate;
    public float StaminaDegenerationRate;

    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 InAirMoveDirection = Vector3.zero;

    public float LastPositionY;
    public float FallDistance;

    public float FallDamageLow;
    public float FallDamageMedium;
    public float FallDamageHigh;

    void Awake()

    {

        CharacterController = GetComponent<CharacterController>();
        Stamina = MaxStamina;

    }

    public void Start()

    {

        IsFalling = false;

    }

    void Update()

    {

        Vector3 Upwards = transform.up;

        if (Physics.Raycast(transform.position, Upwards, CanStandDistance))

        {

            CanStand = false;

        }

        else

        {

            CanStand = true;

        }

        if (CharacterController.isGrounded == true)

        {

            IsFalling = false;

            if (CanControl == true)

            {

                MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                MoveDirection = transform.TransformDirection(MoveDirection);
                MoveDirection *= Speed;

                if (Input.GetKeyDown(Keycodes.Jump) && (CanJump == true))

                {

                    MoveDirection.y = JumpSpeed;
                    Stamina -= JumpStamina;

                }

            }

        }

        if (CharacterController.isGrounded == false)

        {

            IsFalling = true;

        }

        if (UseGravity == true)

        {

            ApplyGravity();

        }

        if (IsCrouching == true)

        {

            CharacterController.height = HalfScale;
            Camera.gameObject.transform.position = CameraCrouchPosition.gameObject.transform.position;

            AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, this.transform.position.y - 0.6f, AttachedBody.transform.position.z);

            CanJump = false;

        }

        else if (IsCrouching == false)

        {

            CharacterController.height = FullScale;
            Camera.gameObject.transform.position = CameraStandPosition.gameObject.transform.position;

            AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, this.transform.position.y - 1, AttachedBody.transform.position.z);

            CanJump = true;

        }

        if (Input.GetKeyDown(Keycodes.Crouch) && (IsCrouching == false))

        {

            IsCrouching = true;

        }

        else if (Input.GetKeyDown(Keycodes.Crouch) && (IsCrouching == true) && (CanStand == true))

        {

            IsCrouching = false;
            AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, AttachedBody.transform.position.y, AttachedBody.transform.position.z);

        }

        if ((Stamina > MinStamina) && (CanRun == true) && (IsCrouching == false) && (Input.GetKey(KeyCode.LeftShift)))

        {

            IsRunning = true;
            IsWalking = false;

        }

        else

        {

            IsRunning = false;
            IsWalking = true;

        }

        if (IsWalking == true)

        {

            Speed = WalkSpeed;

        }

        else if ((IsRunning == true) && (CanRun == true))

        {

            Speed = RunSpeed;
            Stamina -= 1 * Time.deltaTime * StaminaDegenerationRate;

        }

        if (Stamina >= MaxStamina)

        {

            Stamina = MaxStamina;
            CanRun = true;

        }

        if ((Stamina >= MinStamina && Stamina < MaxStamina) && (IsRunning == false))

        {

            Stamina += 1 * Time.deltaTime * StaminaRegenerationRate;

        }

        if (Stamina <= MinStamina)

        {

            Stamina = MinStamina;
            CanRun = false;
            CanJump = false;

        }

        if (LastPositionY > transform.position.y)

        {

            FallDistance += LastPositionY - transform.position.y;

        }

        LastPositionY = transform.position.y;

        if (CharacterController.isGrounded)

        {

            ApplyNormal();

        }

        CharacterController.Move(MoveDirection * Time.deltaTime);

    }

    public void ApplyNormal()

    {

        FallDistance = 0;
        LastPositionY = 0;

    }

    public void ApplyGravity()

    {

        MoveDirection.y -= Gravity * Time.deltaTime;

    }

    public IEnumerator PickupSpeed()

    {

        WalkSpeed *= 3;
        RunSpeed *= 3;

        SpeedBoostIcon.enabled = true;

        yield return new WaitForSeconds(SpeedBoostTime);

        WalkSpeed /= 3;
        RunSpeed /= 3;

        SpeedBoostIcon.enabled = false;

    }

    public IEnumerator PickupJump()

    {

        JumpSpeed *= 3;

        JumpBoostIcon.enabled = true;

        yield return new WaitForSeconds(JumpBoostTime);

        JumpSpeed /= 3;

        JumpBoostIcon.enabled = false;

    }

}