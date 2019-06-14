using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public enum EnumUploadResult
    {
        Error = 0,
        Success = 1,
        SaveImageError = 2,
        UpLoadZipError = 3,
        UnZipError = 4,
        SaveCourseInfoError = 5,
        SaveCourseTagsError = 6,
        SaveChapterSectionInfoError = 7,
        NoChapter = 8,
        NoSection = 9
    }
}