using System;
using System.Collections.Generic;

namespace DAL.BeautySky.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public string? SkinTypeId { get; set; }

    public string? Point { get; set; }

    public virtual Question? Question { get; set; }
}
