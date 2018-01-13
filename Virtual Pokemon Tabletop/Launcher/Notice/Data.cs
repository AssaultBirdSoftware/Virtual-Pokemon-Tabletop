namespace Launcher.Notice
{
    internal enum NoticeColors
    {
        Red,
        Yellow,
        Green,
        Blue,
        Orange,
        Purple,
        Black,
        White,
        Gray
    }

    internal class Data
    {
        public string Message { get; set; }
        public NoticeColors Back_Color { get; set; }
    }
}