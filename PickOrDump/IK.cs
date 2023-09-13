using UnityEngine;

public class IK : MonoBehaviour
{
    public bool ikActive;
    public Animator _Animator;
    public Transform RightHandObj;
    public Transform RightElbowObj;
    public Transform LeftHandObj;

    #region IK
    void OnAnimatorIK()
    {
        if (_Animator != null)
        {
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                // Set the right hand target position and rotation, if one has been assigned
                if (RightHandObj != null)
                {
                    _Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    _Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    _Animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandObj.position);
                    _Animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandObj.rotation);
                }
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    _Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    _Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                }
                if (RightElbowObj != null)
                {
                    _Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                    _Animator.SetIKHintPosition(AvatarIKHint.RightElbow, RightElbowObj.position);
                }
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    _Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
                }
                if (LeftHandObj != null)
                {
                    _Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    _Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    _Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandObj.position);
                    _Animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandObj.rotation);
                }
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    _Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    _Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                }
            }
        }
    }
    #endregion
}