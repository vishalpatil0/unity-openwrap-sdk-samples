#if UNITY_ANDROID
/*
* PubMatic Inc. ("PubMatic") CONFIDENTIAL
* Unpublished Copyright (c) 2006-2022 PubMatic, All Rights Reserved.
*
* NOTICE:  All information contained herein is, and remains the property of PubMatic. The intellectual and technical concepts contained
* herein are proprietary to PubMatic and may be covered by U.S. and Foreign Patents, patents in process, and are protected by trade secret or copyright law.
* Dissemination of this information or reproduction of this material is strictly forbidden unless prior written permission is obtained
* from PubMatic.  Access to the source code contained herein is hereby forbidden to anyone except current PubMatic employees, managers or contractors who have executed
* Confidentiality and Non-disclosure agreements explicitly covering such access or to such other persons whom are directly authorized by PubMatic to access the source code and are subject to confidentiality and nondisclosure obligations with respect to the source code.
*
* The copyright notice above does not evidence any actual or intended publication or disclosure  of  this source code, which includes
* information that is confidential and/or proprietary, and is a trade secret, of  PubMatic.   ANY REPRODUCTION, MODIFICATION, DISTRIBUTION, PUBLIC  PERFORMANCE,
* OR PUBLIC DISPLAY OF OR THROUGH USE  OF THIS  SOURCE CODE  WITHOUT  THE EXPRESS WRITTEN CONSENT OF PUBMATIC IS STRICTLY PROHIBITED, AND IN VIOLATION OF APPLICABLE
* LAWS AND INTERNATIONAL TREATIES.  THE RECEIPT OR POSSESSION OF  THIS SOURCE CODE AND/OR RELATED INFORMATION DOES NOT CONVEY OR IMPLY ANY RIGHTS
* TO REPRODUCE, DISCLOSE OR DISTRIBUTE ITS CONTENTS, OR TO MANUFACTURE, USE, OR SELL ANYTHING THAT IT  MAY DESCRIBE, IN WHOLE OR IN PART.
*/

using UnityEngine;
using System;

namespace OpenWrapSDK.Android
{
    /// <summary>
    /// Android client to set global parameters on OpenWrap SDK.
    /// </summary>
    public static class OpenWrapSDKClient
    {
        private static readonly AndroidJavaClass OpenWrapSDKClass = new AndroidJavaClass(POBConstants.OpenWrapSDKClassName);

        private static POBUserInfo internalUserInfo;

        /// <summary>
        /// Returns the OpenWrap SDK's native platform version.
        /// </summary>
        /// <returns>OpenWrap SDK's native platform version</returns>
        public static string GetVersion()
        {
            return OpenWrapSDKClass.CallStatic<string>("getVersion");
        }

        /// <summary>
        /// Indicates whether Android advertisement ID should be sent in the request or not.
        /// By default advertisement ID will be used.
        /// Possible values are:
        /// true : Advertisement id will be sent in the request.
        /// false : Advertisement id will not be sent in the request.
        /// 
        /// </summary>
        /// <param name="allow">state of advertisement id usage</param>
        public static void AllowAdvertisingId(bool allow)
        {
            OpenWrapSDKClass.CallStatic("allowAdvertisingId", allow);
        }

        /// <summary>
        /// Used to enable/disable location access.
        /// This value decides whether the OpenWrap SDK should access device location using
        /// Core Location APIs to serve location-based ads.When set to false, the SDK will not attempt
        /// to access device location.When set to true, the SDK will periodically try to fetch location
        /// efficiently.
        /// Note that, this only occurs if location services are enabled and the user has already
        /// authorized the use of location services for the application. The OpenWrap SDK never asks
        /// permission to use location services by itself.
        /// <p>
        /// The default value is true.
        /// </summary>
        /// <param name="allow">enable or disable location access behavior</param>
        public static void AllowLocationAccess(bool allow)
        {
            OpenWrapSDKClass.CallStatic("allowLocationAccess", allow);
        }

        /// <summary>
        /// Set the CCPA compliant string, it helps publisher toward compliance with the California Consumer Privacy Act (CCPA).
        /// For more details refer https://www.iab.com/guidelines/ccpa-framework/
        /// Make sure that the string value you use is compliant with the IAB Specification, refer
        /// https://iabtechlab.com/wp-content/uploads/2019/11/U.S.-Privacy-String-v1.0-IAB-Tech-Lab.pdf
        /// <p>
        /// If this is not set, SDK looks for app's default SharedPreference with key 'IABUSPrivacy_String'
        /// If CCPA is applied through both options, the SDK will honour only API property.
        /// If both are not set then CCPA parameter is omitted from an ad request.
        /// </summary>
        /// <param name="ccpaString">is the CCPA compliant string</param>
        public static void SetCCPA(string ccpaString)
        {
            OpenWrapSDKClass.CallStatic("setCCPA", ccpaString);
        }

        /// <summary>
        /// Indicates whether the visitor is COPPA-specific or not.
        /// For COPPA(Children's Online Privacy Protection Act) compliance, if the visitor's age is
        /// below 13, then such visitors should not be served targeted ads.
        /// Possible options are:
        /// false - Indicates that the visitor is not COPPA-specific and can be served targeted ads.
        /// true - Indicates that the visitor is COPPA-specific and should be served only COPPA-compliant ads.
        /// </summary>
        /// <param name="enable">Visitor state for COPPA compliance.</param>
        public static void SetCoppaEnabled(bool enable)
        {
            OpenWrapSDKClass.CallStatic("setCoppa", enable);
        }

        /// <summary>
        /// Sets GDPR consent string, A valid Base64 encoded consent string as per
        /// https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework.
        /// The user consent string is optional, but highly recommended if the request is subject to GDPR
        /// regulations(i.e.gdpr = true). The default sense of consent under GDPR is "opt-out" and
        /// as such, an omitted consent string in a request subject to GDPR would be interpreted as
        /// equivalent to the user fully opting out of all defined purposes for data use by all parties.
        /// </summary>
        /// <param name="gdprConsent">consent string to convey user consent when GDPR regulations are in effect.</param>
        public static void SetGDPRConsent(string gdprConsent)
        {
            OpenWrapSDKClass.CallStatic("setGDPRConsent", gdprConsent);
        }

        /// <summary>
        /// Sets GDPR compliance, it indicates whether or not the ad request is GDPR(General Data Protection Regulation) compliant.
        /// </summary>
        /// <param name="enable">
        /// boolean value
        /// - true : indicates GDPR compliant requests
        /// - false : indicates that the request is not GDPR compliant
        /// By default, this parameter is omitted in the ad request, indicating Unknown.
        /// </param>
        public static void SetGDPREnabled(bool enable)
        {
            OpenWrapSDKClass.CallStatic("setGDPREnabled", enable);
        }

        /// <summary>
        /// Sets user's location and its source. It is useful in delivering geographically relevant ads.
        /// <p>
        /// If your application is already accessing the device location, it is highly recommended to
        /// set the location coordinates inferred from the device GPS.If you are inferring location
        /// from any other source, make sure you set the appropriate location source.
        /// </summary>
        /// <param name="longitude">ongitude of the location</param>
        /// <param name="latitude">latitude of the location</param>
        /// <param name="source">location source</param>
        public static void SetLocation(double longitude, double latitude, POBLocSource source)
        {
            AndroidJavaObject locationSourceJavaObject = POBAndroidUtils.ConvertLocationSourceToJavaObject(source);
            AndroidJavaObject locationJavaObject = new AndroidJavaObject(POBConstants.POBLocationClassName, locationSourceJavaObject, latitude, longitude);
            OpenWrapSDKClass.CallStatic("setLocation", locationJavaObject);
        }

        /// <summary>
        /// Sets log level across all ad formats. Default log level is LogLevel.Warn.
        /// For more details refer <seealso cref="POBSDKLogLevel"/>
        /// </summary>
        /// <param name="logLevel"></param>
        public static void SetLogLevel(POBSDKLogLevel logLevel)
        {
            AndroidJavaObject logLevelJavaObject = POBAndroidUtils.ConvertLogLevelToJavaObject(logLevel);
            OpenWrapSDKClass.CallStatic("setLogLevel", logLevelJavaObject);
        }

        /// <summary>
        /// Enable/disable secure ad calls.
        /// <p>
        /// By default, OpenWrap SDK initiates secure ad calls from an application to the ad server and
        /// delivers only secure ads.You can allow non secure ads by passing false to this method.
        /// </summary>
        /// <param name="enable">false for disable secure creative mode. Default is set to true.</param>
        public static void SetSSLEnabled(bool enable)
        {
            OpenWrapSDKClass.CallStatic("setSSLEnabled", enable);
        }

        /// <summary>
        /// Tells OpenWrap SDK to use internal SDK browser, instead of the default device browser,
        /// for opening landing pages when the user clicks on an ad.
        /// By default, use of internal browser is enabled.If disabled by setting it to false, the
        /// landing page will gets opened in the default browser set by the user in device.
        /// </summary>
        /// <param name="use">boolean value that enables/disables the use of internal browser.</param>
        public static void SetUseInternalBrowser(bool use)
        {
            OpenWrapSDKClass.CallStatic("setUseInternalBrowser", use);
        }

        /// <summary>
        /// Sets Application information, which contains various attributes about app, such as
        /// application category, store URL, domain, etc for more relevant ads.
        /// </summary>
        /// <param name="applicationInfo">Instance of POBApplicationInfo class with required application details</param>
        public static void SetApplicationInfo(POBApplicationInfo applicationInfo)
        {
            AndroidJavaObject appInfoObject = new AndroidJavaObject(POBConstants.POBApplicationInfoClassName);
            appInfoObject.Call("setKeywords", applicationInfo.Keywords);
            appInfoObject.Call("setDomain", applicationInfo.Domain);
            if(applicationInfo.StoreURL != null) { 
                AndroidJavaObject storeURLObject = new AndroidJavaObject("java.net.URL", applicationInfo.StoreURL.ToString());
                appInfoObject.Call("setStoreURL", storeURLObject);
            }
            if(applicationInfo.Paid != POBBool.Unknown)
            {
                appInfoObject.Call("setPaid", Convert.ToBoolean(applicationInfo.Paid));
            }
            appInfoObject.Call("setCategories", applicationInfo.Categories);
            OpenWrapSDKClass.CallStatic("setApplicationInfo", appInfoObject);
        }

        /// <summary>
        /// Gets Application info set by publisher to OpenWrap SDK
        /// </summary>
        /// <returns>instance of POBApplicationInfo class</returns>
        public static POBApplicationInfo GetApplicationInfo()
        {
            POBApplicationInfo appInfo = null;
            AndroidJavaObject appInfoObject =  OpenWrapSDKClass.CallStatic<AndroidJavaObject>("getApplicationInfo");
            if (appInfoObject != null)
            {
                AndroidJavaObject storeURLObject = appInfoObject.Call<AndroidJavaObject>("getStoreURL");
                appInfo = new POBApplicationInfo();
                appInfo.Keywords = appInfoObject.Call<string>("getKeywords");
                appInfo.Domain = appInfoObject.Call<string>("getDomain");
                if (storeURLObject != null)
                {
                    appInfo.StoreURL = new Uri(storeURLObject.Call<string>("toString"));
                }
                if(appInfoObject.Call<AndroidJavaObject>("isPaid") != null)
                {
                    appInfo.Paid = (POBBool)Convert.ToInt32(appInfoObject.Call<bool>("isPaid"));
                }
                appInfo.Categories = appInfoObject.Call<string>("getCategories");
                
            }
            return appInfo;
        }

        /// <summary>
        /// Sets user information, such as birth year, gender, region, etc for more relevant ads.
        /// </summary>
        /// <param name="userInfo">Instance of POBUserInfo class with required user details</param>
        public static void SetUserInfo(POBUserInfo userInfo)
        {
            if(userInfo != null && userInfo.client != null)
            {
                AndroidJavaObject userInfoObject = userInfo.client.GetNativeObject();
                if(userInfoObject != null)
                {
                   internalUserInfo = userInfo;
                   OpenWrapSDKClass.CallStatic("setUserInfo", userInfoObject);
                }
            }
        }

        /// <summary>
        /// Gets user info set by publisher to OpenWrap SDK
        /// </summary>
        /// <returns>instance of POBUSerInfo class</returns>
        public static POBUserInfo GetUserInfo()
        {
            return internalUserInfo;
        }

        /// <summary>
        /// Add the External user id /Data Partner ids which helps publisher in better user targeting
        /// </summary>
        /// <param name="externalUserId">Instance of POBExternalUserId</param>
        public static void AddExternalUserId(POBExternalUserId externalUserId)
        {
            if (externalUserId != null)
            {
                OpenWrapSDKClass.CallStatic("addExternalUserId", externalUserId.externalUserIdClient.GetNativeObject());
                
            }
        }

        /// <summary>
        /// Removes the external user ids of a particular source
        /// </summary>
        public static void RemoveExternalUserIds(string source) {
            OpenWrapSDKClass.CallStatic("removeExternalUserIds", source);
        }

        /// <summary>
        /// Remove all external user ids
        /// </summary>
        public static void RemoveAllExternalUserIds() {
            OpenWrapSDKClass.CallStatic("removeAllExternalUserIds");
        }
    }
}
#endif