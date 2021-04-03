using System;
using System.ComponentModel.DataAnnotations;
using TodoList.Models;

namespace TodoList.ViewModel
{
    public class TodoCreateViewModel
    {
        [Display(Name = "Todo")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Date")]
        [Required]
        [TodoListDate(ErrorMessage = "Please choose a future date!")]
        [DataType(DataType.DateTime)]
        public DateTime TodoDate { get; set; }
    }
}
