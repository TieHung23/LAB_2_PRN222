using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Feature
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Descripsion { get; set; } = string.Empty;
}