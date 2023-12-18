﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace EducationAssingments.Data.Entities;

public class Assingment
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = "";

    [MaxLength(100)]
    [Required]
    public string Answer { get; set; } = "";

    [Required]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [Required]
    public DateTime DueDate { get; set; }

    public bool AllowOverDueTurningIn { get; set; } = false;

    public List<AssingmentAnswer> Answers { get; set; } = [];
}
