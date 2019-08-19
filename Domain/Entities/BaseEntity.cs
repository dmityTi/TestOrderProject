using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Key]
        public long Id { get; set; }
    }
}