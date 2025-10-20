using EVDMS.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class CreatedCommon
{
    public long CreatedAtTick { get; set; } = DateTime.Now.Ticks;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid? CreatedById { get; set; }

    [NotMapped]
    public Account? CreatedBy { get; set; }
}