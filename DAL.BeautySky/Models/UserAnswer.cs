using System;
using System.Collections.Generic;

namespace DAL.BeautySky.Models;

public partial class UserAnswer
{
    public int UserAnswerId { get; set; }

    public int? UserQuizId { get; set; }

    public string? QuestionId { get; set; }

    public string? AnswerId { get; set; }

    public int? SkinTypeId { get; set; }

    public virtual SkinType? SkinType { get; set; }

    public virtual UserQuiz? UserQuiz { get; set; }
}
