using InfyKiddoFun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfyKiddoFun.Domain.Entities
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public SpecificStream SpecificStream { get; set; }
    }
}
