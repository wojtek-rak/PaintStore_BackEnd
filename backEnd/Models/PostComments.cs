﻿using System;
using System.Collections.Generic;

namespace backEnd.Models
{
    public partial class PostComments
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}
