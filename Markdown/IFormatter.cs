namespace Markdown
{
    public interface IFormatter
    {
        string FormatUnderscore(string text);
        string FormatDoubleUnderscore(string text);
        string FormatBacktick(string text);
        string FormatMoreLess(string text);
    }
}