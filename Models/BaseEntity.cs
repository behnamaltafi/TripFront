// مدل‌های پایه
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Serializable]
public abstract class BaseEntity<T> : BaseEntity
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Column("ID")]
    [Display(Name = "شناسه")]
    public T Id { get; set; }
}

public abstract class BaseEntity
{

}
