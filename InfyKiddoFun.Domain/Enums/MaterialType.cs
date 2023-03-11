using System.ComponentModel;

namespace InfyKiddoFun.Domain.Enums;

public enum MaterialType : byte
{
    [Description("Document Files")]
    DocumentFile,
    [Description("Video Lecture")]
    VideoLecture,
    [Description("External Reference Link")]
    ExternalReferenceLink,
}