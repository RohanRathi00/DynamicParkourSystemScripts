using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] private List<ParkourAction> parkourAction;

    private bool inAction;

    private EnvironmentScanner environmentScanner;
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        environmentScanner = GetComponent<EnvironmentScanner>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButton("Jump") && !inAction)
        {
            var hitData = environmentScanner.CheckObstracle();

            if (hitData.obstracleDetected)
            {
                foreach (var action in parkourAction)
                {
                    if (action.CheckIfAnimationIsPossible(hitData, transform))
                    {
                        StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator DoParkourAction(ParkourAction action)
    {
        inAction = true;
        playerController.SetControl(false);

        animator.SetBool("mirrorAnimation", action.ShouldMirrorAnimation);
        animator.CrossFade(action.AnimName, 0.2f);
        yield return null;

        var animState = animator.GetNextAnimatorStateInfo(0);

        float timer = 0;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            if (action.RotateTowardsObstracle)
            {
                Quaternion.RotateTowards(transform.rotation, action.TargetRotation, playerController.RotationSpeed * Time.deltaTime);
            }

            if (action.EnableTargetMatching)
            {
                MatchTarget(action);
            }

            if (animator.IsInTransition(0) && timer > 0.5f)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(action.PostClimbDelay);

        playerController.SetControl(true);
        inAction = false;
    }

    private void MatchTarget(ParkourAction action)
    {
        if (animator.isMatchingTarget)
        {
            return;
        }

        animator.MatchTarget(action.MatchPos, transform.rotation, action.MatchBodyPart, new MatchTargetWeightMask(action.MatchPosWeight, 0), action.MatchStartTime, action.MatchTargetTime);
    }
}
