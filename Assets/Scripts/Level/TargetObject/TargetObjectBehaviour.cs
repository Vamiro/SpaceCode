using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Renderer))]
    public class TargetObjectBehaviour : MonoBehaviour, IResetColor
    {
        private Renderer _renderer;
        [SerializeField] private ColorNames _currentHueColor = ColorNames.Blue;
        
        public ColorNames CurrentHueColor => _currentHueColor;

        private void Awake()
        {
            try
            {
                _renderer = gameObject.GetComponent<Renderer>();
                _renderer.material.color = (_currentHueColor).ConvertColor();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public async Task<bool> StepForward(Vector3 nextPos, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return false; //stops async method if needed

            var position = transform.position;
            var transformPosition = nextPos - position;
            var killDate = false;
            //raycast starts
            var ray = new Ray(position, transformPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, transformPosition.magnitude))
            {
                if (hit.transform.GetComponent<IStopTarget>() == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken);
                    return false; //stops async method if collides with smth except scanner
                }
                if (hit.transform.GetComponent<IStopTarget>().IsLocked(this))
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken);
                    return false; //stops async method if scanner and targetObject got different collors
                }
            }
            //raycast ends
            var tween = transform.DOMove(nextPos, 0.2F).SetEase(Ease.Linear).SetAutoKill(false); //create DOTween var
            tween.OnUpdate(() => { if (cancellationToken.IsCancellationRequested) tween.Kill(); }); //stops async method and kill DOTween var if needed
            tween.OnKill(() => { killDate = true; tween = null; }); //lambda function witch starts when DOTween var was Killed
            await Task.WhenAny(tween.AsyncWaitForCompletion(), tween.AsyncWaitForKill()); //await fot Completion ot Killing DOTween var
            
            try
            {
                return !killDate && tween.IsComplete(); //returns True if DOTween var completed or False if killed
            }
            finally
            {
                if(!killDate) tween.Kill(); //Kills DOTween var if it completed
            }
        }
        
        public async Task<bool> ChangeColor(ColorNames value, CancellationToken cancellationToken)
        {
            //_renderer.material.color = (_currentHueColor = value).ConvertColor();
            if (cancellationToken.IsCancellationRequested) return false;
            
            var killDate = false;
            var tween = _renderer.material.DOColor((_currentHueColor = value).ConvertColor(),
                1f).SetAutoKill(false);
            tween.OnUpdate(() => { if (cancellationToken.IsCancellationRequested) tween.Kill(); });
            tween.OnKill(() => { killDate = true; tween = null; });
            
            await Task.WhenAny(tween.AsyncWaitForCompletion(), tween.AsyncWaitForKill());
            
            try
            {
                return !killDate && tween.IsComplete(); //returns True if DOTween var completed or False if killed
            }
            finally
            {
                if(!killDate) tween.Kill(); //Kills DOTween var if it completed
            }
        }

        public bool CheckGround()
        {
            var ray = new Ray(transform.position, -Vector3.up);
            if (Physics.Raycast(ray, 1f, LayerMask.GetMask("Ground")))
            {
                return true;
            }
            Debug.Log("Not on ground");
            return false;
        }

        public bool CheckObstacle()
        {
            var transform1 = transform;
            var position = transform1.position;
            var transformPosition = transform1.forward;
            //raycast starts
            var ray = new Ray(position, transformPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, transformPosition.magnitude))
            {
                if (hit.transform.GetComponent<IStopTarget>() == null)
                {
                    return true;
                }
                if (hit.transform.GetComponent<IStopTarget>().IsLocked(this))
                {
                    return false;
                }
            }
            return false;
        }
    }
}
