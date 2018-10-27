using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouvementHandler : MonoBehaviour {
    public bool activateMouvementHandler;
    public float timeout;
    public Text countdownTextObject;

    public int movementInProgress;

    private float nextTimeout;

    private int lastMovementId = 0;
    private float getMovementTimeOut = 1;
    private float startTimeNewMovement;
    private float startTimeCountdown;
    private int nbSecLeft;
    private string countdownText = "Redémarrage de la détection dans ";

    public float countDownDelay;

    //variable pour la détection de mouvement
    public GameObject leftHand;
    public GameObject rightHand;
    public float sensitivity;

    private Vector3 lastPosRight;
    public Vector3 currPosRight;
    private Vector2 startPosRight;

    private Vector3 lastPosLeft;
    public Vector3 currPosLeft;
    private Vector2 startPosLeft;

    public int[] goingRight = { 0, 0, 0 };
    public int[] goingLeft = { 0, 0, 0 };

    public const int AXE_X = 0;
    public const int AXE_Y = 1;
    public const int AXE_Z = 2;

    private float startTime;

    // Use this for initialization
    void Start () {
        movementInProgress = 0;
        if (countdownTextObject != null)
            countdownTextObject.enabled = false;

        lastPosRight = rightHand.transform.position;
        lastPosLeft = leftHand.transform.position;
        nbSecLeft = -1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextTimeout + timeout) {
            movementInProgress = 0;
        }
        if (countdownTextObject != null && nbSecLeft >= 0) {
            //Debug.Log("NbSecLeft = " + nbSecLeft);
            if (startTimeCountdown + countDownDelay < Time.time) {
                //Debug.Log(1);
                nbSecLeft--;
                if (nbSecLeft < 0) {
                    countdownTextObject.enabled = false;
                    activateMouvementHandler = true;
                    return;
                }
                countdownTextObject.text = countdownText + nbSecLeft + " sec";
                startTimeCountdown = Time.time;
            }
        }/* else if (!activateMouvementHandler)
            Debug.Log("pas bon: " + nbSecLeft + " - ");*/

        currPosRight = rightHand.transform.position;
        currPosLeft = leftHand.transform.position;

        if (activateMouvementHandler)
            updateHands();

        lastPosRight = currPosRight;
        lastPosLeft = currPosLeft;
    }

    void updateHands()
    {
        //####RightHand####
        ArrayList currRightPosAxes = new ArrayList();
        currRightPosAxes.Add(currPosRight.x); currRightPosAxes.Add(currPosRight.y); currRightPosAxes.Add(currPosRight.z);
        ArrayList lastRightPosAxes = new ArrayList();
        lastRightPosAxes.Add(lastPosRight.x); lastRightPosAxes.Add(lastPosRight.y); lastRightPosAxes.Add(lastPosRight.z);

        for (int i = 0; i < 3; ++i) {
            float currPosAxe = (float)currRightPosAxes[i]; float lastPosAxe = (float)lastRightPosAxes[i];
            float diff = Mathf.Abs(currPosAxe - lastPosAxe);

            if (currPosAxe > lastPosAxe && diff >= sensitivity)
                goingRight[i] = 1;
            else if (currPosAxe < lastPosAxe && diff >= sensitivity)
                goingRight[i] = -1;
        }

        //####LeftHand####
        ArrayList currLeftPosAxes = new ArrayList();
        currLeftPosAxes.Add(currPosLeft.x); currLeftPosAxes.Add(currPosLeft.y); currLeftPosAxes.Add(currPosLeft.z);
        ArrayList lastLeftPosAxes = new ArrayList();
        lastLeftPosAxes.Add(lastPosLeft.x); lastLeftPosAxes.Add(lastPosLeft.y); lastLeftPosAxes.Add(lastPosLeft.z);

        for (int i = 0; i < 3; ++i) {
            float currPosAxe = (float)currLeftPosAxes[i]; float lastPosAxe = (float)lastLeftPosAxes[i];
            float diff = Mathf.Abs(currPosAxe - lastPosAxe);

            if (currPosAxe > lastPosAxe && diff >= sensitivity) {
                goingLeft[i] = 1;
            } else if (currPosAxe < lastPosAxe && diff >= sensitivity) {
                goingLeft[i] = -1;
            }
        }
    }

    public bool startMovement(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == 0) {
            //nextTimeout = Time.time + timeout;
            movementInProgress = movementIndex;
            return true;
        }

        return false;
    }

    public bool isMyMovementInProgress(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == movementIndex)
        { 
            return true;
        }

        return false;
    }

    public bool isAMovementInProgress()
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress != 0)
        {
            return true;
        }

        return false;
    }

    public bool endMovement(int movementIndex, bool success)
    {
        /*if (!activateMouvementHandler) {
            Debug.Log("no movement handler");
            return false;
        }*/

        //if (movementInProgress == movementIndex) {
            //Debug.Log("ok for now");
            movementInProgress = 0;
            setLastMovement(movementIndex);
            reInitHandsMove();
            if (countdownTextObject != null) {
                activateMouvementHandler = false;
                startDetectionCountDown(3);
            }
            return true;
        /*}

        Debug.Log("just nothing work");
        return false;*/
    }

    public bool endMovement(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == movementIndex) {
            movementInProgress = 0;
            reInitHandsMove();
            if (countdownTextObject != null) {
                activateMouvementHandler = false;
                startDetectionCountDown(3);
            }
            return true;
        }

        return false;
    }

    public void startTimeoutCountdown()
    {
        nextTimeout = Time.time;
    }

    public bool GetMouvement(int movementId)
    {
        if (lastMovementId == movementId && startTimeNewMovement + getMovementTimeOut > Time.time)
            return true;
        return false;
    }

    public bool GetMouvementTimeout()
    {
        if (nextTimeout + timeout < Time.time) {
            nextTimeout = 0;
            return true;
        }
        return false;
    }

    private void setLastMovement(int movementId)
    {
        lastMovementId = movementId;
        startTimeNewMovement = Time.time;
    }

    public void startDetectionCountDown(int sec)
    {
        if (countdownTextObject == null)
            return;
        startTimeCountdown = Time.time;
        nbSecLeft = sec;
        countdownTextObject.text = countdownText + nbSecLeft + " sec";
        countdownTextObject.enabled = true;
    }

    private void reInitHandsMove()
    {
        goingRight[AXE_X] = 0; goingRight[AXE_Y] = 0; goingRight[AXE_Z] = 0;
        goingLeft[AXE_X] = 0; goingLeft[AXE_Y] = 0; goingLeft[AXE_Z] = 0;
    }
}
