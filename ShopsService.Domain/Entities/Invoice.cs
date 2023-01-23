using MongoDB.Bson.Serialization.Attributes;
using ShopsService.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopsService.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [BsonIgnoreExtraElements]
    public class Invoice:BaseEntity
    {
        [Required]
        public string CustomerId { get; set; } = null!;
         
        public decimal InvoiceAmount { get; set; }
         
        public decimal DiscountAmount { get; set; }
    }
}
