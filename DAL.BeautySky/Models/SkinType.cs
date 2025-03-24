using System;
using System.Collections.Generic;

namespace DAL.BeautySky.Models;

public partial class SkinType
{
    public int SkinTypeId { get; set; }

    public string SkinType1 { get; set; } = null!;

    public virtual ICollection<CarePlan> CarePlans { get; set; } = new List<CarePlan>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
