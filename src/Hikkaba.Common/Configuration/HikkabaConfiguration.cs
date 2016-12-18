﻿using System.Collections.Generic;

namespace Hikkaba.Common.Configuration
{
    public class HikkabaConfiguration
    {
        public int ThumbnailsMaxWidth { get; set; } = 100;
        public int ThumbnailsMaxHeight { get; set; } = 100;

        public ICollection<string> AudioExtensions { get; set; } = new List<string>() { "mp3", "ogg", "aac" };
        public ICollection<string> PictureExtensions { get; set; } = new List<string>() { "jpg", "jpeg", "png", "gif", "svg" };
        public ICollection<string> VideoExtensions { get; set; } = new List<string>() { "webm", "mp4" };

        public int MaxAttachmentsCountPerPost { get; set; } = 10;
        public int MaxAttachmentsBytesPerPost { get; set; } = 20000000;
    }
}
