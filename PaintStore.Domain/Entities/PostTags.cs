﻿namespace PaintStore.Domain.Entities
{
    public partial class PostTags
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
