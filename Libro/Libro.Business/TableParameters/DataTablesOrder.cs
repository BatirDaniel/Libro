﻿using System.Runtime.Serialization;

namespace Libro.Business.TableParameters
{
    [Serializable]
    [DataContract]
    public class DataTablesOrder
    {
        [DataMember(Name = "column")]
        public int Column { get; set; }

        [DataMember(Name = "dir")]
        public string Dir { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
