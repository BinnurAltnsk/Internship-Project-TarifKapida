﻿using System;
using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
