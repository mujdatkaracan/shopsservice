using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShopsService.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity<string>
    {
        [System.Xml.Serialization.XmlIgnore]
        [BsonElement("_id")]
        [BsonId(IdGenerator = typeof(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } 

        public bool IsDeleted { get; set; }
    }
}
