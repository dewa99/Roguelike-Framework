using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public partial class CardView
    {
        public async UniTask RunPlayAnimation()
        {
            await Sequence.Create()
                .Group(Tween.ShakeLocalPosition(transform, new Vector3(5f,5f,5f) ,1f, 0.3f));
        }

        public async UniTask RunMoveAnimation(Transform target)
        {
            this.transform.SetParent(target);
            await Sequence.Create()
                .Group(Tween.LocalPosition(this.transform, target.position, 0.3f, Ease.InQuad));
        }
    }
}
