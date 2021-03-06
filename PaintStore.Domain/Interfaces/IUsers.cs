﻿namespace PaintStore.Domain.Interfaces
{
    public interface IUsers
    {
         int Id { get; set; }
         string Name { get; set; }
         string Link { get; set; }
         string AvatarImgLink { get; set; }
         string BackgroundImgLink { get; set; }
         string About { get; set; }
         int PostsCount { get; set; }
         int FollowedCount { get; set; }
         int FollowingCount { get; set; }
    }
}
