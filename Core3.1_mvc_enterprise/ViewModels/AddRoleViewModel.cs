﻿using System.ComponentModel.DataAnnotations;

namespace PandasPizzaShop.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}