using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Domain.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
}