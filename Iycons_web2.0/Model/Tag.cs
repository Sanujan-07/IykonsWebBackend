﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Tag
    {
        [Key]
       
        public int TagId { get; set; }
        public string? TagName { get; set; }
        public ICollection<PostTag> TagPosts { get; set; }


    }
}
