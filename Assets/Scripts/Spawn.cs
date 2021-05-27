using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Spawn : MonoBehaviour
{
    public GameObject towerPrefab;
    public Material capMaterial;
    GameObject spawnedObj;
     ARRaycastManager arRaycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // タップした場所に生成
        if (Input.touchCount > 0)
        {
            if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if (spawnedObj == null)
                {
                    // 配置する
                    spawnedObj = Instantiate(towerPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    // 切り取る
                    spawnedObj = BLINDED_AM_ME.MeshCut.Cut(spawnedObj, transform.position, transform.right, capMaterial)[0];
                    // 移動させる
//                     spawnedObj.transform.position = hitPose.position;
                }
            }

        }
    }
}