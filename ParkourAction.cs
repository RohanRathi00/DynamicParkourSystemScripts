using UnityEngine;

[CreateAssetMenu(menuName = "New Parkour System")]
public class ParkourAction : ScriptableObject
{
    [SerializeField] private string animName;
    [SerializeField] private string obstracleTag;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    [SerializeField] private bool rotateTowardsObstracle;
    [SerializeField] private float postClimbDelay;

    [Header("Target Matching")]
    [SerializeField] private bool enableTargetMatching = true;
    [SerializeField] protected AvatarTarget matchBodyPart;
    [SerializeField] private float matchStartTime;
    [SerializeField] private float matchTargetTime;
    [SerializeField] private Vector3 matchPosWeight = new Vector3(0, 1, 0);

    public Quaternion TargetRotation { get; set; }
    public Vector3 MatchPos { get; set; }
    public bool ShouldMirrorAnimation { get; set; }

    public virtual bool CheckIfAnimationIsPossible(ObstracleHitData hitData, Transform player)
    {
        if (!string.IsNullOrEmpty(obstracleTag) && hitData.obstracleHitInfo.transform.tag != obstracleTag)
        {
            return false;
        }

        float heightOfObstracle = hitData.obstracleHeightHitInfo.point.y - player.position.y;

        if (heightOfObstracle < minHeight || heightOfObstracle > maxHeight)
        {
            return false;
        }

        if (rotateTowardsObstracle)
        {
            TargetRotation = Quaternion.LookRotation(-hitData.obstracleHitInfo.normal);
        }

        if (enableTargetMatching)
        {
            MatchPos = hitData.obstracleHeightHitInfo.point;
        }

        return true;
    }

    public string AnimName => animName;
    public bool RotateTowardsObstracle => rotateTowardsObstracle;
    public float PostClimbDelay => postClimbDelay;

    public bool EnableTargetMatching => enableTargetMatching;
    public AvatarTarget MatchBodyPart => matchBodyPart;    
    public float MatchStartTime => matchStartTime;
    public float MatchTargetTime => matchTargetTime;
    public Vector3 MatchPosWeight => matchPosWeight;
}
