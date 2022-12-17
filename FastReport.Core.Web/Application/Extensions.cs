using FastReport.Table;
using System;
using System.Collections.Generic;

using System.Text;

namespace FastReport.Web
{
    public static partial class Extensions
    {
        public static void FindClickedObject<T>(
                this Report Report,
                string objectName,
                int pageN,
                float left,
                float top,
                Action<T, ReportPage, int> action
            )
            where T : ComponentBase
        {
            if (Report.PreparedPages == null)
                return;

            bool found = false;
            while (pageN < Report.PreparedPages.Count && !found)
            {
                ReportPage page = Report.PreparedPages.GetPage(pageN);
                if (page != null)
                {
                    ObjectCollection allObjects = page.AllObjects;
                    var point = new SkiaSharp.SKPoint(left + 1, top + 1);
                    foreach (Base obj in allObjects)
                    {
                        if (obj is ReportComponentBase)
                        {
                            ReportComponentBase c = obj as ReportComponentBase;
                            if (c is TableBase)
                            {
                                TableBase table = c as TableBase;
                                for (int i = 0; i < table.RowCount; i++)
                                {
                                    for (int j = 0; j < table.ColumnCount; j++)
                                    {
                                        TableCell textcell = table[j, i];
                                        if (textcell.Name == objectName)
                                        {
                                            SkiaSharp.SKRect rect = new SkiaSharp.SKRect();
                                            rect.Location = new SkiaSharp.SKPoint(table.Columns[j].AbsLeft,
                                                table.Rows[i].AbsTop);
                                            rect.Size = new SkiaSharp.SKSize(textcell.Width,
                                                textcell.Height);
                                            if (rect.Contains(point))
                                            {
                                                action(textcell as T, page, pageN);
                                                found = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (found)
                                        break;
                                }
                            }
                            else if (c is T)
                            {
                                if (c.Name == objectName && c.AbsBounds.Contains(point))
                                {
                                    action(c as T, page, pageN);
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                                break;
                        }
                    }
                    page.Dispose();
                    pageN++;
                }
            }
        }
    }
}
