﻿using System;
using System.Runtime.Serialization;

namespace Musicalog.Service.Models
{
    [DataContract]
    public class DeleteAlbumRequestModel
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}