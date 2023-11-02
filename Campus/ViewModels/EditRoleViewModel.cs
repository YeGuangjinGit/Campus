﻿using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class EditRoleViewModel
    {


        [Display(Name = "角色Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "角色名称是必填的")]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

    }
}
