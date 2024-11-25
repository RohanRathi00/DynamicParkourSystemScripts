using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/Custom Actions/New Vault Action")]
public class VaultAction : ParkourAction
{
    public override bool CheckIfAnimationIsPossible(ObstracleHitData hitData, Transform player)
    {
        if (!base.CheckIfAnimationIsPossible(hitData, player))
        {
            return false;
        }

        var hitPoint = hitData.obstracleHitInfo.transform.InverseTransformPoint(hitData.obstracleHitInfo.point);
         
        if (hitPoint.z < 0 && hitPoint.x < 0 || hitPoint.z > 0 && hitPoint.x > 0)
        {
            // Mirror Animation
            ShouldMirrorAnimation = true;
            matchBodyPart = AvatarTarget.RightHand;
        }
        else
        {
            // Don'r Mirror Animation
            ShouldMirrorAnimation = false;
            matchBodyPart = AvatarTarget.LeftHand;
        }

        return true;
    }
}
