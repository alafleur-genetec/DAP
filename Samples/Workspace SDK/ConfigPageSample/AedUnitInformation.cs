﻿// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

namespace Genetec.Dap.CodeSamples
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    [DataContract(Namespace = "")]
    public class AedUnitInformation
    {
        private static readonly DataContractSerializer s_serializer = new(typeof(AedUnitInformation));

        [DataMember] 
        public DateTime LastInspectionDate { get; set; }

        [DataMember] 
        public DateTime NextScheduledMaintenance { get; set; }

        [DataMember] 
        public DateTime BatteryExpirationDate { get; set; }

        [DataMember] 
        public DateTime PadExpirationDate { get; set; }

        // Deserialize the data from an XML string
        public static AedUnitInformation Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new AedUnitInformation();
            }

            using var stringReader = new StringReader(data);
            using var xmlReader = XmlReader.Create(stringReader);
            return (AedUnitInformation)s_serializer.ReadObject(xmlReader);
        }

        // Serialize the data to an XML string
        public string Serialize()
        {
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);
            s_serializer.WriteObject(xmlWriter, this);
            xmlWriter.Flush();
            return stringWriter.ToString();
        }
    }
}