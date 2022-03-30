using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardPositioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Set at Runtime")]
    public RectTransform Rtransform;
    public Vector2 startPosition;
    public Vector3 startRotation;
    public bool isHoverable;
    
    //Fields Only Used in This Class
    private Tween moveTween;
    private Tween rotateTween;
    private Tween scaleTween;
    private Tween fadeTween;
    private Vector3 rotation;
    
    //set Anchors, MoveTo, RotateTo,Hover,Click,Add,Remove

    public void Awake() {
        //Static During Runtime
        Rtransform = GetComponent<RectTransform>();
        Rtransform.anchorMin = new Vector2(0.5f,0f);
        Rtransform.anchorMax = new Vector2(0.5f, 0f);
    }

    public void setStart(Vector3 startPos, Vector3 startRot) {
        startPosition = startPos;
        startRotation = startRot;
    }

    public void setSpawn() {
        startPosition = PlayerInfo.instance.cardSpawn.GetComponent<RectTransform>().anchoredPosition;
        Rtransform.anchoredPosition = startPosition;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        moveTo(new Vector3(startPosition.x,startPosition.y + 80,0), 0.3f);
        rotateTo(Vector3.zero, 0.3f);
        scaleTo(new Vector3(1.3f,1.3f,0),0.3f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        moveTo(new Vector3(startPosition.x,startPosition.y,0), 0.3f);
        rotateTo(startRotation,0.3f);
        scaleTo(Vector3.one, 0.3f);
    }

    public void moveTo(Vector3 endPosition, float speed) {
        moveTween?.Kill();
        moveTween = Rtransform.DOAnchorPos(endPosition,speed).SetEase(Ease.OutCubic).SetAutoKill(false);
    }

    public void rotateTo(Vector3 endRotation, float speed) {
        rotateTween?.Kill();
        rotateTween = Rtransform.DORotate(endRotation,speed).SetEase(Ease.OutCubic).SetAutoKill(false);
    }

    public void scaleTo(Vector3 endScale, float speed) {
        scaleTween?.Kill();
        scaleTween = Rtransform.DOScale(endScale, speed).SetEase(Ease.OutCubic).SetAutoKill(false);
    }

    public void fadeTo(float endFade, float duration) {
        fadeTween?.Kill();
        fadeTween = Rtransform.GetComponent<CanvasGroup>().DOFade(endFade, duration).SetEase(Ease.OutCubic)
            .SetAutoKill(false);
    }
    
    void OnDestroy() {
        DOTween.Kill(transform);
        DOTween.Kill(gameObject);
        DOTween.Kill(GetComponent<CanvasGroup>());
        foreach (Transform child in transform)
        {
            DOTween.Kill(child);
        }
    }
}
