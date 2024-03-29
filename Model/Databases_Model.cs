﻿using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class DatabasesModel
    {
        [BsonElement("name")] public string name { get; set; }

        [BsonElement("sizeOnDisk")] public double size { get; set; }

        [BsonElement("empty")] public bool empty { get; set; }
    }
}