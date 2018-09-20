using System;

namespace Kata04.TabularFile
{
    public interface IBuildTabularFileDescription
    {
        IBuildTabularFileDescription HasHeadings(bool hasHeading);
        IBuildTabularFileDescription HasSummaryLine(bool hasSummary);
        IBuildTabularFileDescription IgnoreBlankLines(bool ignoreBlankLines);
        IBuildTabularFileDescription IgnoreSpecialLines(bool ignoreSpecialLines);

        IBuildTabularFileDescription AddField(string propertyName, Type dataType, int startIndex,
            int length);
        IBuildTabularFileDescription AddField(string propertyName, string dataFieldName, Type dataType, int startIndex,
            int length);

        TabularFileDescription Build();
    }
}