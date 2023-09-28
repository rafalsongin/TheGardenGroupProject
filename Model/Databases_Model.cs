using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public abstract class DatabasesModel
    {
        [BsonElement("name")]
        public string name { get; set; }
        
        [BsonElement("sizeOnDisk")]
        public double size { get; set; }
        
        [BsonElement("empty")]
        public bool empty { get; set; }
        
        
    }
}
