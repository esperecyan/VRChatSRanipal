using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using OscCore;
using ViveSR;
using ViveSR.anipal.Eye;

public class VRChatSRanipal : MonoBehaviour
{
    private static readonly int VRChatReceivingPort = 9000;
    private static readonly string VRChatReceivingAddressPrefix = "/avatar/parameters/";
    private static readonly float MaxEyeGazeValue = 0.6f;

    private static EyeData_v2 EyeData;

    private OscClient oscClient;
    private bool eyeDataCallbackRegisitered = false;
    private bool needSendingDefaultEyesValue = true;

    private void Start()
    {
        this.oscClient = new OscClient("127.0.0.1", VRChatSRanipal.VRChatReceivingPort);
    }

    private static void EyeCallback(ref EyeData_v2 eyeData)
    {
        VRChatSRanipal.EyeData = eyeData;
    }

    // Update is called once per frame
    private void Update()
    {
        // アイトラッキングが有効
        var eyeEnabled = SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING;

        if (eyeEnabled && !this.eyeDataCallbackRegisitered)
        {
            if (SRanipal_Eye_v2.WrapperRegisterEyeDataCallback(
               Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback)
            ) == (int)Error.NOT_INITIAL)
            {
                return;
            }
            this.eyeDataCallbackRegisitered = true;
        }

        // かつHMDをかぶっている
        eyeEnabled = eyeEnabled && VRChatSRanipal.EyeData.no_user;
        if (eyeEnabled || this.needSendingDefaultEyesValue)
        {
            var blinkLeft = 0f;
            var blinkRight = 0f;
            var gazeLeftX = 0.5f;
            var gazeLeftY = 0.5f;
            var gazeRightX = 0.5f;
            var gazeRightY = 0.5f;
            if (eyeEnabled && SRanipal_Eye_v2.GetEyeWeightings(out Dictionary<EyeShape_v2, float> shapes))
            {
                blinkLeft = shapes[EyeShape_v2.Eye_Left_Blink];
                blinkRight = shapes[EyeShape_v2.Eye_Right_Blink];
                gazeLeftX = Math.Clamp(shapes[EyeShape_v2.Eye_Left_Left] > 0
                    ? 0.5f - shapes[EyeShape_v2.Eye_Left_Left] / VRChatSRanipal.MaxEyeGazeValue / 2
                    : 0.5f + shapes[EyeShape_v2.Eye_Left_Right] / VRChatSRanipal.MaxEyeGazeValue / 2, 0, 1);
                gazeLeftY = Math.Clamp(shapes[EyeShape_v2.Eye_Left_Down] > 0
                    ? 0.5f - shapes[EyeShape_v2.Eye_Left_Down] / VRChatSRanipal.MaxEyeGazeValue / 2
                    : 0.5f + shapes[EyeShape_v2.Eye_Left_Up] / VRChatSRanipal.MaxEyeGazeValue / 2, 0, 1);
                gazeRightX = Math.Clamp(shapes[EyeShape_v2.Eye_Right_Left] > 0
                    ? 0.5f - shapes[EyeShape_v2.Eye_Right_Left] / VRChatSRanipal.MaxEyeGazeValue / 2
                    : 0.5f + shapes[EyeShape_v2.Eye_Right_Right] / VRChatSRanipal.MaxEyeGazeValue / 2, 0, 1);
                gazeRightY = Math.Clamp(shapes[EyeShape_v2.Eye_Right_Down] > 0
                    ? 0.5f - shapes[EyeShape_v2.Eye_Right_Down] / VRChatSRanipal.MaxEyeGazeValue / 2
                    : 0.5f + shapes[EyeShape_v2.Eye_Right_Up] / VRChatSRanipal.MaxEyeGazeValue / 2, 0, 1);

                this.needSendingDefaultEyesValue = true;
            }
            else
            {
                this.needSendingDefaultEyesValue = false;
            }

            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "BlinkLeft", blinkLeft);
            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "BlinkRight", blinkRight);
            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "GazeLeftX", gazeLeftX);
            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "GazeLeftY", gazeLeftY);
            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "GazeRightX", gazeRightX);
            this.oscClient.Send(VRChatSRanipal.VRChatReceivingAddressPrefix + "GazeRightY", gazeRightY);
        }
    }
}
