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

using System.Collections.Generic;
using UnityEngine;

namespace OpenWrapSDK.Android
{
    /// <summary>
    /// Android client for POBBid class, which provides the bid information. It will be common across all the ad formats in Android.
    /// </summary>
    internal class POBBidClient : IPOBBid
    {
        private AndroidJavaObject bidObject;


        /// <summary>
        /// Constructor of POBBid Client
        /// </summary>
        /// <param name="javaObject">AndroidJavaObject of POBBid</param>
        internal POBBidClient(AndroidJavaObject javaObject)
        {
            bidObject = javaObject;   
        }

        /// <summary>
        ///  Returns creative id of bid
        /// </summary>
        /// <returns>creative id</returns>
        public string GetCreativeId()
        {
            return bidObject.Call<string>("getCreativeId");
        }

        /// <summary>
        /// Returns creative tag of bid
        /// </summary>
        /// <returns>creative tag</returns>
        public string GetCreativeTag()
        {
            return bidObject.Call<string>("getCreative");
        }

        /// <summary>
        /// Returns creative type of bid
        /// </summary>
        /// <returns>creative type</returns>
        public string GetCreativeType()
        {
            return bidObject.Call<string>("getCreativeType");
        }

        /// <summary>
        /// Returns deal id of bid
        /// </summary>
        /// <returns>deal id</returns>
        public string GetDealId()
        {
            return bidObject.Call<string>("getDealId");
        }

        /// <summary>
        /// Returns if bid has won or not
        /// </summary>
        /// <returns>boolean vanlue if bid win</returns>
        public bool GetHasWon()
        {
            return bidObject.Call<bool>("hasWon");
        }

        /// <summary>
        /// Returns height of creative of bid
        /// </summary>
        /// <returns>height</returns>
        public int GetHeight()
        {
            return bidObject.Call<int>("getHeight");
        }

        /// <summary>
        /// Returns the name of the winning partner
        /// </summary>
        /// <returns>Name of the winning partner</returns>
        public string GetPartner()
        {
            return bidObject.Call<string>("getPartnerName");
        }

        /// <summary>
        /// Returns Net Price/bid value
        /// This method is updated to return net price from OW SDK v2.4.0.
        /// </summary>
        /// <returns>net ecm/price</returns>
        public double GetPrice()
        {
            return bidObject.Call<double>("getPrice");
        }

        /// <summary>
        /// Returns the partner id of bid 
        /// </summary>
        /// <returns>partner id</returns>
        public string GetPubmaticPartnerId()
        {
            return bidObject.Call<string>("getPartnerId");
        }

        /// <summary>
        /// Returns winning  status of bid
        /// </summary>
        /// <returns>status</returns>
        public int GetStatus()
        {
            return bidObject.Call<int>("getStatus");
        }

        /// <summary>
        /// Returns targeting info of bid
        /// </summary>
        /// <returns>targeting info</returns>
        public Dictionary<string, string> GetTargetingInfo()
        {
            AndroidJavaObject targetingInfo = bidObject.Call<AndroidJavaObject>("getTargetingInfo");
            Dictionary<string, string> targetingMap = POBAndroidUtils.ConvertJavaMapToDictionary<string,string>(targetingInfo);

            return targetingMap;

        }

        /// <summary>
        /// Returns width of bid creative
        /// </summary>
        /// <returns>width</returns>
        public int GetWidth()
        {
            return bidObject.Call<int>("getWidth");
        }

        /// <summary>
        /// Returns if bid expired or not
        /// </summary>
        /// <returns>bid expiry state</returns>
        public bool IsExpired()
        {
            return bidObject.Call<bool>("isExpired");
        }

        /// <summary>
        /// Sets the current bid as winnging bid
        /// </summary>
        /// <param name="hasWon">bid win status</param>
        public void SetHasWon(bool hasWon)
        {
            bidObject.Call("setHasWon",hasWon);
        }

        /// <summary>
        /// Return id of bid
        /// </summary>
        /// <returns>bid id</returns>
        public string GetBidId()
        {
            return bidObject.Call<string>("getId");
        }

        /// <summary>
        /// Returns gross price of bid 
        /// </summary>
        /// <returns>gross price</returns>
        public double GetGrossPrice()
        {
            return bidObject.Call<double>("getGrossPrice");
        }

        public override string ToString()
        {
            return bidObject.Call<string>("toString");
        }
    }
}
#endif