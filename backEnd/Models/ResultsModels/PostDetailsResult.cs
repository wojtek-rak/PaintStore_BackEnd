﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backEnd.Interfaces;

namespace backEnd.Models.ResultsModels
{
    public class PostDetailsResult : PostsResults, IPosts
    {
        public string CategoryTypeName { get; set; }
        public string CategoryToolName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }

        public PostDetailsResult(IPosts iPosts) : base (iPosts)
        {
        }
    }
}
