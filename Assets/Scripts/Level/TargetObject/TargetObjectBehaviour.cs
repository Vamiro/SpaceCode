using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Renderer))]
    public class TargetObjectBehaviour : MonoBehaviour
    {
        private Renderer _renderer;
        private ColorNames _currentHueColor = ColorNames.Blue;

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
            bool killDate = false;
            //raycast starts
            var ray = new Ray(position, transformPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, transformPosition.magnitude))
            {
                if (hit.transform.GetComponent<LevelScannerBehaviour>() == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken);
                    return false; //stops async method if collides with smth except scanner
                }
                if (hit.transform.GetComponent<LevelScannerBehaviour>().currentScannerHueColor != _currentHueColor)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken);
                    return false; //stops async method if scanner and targetObject got different collors
                }
            }
            //raycast ends
            var tween = transform.DOMove(nextPos, 0.2F).SetEase(Ease.Linear).SetAutoKill(false); //create DOTween var
            tween.OnUpdate(() => {if (cancellationToken.IsCancellationRequested) tween.Kill();}); //stops async method and kill DOTween var if needed
            tween.OnKill(() => { killDate = true; tween = null; /*Debug.Log($"Tween has been killed{this}");*/}); //lambda function witch starts when DOTween var was Killed
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
            _renderer.material.color = (_currentHueColor = value).ConvertColor();
            return true;
        }
    }
}
