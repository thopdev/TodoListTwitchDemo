﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models.Forms
{
    public class NewTodoItemFormModel
    {
        [Required(ErrorMessage = "Please provide a text for your new todo item!")] 
        public string Name { get; set; }
    }
}
