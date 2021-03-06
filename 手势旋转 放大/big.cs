﻿
//   █████▒█    ██  ▄████▄   ██ ▄█▀       ██████╗ ██╗   ██╗ ██████╗
// ▓██   ▒ ██  ▓██▒▒██▀ ▀█   ██▄█▒        ██╔══██╗██║   ██║██╔════╝
// ▒████ ░▓██  ▒██░▒▓█    ▄ ▓███▄░        ██████╔╝██║   ██║██║  ███╗
// ░▓█▒  ░▓▓█  ░██░▒▓▓▄ ▄██▒▓██ █▄        ██╔══██╗██║   ██║██║   ██║
// ░▒█░   ▒▒█████▓ ▒ ▓███▀ ░▒██▒ █▄       ██████╔╝╚██████╔╝╚██████╔╝
//  ▒ ░   ░▒▓▒ ▒ ▒ ░ ░▒ ▒  ░▒ ▒▒ ▓▒       ╚═════╝  ╚═════╝  ╚═════╝
//  ░     ░░▒░ ░ ░   ░  ▒   ░ ░▒ ▒░
//  ░ ░    ░░░ ░ ░ ░        ░ ░░ ░
//           ░     ░ ░      ░  ░

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class big : MonoBehaviour {
    /// <summary>
    /// 上一帧两指间距离
    /// </summary>
    private float lastDistance = 0;

    /// <summary>
    /// 当前两个手指之间的距离
    /// </summary>
    private float twoTouchDistance = 0;
    /// <summary>
    /// 第一根手指按下的坐标
    /// </summary>
    Vector2 firstTouch = Vector3.zero;

    /// <summary>
    /// 第二根手指按下的坐标
    /// </summary>
    Vector2 secondTouch = Vector3.zero;

    /// <summary>
    /// 是否有两只手指按下
    /// </summary>
    private bool isTwoTouch = false;

    void Update () {
        if (gameObject.activeSelf)
        {
           //如果有两个及以上的手指按下
            if (Input.touchCount > 1)
            {
                //当第二根手指按下的时候
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    isTwoTouch = true;
                    //获取第一根手指的位置
                    firstTouch = Input.touches[0].position;
                    //获取第二根手指的位置
                    secondTouch = Input.touches[1].position;

                    lastDistance = Vector2.Distance(firstTouch, secondTouch);
                }

                //如果有两根手指按下
                if (isTwoTouch)
                {
                    //每一帧都得到两个手指的坐标以及距离
                    firstTouch = Input.touches[0].position;
                    secondTouch = Input.touches[1].position;
                    twoTouchDistance = Vector2.Distance(firstTouch, secondTouch);

                    //当前图片的缩放
                    Vector3 curImageScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
                    //两根手指上一帧和这帧之间的距离差
                    //因为100个像素代表单位1，把距离差除以100看缩放几倍
                    float changeScaleDistance = (twoTouchDistance - lastDistance) / 100;
                    //因为缩放 Scale 是一个Vector3，所以这个代表缩放的Vector3的值就是缩放的倍数
                    Vector3 changeScale = new Vector3(changeScaleDistance, changeScaleDistance, 0);
                    //图片的缩放等于当前的缩放加上 修改的缩放
                    transform.localScale = curImageScale + changeScale;
                    //控制缩放级别
                    transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.5f, 10f), Mathf.Clamp(transform.localScale.y, 0.5f, 10f), Mathf.Clamp(transform.localScale.y, 0.5f, 10f));
                    //这一帧结束后，当前的距离就会变成上一帧的距离了
                    lastDistance = twoTouchDistance;
                }

                //当第二根手指结束时（抬起）
                if (Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    isTwoTouch = false;
                    firstTouch = Vector3.zero;
                    secondTouch = Vector3.zero;
                }
            } 
        }
            
    }
}