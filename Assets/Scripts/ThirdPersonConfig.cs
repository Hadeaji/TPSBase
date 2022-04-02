using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class ThirdPersonConfig : MonoBehaviour
{
    [SerializeField] private Rig aimRig;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCam;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform shootPointTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    [SerializeField] private GameObject vfxShootPoint;
    [SerializeField] private List<GameObject> vfx = new List<GameObject>();
    [SerializeField] private GameObject effectToSpawn;


    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private GameObject arrowPlaceholder;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Vector3 mouseWorldPosition;
    private Transform hitTransform = null;

    private float aimRigWieght = 1f;

    // debug serialization
    [SerializeField] private float shotDamage = 30f;
    [SerializeField] private float initialChargeMultiplier = 1f;
    private float chargeMultiplier = 1f;
    [SerializeField] private float chargeRate = 1f;
    [SerializeField] private float maxChargeMultiplier = 3f;
    private float timeDelay = 1f;
    private float timer = 0f;
    private bool isShooting = false;


    [SerializeField] private float reloadTimer = 0.2f;
    //

    // animation IDs
    private int _animIDShoot;
    private int _animIDChargeProc;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        // initial mouse position
        mouseWorldPosition = Vector3.zero;

        _animIDShoot = Animator.StringToHash("IsShooting");
        _animIDChargeProc = Animator.StringToHash("Charge");
    }

    private void Start()
    {
        effectToSpawn = vfx[0];
    }

    private void Update()
    {

        HandleWorldMousePositoion();
        HandleAim();
        HandleStopMove();
        HandleShooting();

        HandleAimations();
    }
    private void HandleStopMove()
    {
        if (!starterAssetsInputs.aim)
        {
            starterAssetsInputs.move = starterAssetsInputs.moveStore;
        }
    }

    private void OnAimStopped()
    {
        // deactivate the aim camera and return to mouse normal Sensitivity
        aimVirtualCam.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(normalSensitivity);
        thirdPersonController.SetRotateOnMove(true);
        aimRigWieght = 0f;
    }
    private void OnAimStarted()
    {
        // activate the aim camera after trigerring the aim
        aimVirtualCam.gameObject.SetActive(true);
        // Mouse Aim Sensitivity
        thirdPersonController.SetSensitivity(aimSensitivity);
        thirdPersonController.SetRotateOnMove(false);
        aimRigWieght = 1f;
    }
    private void HandleWorldMousePositoion()
    {
        // using ray cast with the default layer to find the point you are aiming at 
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            // change shoot transfor position
            shootPointTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;

            hitTransform = raycastHit.transform;
        }

    }

    private void HandleAim()
    {
        if (starterAssetsInputs.aim)
        {
            OnAimStarted();
            AimAnimations();

        }
        else
        {
            OnAimStopped();
            // Aim Layer exit
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
    }

    private void AimAnimations()
    {
        //Debug.Log(vfxShootPoint.transform.childCount);
        //if (vfxShootPoint.transform.childCount == 0)
        //{
        //    arrowPlaceholder = Instantiate(effectToSpawn, vfxShootPoint.transform, false);
        //    Debug.Log("it is there now");

        //}
        // Aim Layer Animations 1 is the layer index 
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        // rotate the player object to aim point
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
    }

    private void HandleShooting()
    {
        if (!starterAssetsInputs.aim || isShooting)
        {
            chargeMultiplier = initialChargeMultiplier;
            animator.SetFloat(_animIDChargeProc, 0f);
            //if (arrowPlaceholder)
            //{
            //    Debug.Log("Deleted");
            //    Destroy(arrowPlaceholder);
            //    arrowPlaceholder = null;
            //}
            return;
        }
        if (starterAssetsInputs.charge && !isShooting)
        {
            // initial shoot damage * charge damage multiplier ex: 0.5
            // increase charge damage multiplier with time * charge rate
            // cap the charge damage multiplier
            // charge animation
            
            ChargeAnimation();
            
            timer += Time.deltaTime;
            if (timer >= timeDelay)
            {
                timer = 0f;
                if (chargeMultiplier < maxChargeMultiplier)
                {
                    chargeMultiplier += chargeRate;
                    // charge animation effect

                } else
                {
                    chargeMultiplier = maxChargeMultiplier;
                    // max charge animation
                }
            }
        }
        if (!starterAssetsInputs.charge && !isShooting && (timer !=0f || chargeMultiplier > initialChargeMultiplier))
        {
            Debug.Log("is this running?");
            // shoot animation
            ShootAnimation();

            Shoot();
            // reset Multiplier
            chargeMultiplier = initialChargeMultiplier;
            timer = 0f;
            
        }
    }

    private void Shoot()
    {
        //if (hitTransform != null)
        //{
        //    Debug.Log(hitTransform.tag);
        //    if (hitTransform.GetComponent<Target>() != null)
        //    {
        //        // Hit target
        //        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        //    }
        //    else
        //    {
        //        // Hit something else
        //        Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        //    }
        //}

        //Debug.Log("Shot Damage:" + shotDamage * chargeMultiplier);
        Vector3 deathPoint = shootPointTransform.position;
        Vector3 aimDirection = (mouseWorldPosition - vfxShootPoint.transform.position).normalized;
        //var simpleEffect = effectToSpawn;
        effectToSpawn.GetComponent<vfxProjectiles>().damage = shotDamage * chargeMultiplier;
        effectToSpawn.GetComponent<vfxProjectiles>().deathPosition = deathPoint;
        effectToSpawn.GetComponent<vfxProjectiles>().targetTransform = hitTransform;
        Instantiate(effectToSpawn, vfxShootPoint.transform.position, Quaternion.LookRotation(aimDirection, Vector3.up));

    }
    private void AnimationStatChange(int _animID,bool state)
    {
        animator.SetBool(_animID, state);
    }

    private void ShootAnimation()
    {
        //if (arrowPlaceholder)
        //{
        //    Destroy(arrowPlaceholder);
        //    arrowPlaceholder = null;
        //}
        // shoot layer
        isShooting = true;
        animator.SetLayerWeight(2, 0.55f);
        AnimationStatChange(_animIDShoot, true);

        FindObjectOfType<AudioManager>().Play("arrow1");

        Invoke("ShootTimeAnimationEnd", 0.3f);
    }

    public void ShootTimeAnimationEnd()
    {
        animator.SetFloat(_animIDChargeProc, 0f);
        AnimationStatChange(_animIDShoot, false);

        animator.SetLayerWeight(2, 0f);
        isShooting = false;
    }

    private void HandleAimations()
    {
        // updating aim rig IK stat all time
        aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWieght, Time.deltaTime * 20f);

    }

    private void ChargeAnimation()
    {
        float chargeVal = animator.GetFloat(_animIDChargeProc);
        if (chargeVal < 1f)
        {
            animator.SetFloat(_animIDChargeProc, (chargeMultiplier / maxChargeMultiplier) + timer);
        }
    }
}
