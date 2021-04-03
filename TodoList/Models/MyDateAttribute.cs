using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TodoListDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d > DateTime.Now;
        }
    }
}
