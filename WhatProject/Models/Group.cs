using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatProject.Models
{
    [Table("student_group")]
    class Group
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("course_id")]
        public long CourseId { get; set; }

        [Column("name")]
        public string GroupName { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("finish_date")]
        public DateTime FinishDate { get; set; }
    }
}
