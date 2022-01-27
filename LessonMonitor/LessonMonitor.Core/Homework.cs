using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LessonMonitor.Core
{
    public class Homework
    {
        [MaxLength(4)]
        public string Title { get; set; }
        public string Link { get; set; }

        [Range(10,20)]
        public int MemberId { get; set; }
        public int? MentorId { get; set; }
    }
}
