﻿namespace TechDrop.Logic.Dto;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;
}