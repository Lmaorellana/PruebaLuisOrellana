﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using SuperNova.Erp.Base.Domain.Utils;
using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class Deportista : EntityBase
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Pais { get; set; }

    public virtual ICollection<Intento> Intentos { get; set; } = new List<Intento>();
}