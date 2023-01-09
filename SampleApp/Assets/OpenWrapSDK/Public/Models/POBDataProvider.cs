#if UNITY_IOS || UNITY_ANDROID
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
* information that is confidential and/or proprietary, and is a trade secret, of  PubMatic.   ANY REPRODUCTION, MODIFICATION, DISTRIBUTION,  PERFORMANCE,
* OR DISPLAY OF OR THROUGH USE  OF THIS  SOURCE CODE  WITHOUT  THE EXPRESS WRITTEN CONSENT OF PUBMATIC IS STRICTLY PROHIBITED, AND IN VIOLATION OF APPLICABLE
* LAWS AND INTERNATIONAL TREATIES.  THE RECEIPT OR POSSESSION OF  THIS SOURCE CODE AND/OR RELATED INFORMATION DOES NOT CONVEY OR IMPLY ANY RIGHTS
* TO REPRODUCE, DISCLOSE OR DISTRIBUTE ITS CONTENTS, OR TO MANUFACTURE, USE, OR SELL ANYTHING THAT IT  MAY DESCRIBE, IN WHOLE OR IN PART.
*/

using System.Collections.Generic;
using System.Linq;
using OpenWrapSDK.Common;
using UnityEngine;

namespace OpenWrapSDK
{
    /// <summary>
    /// RTB's 'Data' object containing Segment details
    /// </summary>
    public class POBDataProvider
    {
        #region Private members
        // iOS/Android specific data provider client
        internal IPOBDataProviderClient dataProviderClient;

        private Dictionary<string, string> extension;
        private string identifier;
        private string name;
        private int segTax;
        private List<POBSegment> segments;
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Constructor with data provider name
        /// </summary>
        /// <param name="name">Data provider's name</param>
        public POBDataProvider(string name)
        {
            this.name = name;
            segments = new List<POBSegment>();
            extension = new Dictionary<string, string>();
#if UNITY_IOS
            dataProviderClient = new iOS.POBDataProviderClient(name, null);
#else
            dataProviderClient = new Android.POBDataProviderClient(name);
#endif
        }

        /// <summary>
        /// Constructor with data provider identifier & name
        /// </summary>
        /// <param name="name">Data provider's name</param>
        /// <param name="identifier">Data provider's id</param>
        public POBDataProvider(string name, string identifier)
        {
            this.name = name;
            this.identifier = identifier;
            segments = new List<POBSegment>();
            extension = new Dictionary<string, string>();
#if UNITY_IOS
            dataProviderClient = new iOS.POBDataProviderClient(name, identifier);
#else
            dataProviderClient = new Android.POBDataProviderClient(name, identifier);
#endif
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~POBDataProvider()
        {
            if (dataProviderClient != null)
            {
                dataProviderClient.Destroy();
                dataProviderClient = null;
            }
            // Clear data providers list.
            if (segments != null)
            {
                segments.Clear();
                segments = null;
            }
            if(extension != null)
            {
                extension.Clear();
                extension = null;
            }

        }
        #endregion

        #region Public APIs
        /// <summary>
        /// Data provider's identifier
        /// </summary>
        public string Identifier
        {
            get
            {
                return identifier;
            }
        }

        /// <summary>
        /// Data provider's name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// segment taxonomy id. Reference: https://github.com/InteractiveAdvertisingBureau/AdCOM/blob/master/AdCOM%20v1.0%20FINAL.md#list--category-taxonomies
        /// </summary>
        public int SegTax
        {
            get
            {
                return segTax;
            }
            set
            {
                segTax = value;
                if (dataProviderClient != null)
                {
                    dataProviderClient.SegTax = value;
                }
            }
        }

        /// <summary>
        /// RTB extension object for this data
        /// </summary>
        public Dictionary<string, string> Extension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = Extension;
                if (dataProviderClient != null)
                {
                    dataProviderClient.Extension = value;
                }
            }
        }

        /// <summary>
        /// Adds a segment details to data object
        /// </summary>
        /// <param name="segmentClient">Segment client details to be added to data object</param>
        public void AddSegment(POBSegment segment)
        {
            if (segment != null && !POBUtils.IsNullOrEmpty(segment.Identifier))
            {
                bool isDuplicate = segments.Any(seg => seg.Identifier.Equals(segment.Identifier));
                if (!isDuplicate && dataProviderClient != null)
                {
                    dataProviderClient.AddSegment(segment);

                    // Add C# segment locally
                    segments.Add(segment);
                }
                else
                {
                    Debug.Log("POBDataProvider : segments with duplicate id not allowed");
                }
            }
            else
            {
                Debug.Log("POBDataProvider : segment is null or required fields are not available.");
            }
        }

        /// <summary>
        /// Removes a segment details from data object
        /// </summary>
        /// <param name="identifier">Identifier for which a segment is to be removed</param>
        public void RemoveSegment(string identifier)
        {
            if (identifier != null)
            {
                if (segments.Count > 0)
                {
                    POBSegment segment = segments.Find(seg => seg.Identifier.Equals(identifier));
                    if (segment != null)
                    {
                        segments.Remove(segment);

                        if (dataProviderClient != null)
                        {
                            dataProviderClient.RemoveSegment(segment);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes all segments from the data object
        /// </summary>
        public void RemoveAllSegments()
        {
            segments.Clear();
            if (dataProviderClient != null)
            {
                dataProviderClient.RemoveAllSegments();
            }
        }

        /// <summary>
        /// Returns all the segments associated with this data object
        /// </summary>
        /// <returns>Array of POBSegmentsClients</returns>
        public List<POBSegment> GetSegments()
        {
            return segments;
        }

        /// <summary>
        /// Returns a segment for given id
        /// </summary>
        /// <param name="id">segment identifier</param>
        /// <returns>Reference of the POBSegmentClient object associated with the given identifier</returns>
        public POBSegment GetSegment(string identifier)
        {
            if (identifier != null && segments!= null && segments.Count > 0)
            {
                return segments.Find(segment => segment.Identifier.Equals(identifier));
            }
            return null;
        }
        #endregion
    }
}
#endif
