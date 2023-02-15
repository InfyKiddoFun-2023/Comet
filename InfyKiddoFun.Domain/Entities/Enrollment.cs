using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfyKiddoFun.Domain.Entities
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }


        public Course Course { get; set; }
        public StudentUser Student { get; set; }
    }
}
