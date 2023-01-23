using MongoDB.Bson.Serialization.Attributes;
using ShopsService.Domain.Entities.Base;
using ShopsService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopsService.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [BsonIgnoreExtraElements]
    public class Customer:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(25)]
        public string Email { get; set; } = null!;
         
        public CustomerType CustomerType { get; set; }
    }
}
