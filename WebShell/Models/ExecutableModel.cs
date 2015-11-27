using System;
using System.ComponentModel.DataAnnotations;

namespace WebShell.Models
{
    [Serializable]
    public class ExecutableModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Arguments { get; set; }
    }
}