/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.V1.Model
{
    /// <summary>
    /// An object describing a contextual entity mention.
    /// </summary>
    public class EntityMention
    {
        /// <summary>
        /// The text of the user input example.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The name of the intent.
        /// </summary>
        [JsonProperty("intent", NullValueHandling = NullValueHandling.Ignore)]
        public string Intent { get; set; }
        /// <summary>
        /// An array of zero-based character offsets that indicate where the entity mentions begin and end in the input
        /// text.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<long?> Location { get; set; }
    }
}
