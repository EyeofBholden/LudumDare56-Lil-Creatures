using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Slider pFierceSlider;
    public Slider pCuteSlider;
    public Slider pDurpySlider;
    public Slider pCoolSlider;
    public Slider mFierceSlider;
    public Slider mCuteSlider;
    public Slider mDurpySlider;
    public Slider mCoolSlider;

    public List<TargetInfo> Targets = new List<TargetInfo>();
    public TargetInfo target;
    // Start is called before the first frame update
    public static TargetController instance;
    private void Awake(){
        instance = this;
    }
    void Start()
    {
        
        target = Targets[Random.Range(0,Targets.Count)];
    }
    void Update()
    {

    }
    public void generateNewTarget()
    {
        target = Targets[Random.Range(0, Targets.Count)];
    }
    public void changeFierceMeter(int mod)
    {
        if (mod > 0)
        {
            if (mFierceSlider.value > 0)
            {
                
                if (mFierceSlider.value < (mod * -1))
                {
                    pFierceSlider.value += mFierceSlider.value + mod;
                    mFierceSlider.value = 0;
                }
                else
                {
                    mFierceSlider.value -= mod;
                }
            }
            else
            {
                pFierceSlider.value += mod;
            }
        }
        if (mod < 0)
        {
            if (pFierceSlider.value > 0)
            {

                if (pFierceSlider.value < (mod * -1))
                {
                    mFierceSlider.value += pFierceSlider.value + mod;
                    pFierceSlider.value = 0;
                }
                else
                {
                    pFierceSlider.value += mod;
                }
            }
            else
            {
                mFierceSlider.value += mod * -1;
            }
        }
    }
    public void changeCuteMeter(int mod)
    {
        if (mod > 0)
        {
            if (mCuteSlider.value > 0)
            {

                if (mCuteSlider.value < (mod * -1))
                {
                    pCuteSlider.value += mCuteSlider.value + mod;
                    mCuteSlider.value = 0;
                }
                else
                {
                    mCuteSlider.value -= mod;
                }
            }
            else
            {
                pCuteSlider.value += mod;
            }
        }
        if (mod < 0)
        {
            if (pCuteSlider.value > 0)
            {

                if (pCuteSlider.value < (mod * -1))
                {
                    mFierceSlider.value += pCuteSlider.value + mod;
                    pCuteSlider.value = 0;
                }
                else
                {
                    pCuteSlider.value += mod;
                }
            }
            else
            {
                mCuteSlider.value += mod * -1;
            }
        }
    }
    public void changeDurpyMeter(int mod)
    {
        if (mod > 0)
        {
            if (mDurpySlider.value > 0)
            {

                if (mDurpySlider.value < (mod * -1))
                {
                    pCuteSlider.value += mDurpySlider.value + mod;
                    mDurpySlider.value = 0;
                }
                else
                {
                    mDurpySlider.value -= mod;
                }
            }
            else
            {
                pDurpySlider.value += mod;
            }
        }
        if (mod < 0)
        {
            if (pDurpySlider.value > 0)
            {

                if (pDurpySlider.value < (mod * -1))
                {
                    mFierceSlider.value += pDurpySlider.value + mod;
                    pDurpySlider.value = 0;
                }
                else
                {
                    pDurpySlider.value += mod;
                }
            }
            else
            {
                mDurpySlider.value += mod * -1;
            }
        }
    }
    public void changeCoolMeter(int mod)
    {
        if (mod > 0)
        {
            if (mCoolSlider.value > 0)
            {

                if (mCoolSlider.value < (mod * -1))
                {
                    pCuteSlider.value += mCoolSlider.value + mod;
                    mCoolSlider.value = 0;
                }
                else
                {
                    mCoolSlider.value -= mod;
                }
            }
            else
            {
                pCoolSlider.value += mod;
            }
        }
        if (mod < 0)
        {
            if (pCoolSlider.value > 0)
            {

                if (pCoolSlider.value < (mod * -1))
                {
                    mFierceSlider.value += pCoolSlider.value + mod;
                    pCoolSlider.value = 0;
                }
                else
                {
                    pCoolSlider.value += mod;
                }
            }
            else
            {
                mCoolSlider.value += (mod * -1);
            }
        }
    }
    public string WinCheck()
    {
        if ((pFierceSlider.value >= target.targetFierce && pCuteSlider.value >= target.targetCute && pDurpySlider.value >= target.targetDurpy && pCoolSlider.value >= target.targetCool) || pCoolSlider.value == 25 || pDurpySlider.value == 25 || pCuteSlider.value == 25 || pFierceSlider.value == 25)
        {
            return "adopt";
        }
        else if ((mFierceSlider.value >= target.targetFierce && mCuteSlider.value >= target.targetCute && mDurpySlider.value >= target.targetDurpy && mCoolSlider.value >= target.targetCool)|| mCoolSlider.value == 25 || mDurpySlider.value == 25 || mCuteSlider.value == 25 || mFierceSlider.value == 25)
        {
            mFierceSlider.value = 0;
            mCuteSlider.value = 0;
            mDurpySlider.value = 0;
            mCoolSlider.value = 0;
            return "foeAdopt";
        }
        else
        {
            return "no";
        }
    }

}
