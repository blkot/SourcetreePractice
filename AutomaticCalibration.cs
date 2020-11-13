using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

public class AutomaticCalibration : MonoBehaviour
{
    public Transform PointOfInterestA;
    public Transform PointOfInterestB;

    public GameObject calibrationMarkerA;
    public GameObject calibrationMarkerB;

    [SerializeField]
    WorldAnchorManager worldAnchorManager;
    //WorldAnchorStore anchorStore;
    //WorldAnchor anchor;
    

    bool canCalibrate = false;

    [SerializeField]
    private GameObject CalibratedObj;

    [SerializeField]
    GameObject marker;
    //Transform pin;

    //Vector3 POINear = new Vector3(0, 0, -2);
    //Vector3 POIFar = new Vector3(0, 0, 2);

    //public string ObjectAnchorStoreName;




    // Start is called before the first frame update
    void Start()
    {
        //marker.SetActive(false);
        //WorldAnchorStore.GetAsync(AnchorStoreReady);
        //worldAnchorManager = new WorldAnchorManager();
        
        //TODO. set to false on load;

        canCalibrate = (CalibratedObj == null) ? false : true;



        //vectorPOI = PointOfInterestB.position - PointOfInterestA.position;
        if (marker == null)
        {
            Debug.Log("no MarkerPrefab.");
            return;
        }


    }

    private void Awake()
    {
        //WorldAnchorStore.GetAsync(AnchorStoreReady);
        
        //WorldAnchor anchor = anchorStore.Load("StagePin", CalibratedObj);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //InstantiateCalibrationPin();

        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            //CalibrateSceneObjs();
            CalibrateBySinglePin();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            FinishCalibration();

        }
    }

    void RetractPin()
    {
        
    }
    

    

    // Update is called once per frame
    

    public void FinishCalibration()
    {
        pin.gameObject.SetActive(false);
        Destroy(pin.gameObject);

        worldAnchorManager.AttachAnchor(CalibratedObj, "SceneAnchor");

        //AddAnchor();
        //throw new NotImplementedException();
    }

    public void CalibrateBySinglePin()
    {
        worldAnchorManager.RemoveAnchor(CalibratedObj);
        
        
        if (CalibratedObj == null)
        {
            CalibratedObj = GameObject.Find("StageRoot");
        }
        Transform stage3 = GameObject.Find("stage3").transform;
        Debug.LogWarning("Found stage3");

        //transition
        Vector3 offset = marker.transform.position - stage3.position;
        Debug.LogWarning("OFFSET = " + offset);


        CalibratedObj.transform.Translate(offset);

        if (!canCalibrate)
        {
            return;
        }


        //rotation



    }

    

    

    

    


    /// <summary>
    /// instantiate calibration pin to move;
    /// </summary>
    /// <returns></returns>
    public void InstantiateCalibrationPin()
    {
        if (marker = null)
        {
            //return false;
        }
        Transform handmenu = GameObject.Find("HandMenuRoot").transform;
        //GameObject go= GameObject.Instantiate<GameObject>(marker, handmenu, true) as GameObject;
        //go.SetActive(true);
        //go.transform.position = handmenu.position + new Vector3(0, 0.04f, 0);
        //pin.transform = marker.transform;
        //GameObject go = GameObject.Instantiate<GameObject>(markerPrefab, handmenu, true) as GameObject;
        //ObjectManipulator objectManipulator = Instantiate<ObjectManipulator>(markerPrefab);
        //pin = objectManipulator.gameObject.transform;
        //go.AddComponent<>
        //return true;
    }



    /// <summary>
    /// double sphere pin alignment
    /// </summary>
    public void CalibrateByDoublePin()
    {
        if (!canCalibrate)
        {
            return;    

        }

        //scale first cuz the scale pivot is undetermined.
        Vector3 vectorPOI = PointOfInterestB.position - PointOfInterestA.position;
        Vector3 vectorCali = calibrationMarkerB.transform.position - calibrationMarkerA.transform.position;
        Vector3 normal = Vector3.Cross(vectorPOI, vectorCali);

        float scalefactor = vectorCali.magnitude / vectorPOI.magnitude;
        Debug.Log(scalefactor);
        CalibratedObj.transform.localScale *= scalefactor;

        //recalculate vectors for translation and rotation
        vectorPOI = PointOfInterestB.position - PointOfInterestA.position;
        vectorCali = calibrationMarkerB.transform.position - calibrationMarkerA.transform.position;

        //rotation second
        float rotateDeg = Vector3.Angle(vectorPOI, vectorCali);
        //float rotateDegV2 = Vector2.Angle(new Vector2(vectorPOI.x, vectorPOI.z), new Vector2(vectorCali.x, vectorCali.z));
        //Debug.Log(rotateDegV2);
        CalibratedObj.transform.RotateAround(CalibratedObj.transform.rotation.eulerAngles, normal, rotateDeg);







        //tranlation last
        Vector3 translation = calibrationMarkerA.transform.position - PointOfInterestA.position;
        Debug.Log(translation);
        CalibratedObj.transform.position += translation;
    }


    


    

}
