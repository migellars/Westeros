using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
    }

    protected BaseEntity(Guid id)
    {
        Id = new Guid();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; }
    public bool IsActive { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    [Timestamp]
    public byte[]? RowVersion { get; }
}