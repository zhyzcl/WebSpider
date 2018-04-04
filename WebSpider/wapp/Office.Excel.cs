using System;
using System.IO;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NPOI.HSSF.Util;
using NPOI.HSSF.Extractor;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.Util;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.Extractor;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Extractor;
using App;

namespace wapp
{
    public class Excel
    {
        #region 属性设置

        /// <summary>设置或获取Excel操作对象</summary>
        private IWorkbook _Workbook;

        /// <summary>设置或获取Excel操作对象</summary>
        public IWorkbook Workbook
        {
            get
            {
                if (_Workbook == null)
                {
                    CreateWorkbook();
                }
                return _Workbook;
            }
            set { _Workbook = value; }
        }

        /// <summary>设置或获取Excel当前工作表</summary>
        private ISheet _Sheet;

        /// <summary>设置或获取Excel当前工作表</summary>
        public ISheet Sheet
        {
            get
            {
                if (_Sheet == null || _Workbook == null)
                {
                    if (Workbook.NumberOfSheets > 0)
                    {
                        _Sheet = Workbook.GetSheetAt(0);
                    }
                    else
                    {
                        _Sheet = CreateSheet();
                    }
                }
                return _Sheet;
            }
            set { _Sheet = value; }
        }

        /// <summary>设置或获取操作是否发生错误</summary>
        private bool _IsErr = false;

        /// <summary>设置或获取操作是否发生错误</summary>
        public bool IsErr
        {
            get { return _IsErr; }
            set { _IsErr = value; }
        }

        /// <summary>设置或获取发生错误时的错误信息</summary>
        private string _Err = "";

        /// <summary>设置或获取发生错误时的错误信息</summary>
        public string Err
        {
            get { return _Err; }
            set { _Err = value; }
        }

        /// <summary>设置或获取Excel文档类型，0:Excel2003;1:Excel2007</summary>
        private int _ExcelType = 0;

        /// <summary>设置或获取Excel文档类型，0:Excel2003;1:Excel2007</summary>
        public int ExcelType
        {
            get { return _ExcelType; }
            set { _ExcelType = value; }
        }

        #endregion

        #region 初始化类并创建Excel文档或工作表

        /// <summary>构造Excel操作类并创建Excel2003空文挡</summary>
        public Excel()
        {
            CreateWorkbook(0);
            CreateSheet();
        }

        /// <summary>根据Excel文档类型构造Excel操作类并创建Excel空文挡</summary>
        /// <param name="excelType">Excel文档类型，0:Excel2003;1:Excel2007</param>
        public Excel(int excelType)
        {
            CreateWorkbook(excelType);
            CreateSheet();
        }

        /// <summary>根据Excel文档类型与工作表名称构造Excel操作类并创建Excel空文挡</summary>
        /// <param name="excelType">Excel文档类型，0:Excel2003;1:Excel2007</param>
        /// <param name="sheetName">工作表名称</param>
        public Excel(int excelType, string sheetName)
        {
            CreateWorkbook(excelType);
            CreateSheet(sheetName);
        }

        /// <summary>根据Excel文件路径构造Excel工作表</summary>
        /// <param name="filePath">Excel文件路径</param>
        public Excel(string filePath)
        {
            CreateWorkbook(filePath);
        }

        /// <summary>初始化错误信息</summary>
        public void SetErrInfo()
        {
            SetErrInfo(false, "");
        }

        /// <summary>设置操作发生错误是的错误信息</summary>
        /// <param name="iserr">是否发生错误</param>
        /// <param name="err">错误信息</param>
        public void SetErrInfo(bool iserr, string err)
        {
            IsErr = iserr;
            if (!iserr)
            {
                Err = "";
            }
            else
            {
                Err = err;
            }
        }

        /// <summary>根据Excel文件或Excel模版文件创建Workbook</summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>根据Excel文件或Excel模版文件创建Workbook</returns>
        public IWorkbook CreateWorkbook(string filePath)
        {
            SetErrInfo();
            Workbook = null;
            try
            {
                string fileExt = Path.GetExtension(filePath).ToLower();
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileExt.EndsWith("xlsx"))
                    {
                        Workbook = new XSSFWorkbook(file);
                        ExcelType = 1;
                        return Workbook;
                    }
                    else if (fileExt.EndsWith("xls"))
                    {
                        Workbook = new HSSFWorkbook(file);
                        ExcelType = 0;
                        return Workbook;
                    }
                    SetErrInfo(true, "Excel文件扩展名错误或读取失败！");
                }
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return null;
        }

        /// <summary>根据Excel文档类型创建Workbook</summary>
        /// <returns>根据Excel文档类型创建Workbook</returns>
        public IWorkbook CreateWorkbook()
        {
            return CreateWorkbook(ExcelType);
        }

        /// <summary>根据Excel文档类型创建Workbook</summary>
        /// <param name="excelType">Excel文档类型，0:Excel2003;1:Excel2007</param>
        /// <returns>根据Excel文档类型创建Workbook</returns>
        public IWorkbook CreateWorkbook(int excelType)
        {
            if (excelType == 1)
            {
                Workbook = new XSSFWorkbook();
                ExcelType = 1;
            }
            else
            {
                Workbook = new HSSFWorkbook();
                ExcelType = 0;
            }
            return Workbook;
        }

        /// <summary>创建一个新的Sheet</summary>
        /// <returns>创建一个新的Sheet</returns>
        public ISheet CreateSheet()
        {
            ISheet sheet = Workbook.CreateSheet();
            Sheet = sheet;
            return sheet;
        }

        /// <summary>根据工作表名称创建一个新的Sheet</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>根据工作表名称创建一个新的Sheet</returns>
        public ISheet CreateSheet(string sheetName)
        {
            ISheet sheet = Workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                sheet = Workbook.CreateSheet(sheetName);
                Sheet = sheet;
                return sheet;
            }
            else
            {
                Sheet = sheet;
                return sheet;
            }
        }

        #endregion

        #region Excel保存文件流及文件方法

        /// <summary>返回指定属性名称值</summary>
        /// <param name="val">属性值</param>
        /// <param name="type">值类型 0 字符串，1 浮点数，2 浮点数，3 Byte，4 Int16，5 Int32，6 Int64</param>
        /// <param name="defval">默认返回值</param>
        /// <returns>返回指定属性名称值</returns>
        private string GetHtmlTableNameValue(string val, int type, string defval)
        {
            if (type == 0 && val.Trim() != "")
            {
                return val;
            }
            else if (type == 1 && Often.IsNum(val))
            {
                return val;
            }
            else if (type == 2 && Often.IsDate(val))
            {
                return val;
            }
            else if (type == 3 && Often.IsByte(val))
            {
                return val;
            }
            else if (type == 4 && Often.IsInt16(val))
            {
                return val;
            }
            else if (type == 5 && Often.IsInt32(val))
            {
                return val;
            }
            else if (type == 6 && Often.IsInt64(val))
            {
                return val;
            }
            return defval;
        }

        /// <summary>读取HtmlTable实际单元位置</summary>
        /// <param name="holdList">已占用单元位置列表</param>
        /// <param name="rindex">返回单元行索引</param>
        /// <param name="cindex">返回单元列索引</param>
        private void LoadHtmlTableFactRowColIndex(List<string> holdList, ref int rindex, ref int cindex)
        {
            string holds = rindex.ToString() + "-" + cindex.ToString();
            while (holdList.IndexOf(holds) > -1)
            {
                cindex++;
            }
        }

        /// <summary>保存单元占用位置</summary>
        /// <param name="holdList">已占用单元位置列表引用</param>
        /// <param name="rowspan">单元占用行数</param>
        /// <param name="colspan">单元占用列数</param>
        /// <param name="rindex">单元行开始索引</param>
        /// <param name="cindex">单元列开始索引</param>
        private void SaveHtmlTableFactRowColIndex(ref List<string> holdList, int rowspan, int colspan, int rindex, int cindex)
        {
            int mrindex = rindex;
            int mcindex = cindex;
            for (int y = 0; y < rowspan; y++)
            {
                for (int x = 0; x < colspan; x++)
                {
                    mrindex = rindex + y;
                    mcindex = cindex + x;
                    SaveHtmlTableFactRowColIndex(ref holdList, mrindex, mcindex);
                }
            }
        }

        /// <summary>保存单元占用位置</summary>
        /// <param name="holdList">已占用单元位置列表引用</param>
        /// <param name="rindex">单元行开始索引</param>
        /// <param name="cindex">单元列开始索引</param>
        private void SaveHtmlTableFactRowColIndex(ref List<string> holdList, int rindex, int cindex)
        {
            string holds = rindex.ToString() + "-" + cindex.ToString();
            if (holdList.IndexOf(holds) == -1)
            {
                holdList.Add(holds);
            }
        }

        /// <summary>将指定的HtmlTable转换为Excel工作表并导入到默认的Excel工作表中</summary>
        /// <param name="htmTable">HtmlTable字符串</param>
        public void HtmlTableToExcel(string htmTable)
        {
            HtmlTableToExcel(Sheet, htmTable);
        }

        /// <summary>将指定的HtmlTable转换为Excel工作表并导入到指定的Excel工作表中</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="htmTable">HtmlTable字符串</param>
        public void HtmlTableToExcel(int sheetIndex, string htmTable)
        {
            ISheet sheet = GetSheet(sheetIndex);
            HtmlTableToExcel(sheet, htmTable);
        }

        /// <summary>将指定的HtmlTable转换为Excel工作表并导入到指定的Excel工作表中</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="htmTable">HtmlTable字符串</param>
        public void HtmlTableToExcel(string sheetName, string htmTable)
        {
            ISheet sheet = GetSheet(sheetName);
            HtmlTableToExcel(sheet, htmTable);
        }

        /// <summary>将指定的HtmlTable转换为Excel工作表并导入到指定的Excel工作表中</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="htmTable">HtmlTable字符串</param>
        public void HtmlTableToExcel(ISheet sheet, string htmTable)
        {
            int rowIndex = 0;
            List<string> holdList = new List<string>();
            string TrPattern = "(<tr[^>]*>)([\\s\\S]*?)(<\\/tr>)";
            string TdPattern = "(<td[^>]*>)([\\s\\S]*?)(<\\/td>)";
            string PropertyPattern = "(\\w+)=([\\'\\\"]?)(\\S+)(\\2)";
            MatchCollection trCollection = Regex.Matches(htmTable, TrPattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < trCollection.Count; i++)
            {
                Match trm = trCollection[i];
                string trtxt = trm.Groups[0].Value;
                int colIndex = 0;
                MatchCollection tdCollection = Regex.Matches(trtxt, TdPattern, RegexOptions.IgnoreCase);
                for (int ii = 0; ii < tdCollection.Count; ii++)
                {
                    Match tdm = tdCollection[ii];
                    string tds = tdm.Groups[1].Value;
                    string tdvalue = tdm.Groups[2].Value;
                    int colspan = 1;
                    int rowspan = 1;
                    int type = 0;
                    MatchCollection proCollection = Regex.Matches(tds, PropertyPattern, RegexOptions.IgnoreCase);
                    for (int iii = 0; iii < proCollection.Count; iii++)
                    {
                        Match pros = proCollection[iii];
                        string name = pros.Groups[1].Value.Trim().ToLower();
                        string val = pros.Groups[3].Value;
                        if (name == "colspan")
                        {
                            colspan = Convert.ToInt32(GetHtmlTableNameValue(val, 5, "1"));
                            if (colspan < 1)
                            {
                                colspan = 1;
                            }
                        }
                        else if (name == "rowspan")
                        {
                            rowspan = Convert.ToInt32(GetHtmlTableNameValue(val, 5, "1"));
                            if (rowspan < 1)
                            {
                                rowspan = 1;
                            }
                        }
                        else if (name == "type")
                        {
                            string types = GetHtmlTableNameValue(val, 0, "");
                            if (Often.IsInt32(types))
                            {
                                type = Convert.ToInt32(types);
                            }
                            else
                            {
                                type = GetCellTypeNum(types);
                            }
                        }
                    }
                    int rindex = rowIndex;
                    int cindex = colIndex;
                    LoadHtmlTableFactRowColIndex(holdList, ref rindex, ref cindex);
                    if (colspan > 1 || rowspan > 1)
                    {
                        int mrindex = rindex;
                        int mcindex = cindex;
                        for (int y = 0; y < rowspan; y++)
                        {
                            for (int x = 0; x < colspan; x++)
                            {
                                mrindex = rindex + y;
                                mcindex = cindex + x;
                                SetCellValue(sheet, mrindex, mcindex, "", type);
                            }
                        }
                        bool ismcell = MergeCell(sheet, rindex, mrindex, cindex, mcindex);
                        SaveHtmlTableFactRowColIndex(ref holdList, rowspan, colspan, rindex, cindex);
                    }
                    else
                    {
                        SaveHtmlTableFactRowColIndex(ref holdList, rindex, cindex);
                    }
                    SetCellValue(sheet, rindex, cindex, tdvalue, type);
                    colIndex++;
                }
                rowIndex++;
            }
        }

        /// <summary>将指定的内存表转换为Excel工作表并导入到默认的Excel工作表中</summary>
        /// <param name="dt">内存表</param>
        public void DataTableToExcel(DataTable dt)
        {
            DataTableToExcel(Sheet, dt, true);
        }

        /// <summary>将指定的内存表转换为Excel工作表并导入到默认的Excel工作表中</summary>
        /// <param name="dt">内存表</param>
        /// <param name="istitle">是否输出标题列</param>
        public void DataTableToExcel(DataTable dt, bool istitle)
        {
            DataTableToExcel(Sheet, dt, istitle);
        }

        /// <summary>将指定的内存表转换为Excel工作表并导入到默认的Excel工作表中</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="dt">内存表</param>
        public void DataTableToExcel(string sheetName, DataTable dt)
        {
            ISheet sheet = GetSheet(sheetName);
            DataTableToExcel(sheet, dt, true);
        }

        /// <summary>将指定的内存表转换为Excel工作表并导入到默认的Excel工作表中</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="dt">内存表</param>
        /// <param name="istitle">是否输出标题列</param>
        public void DataTableToExcel(string sheetName, DataTable dt, bool istitle)
        {
            ISheet sheet = GetSheet(sheetName);
            DataTableToExcel(sheet, dt, istitle);
        }

        /// <summary>将指定的内存表转换为Excel工作表并导入到指定的Excel工作表中</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="dt">内存表</param>
        /// <param name="istitle">是否输出标题列</param>
        public void DataTableToExcel(ISheet sheet, DataTable dt, bool istitle)
        {
            if (istitle)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    string colname = dt.Columns[ii].ColumnName;
                    SetCellValue(sheet, 0, ii, colname, 0);
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    int rint = i;
                    if (istitle)
                    {
                        rint += 1;
                    }
                    string val = dt.Rows[i][ii].ToString();
                    string dtype = dt.Columns[ii].DataType.ToString().ToLower();
                    if (dtype.IndexOf("string") > -1 || dtype.IndexOf("char") > -1 || dtype.IndexOf("byte[]") > -1 || dtype.IndexOf("time") > -1)
                    {
                        SetCellValue(sheet, rint, ii, val, 0);
                    }
                    else
                    {
                        SetCellValue(sheet, rint, ii, val, 1);
                    }
                }
            }
        }

        /// <summary>将指定的工作表转换为DataTable</summary>
        /// <param name="sheetName">Excel工作表名称(sheet)</param>
        /// <param name="headerRowIndex">标题行索引</param>
        /// <param name="haveHeader">是否存在标题列</param>
        /// <returns>将指定的工作表转换为DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, int headerRowIndex, bool haveHeader)
        {
            ISheet sheet = Workbook.GetSheet(sheetName);
            return ExcelToDataTable(sheet, headerRowIndex, haveHeader);
        }

        /// <summary>将指定的工作表转换为DataTable</summary>
        /// <param name="SheetIndex">Excel工作表索引(sheet index)</param>
        /// <param name="headerRowIndex">列标题行索引</param>
        /// <param name="haveHeader">是否存在标题列</param>
        /// <returns>将指定的工作表转换为DataTable</returns>
        public DataTable ExcelToDataTable(int SheetIndex, int headerRowIndex, bool haveHeader)
        {
            ISheet sheet = Workbook.GetSheetAt(SheetIndex);
            return ExcelToDataTable(sheet, headerRowIndex, haveHeader);
        }

        /// <summary>将指定的工作表转换为DataTable</summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="headerRowIndex">列标题行索引</param>
        /// <param name="haveHeader">是否存在标题列</param>
        /// <returns>将指定的工作表转换为DataTable</returns>
        public DataTable ExcelToDataTable(ISheet sheet, int headerRowIndex, bool haveHeader)
        {
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                string ColumnName = (haveHeader == true) ? headerRow.GetCell(i).StringCellValue : "f" + i.ToString();
                DataColumn column = new DataColumn(ColumnName);
                table.Columns.Add(column);
            }
            int rowCount = sheet.LastRowNum;
            int RowStart = (haveHeader == true) ? sheet.FirstRowNum + 1 : sheet.FirstRowNum;
            for (int i = RowStart; i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        /// <summary>返回Excel内存流对象</summary>
        /// <returns>返回Excel内存流对象</returns>
        public MemoryStream GetMemoryStream()
        {
            SetErrInfo();
            try
            {
                MemoryStream ms = new MemoryStream();
                Workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return null;
        }

        /// <summary>返回Excel流对象</summary>
        /// <returns>返回Excel流对象</returns>
        public Stream GetStream()
        {
            return GetMemoryStream();
        }

        /// <summary>将Excel写入到文件</summary>
        /// <param name="filePath">文件路径及文件名</param>
        public void WriteFile(string filePath)
        {
            SetErrInfo();
            try
            {
                MemoryStream ms = new MemoryStream();
                Workbook.Write(ms);
                WriteSteamToFile(ms.ToArray(), filePath);
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
        }

        /// <summary>将内存流写入到文件</summary>
        /// <param name="ms">内存流</param>
        /// <param name="filePath">文件路径及文件名</param>
        public void WriteSteamToFile(MemoryStream ms, string filePath)
        {
            SetErrInfo();
            try
            {
                WriteSteamToFile(ms.ToArray(), filePath);
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
        }

        /// <summary>将字节流写入到文件</summary>
        /// <param name="data">字节流</param>
        /// <param name="filePath">文件路径及文件名</param>
        public void WriteSteamToFile(byte[] data, string filePath)
        {
            SetErrInfo();
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
                data = null;
                fs = null;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
        }

        #endregion

        #region Excel文档常用操作

        /// <summary>根据数字返回图片类型，0：JPEG，1：PNG，2：EMF，3：WMF，4：PICT，5：DIB，6：None</summary>
        /// <param name="type">0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>根据数字返回图片类型，0：JPEG，1：PNG，2：EMF，3：WMF，4：PICT，5：DIB，6：None</returns>
        public static PictureType GetPictureType(int type)
        {
            if (type == 1)
            {
                return PictureType.PNG;
            }
            else if (type == 2)
            {
                return PictureType.EMF;
            }
            else if (type == 3)
            {
                return PictureType.WMF;
            }
            else if (type == 4)
            {
                return PictureType.PICT;
            }
            else if (type == 5)
            {
                return PictureType.DIB;
            }
            else if (type == 6)
            {
                return PictureType.None;
            }
            return PictureType.JPEG;
        }

        /// <summary>根据数字返回单元值类型，0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</summary>
        /// <param name="type">0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>根据数字返回单元值类型，0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</returns>
        public static CellType GetCellType(int type)
        {
            if (type == 1)
            {
                return CellType.Numeric;
            }
            else if (type == 2)
            {
                return CellType.Boolean;
            }
            else if (type == 3)
            {
                return CellType.Formula;
            }
            else if (type == 4)
            {
                return CellType.Blank;
            }
            else if (type == 5)
            {
                return CellType.Unknown;
            }
            else if (type == 6)
            {
                return CellType.Error;
            }
            return CellType.String;
        }

        /// <summary>根据字符串返回单元值类型，默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</summary>
        /// <param name="type">默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</param>
        /// <returns>根据字符串返回单元值类型，默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</returns>
        public static CellType GetCellType(string type)
        {
            type = type.Trim().ToLower();
            if (type == "numeric")
            {
                return CellType.Numeric;
            }
            else if (type == "boolean")
            {
                return CellType.Boolean;
            }
            else if (type == "formula")
            {
                return CellType.Formula;
            }
            else if (type == "blank")
            {
                return CellType.Blank;
            }
            else if (type == "unknown")
            {
                return CellType.Unknown;
            }
            else if (type == "error")
            {
                return CellType.Error;
            }
            return CellType.String;
        }

        /// <summary>根据字符串返回单元值类型数字标识，默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</summary>
        /// <param name="type">默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</param>
        /// <returns>根据字符串返回单元值类型数字标识，默认：字符串，numeric：数字，boolean：布尔，formula：公式，blank：空，unknown：未知，error：错误</returns>
        public static int GetCellTypeNum(string type)
        {
            type = type.Trim().ToLower();
            if (type == "numeric")
            {
                return 1;
            }
            else if (type == "boolean")
            {
                return 2;
            }
            else if (type == "formula")
            {
                return 3;
            }
            else if (type == "blank")
            {
                return 4;
            }
            else if (type == "unknown")
            {
                return 5;
            }
            else if (type == "error")
            {
                return 6;
            }
            return 0;
        }

        /// <summary>返回Excel文档文本</summary>
        /// <returns>返回Excel文档文本</returns>
        public string ExtractString()
        {
            if (ExcelType == 1)
            {
                XSSFExcelExtractor extractor = new XSSFExcelExtractor((XSSFWorkbook)Workbook);
                return extractor.Text;
            }
            else
            {
                NPOI.HSSF.Extractor.ExcelExtractor extractor = new NPOI.HSSF.Extractor.ExcelExtractor((HSSFWorkbook)Workbook);
                return extractor.Text;
            }
        }

        /// <summary>根据工作表索引值返回工作表操作对象</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <returns>根据工作表索引值返回工作表操作对象</returns>
        public ISheet GetSheet(int sheetIndex)
        {
            int snum = Workbook.NumberOfSheets;
            if (snum > 0)
            {
                if (snum > sheetIndex && sheetIndex > -1)
                {
                    return Workbook.GetSheetAt(sheetIndex);
                }
                else if (sheetIndex < 0)
                {
                    return Workbook.GetSheetAt(0);
                }
                else
                {
                    return Workbook.GetSheetAt(snum - 1);
                }
            }
            return CreateSheet();
        }

        /// <summary>根据工作表名称返回工作表操作对象</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <returns>根据工作表名称返回工作表操作对象</returns>
        public ISheet GetSheet(string sheetName)
        {
            ISheet sheet = Workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                return CreateSheet(sheetName);
            }
            return sheet;
        }

        #endregion

        #region 获取单元值

        /// <summary>返回默认的工作表中指定的行索引与列索引内的单元值</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <returns>返回默认的工作表中指定的行索引与列索引内的单元值</returns>
        public string GetCellValue(int rowIndex, int cellIndex)
        {
            return GetCellValue(Sheet, rowIndex, cellIndex);
        }

        /// <summary>返回指定的工作表中指定的行索引与列索引内的单元值</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <returns>返回指定的工作表中指定的行索引与列索引内的单元值</returns>
        public string GetCellValue(int sheetIndex, int rowIndex, int cellIndex)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return GetCellValue(sheet, rowIndex, cellIndex);
        }

        /// <summary>返回指定的工作表中指定的行索引与列索引内的单元值</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <returns>返回指定的工作表中指定的行索引与列索引内的单元值</returns>
        public string GetCellValue(string sheetName, int rowIndex, int cellIndex)
        {
            ISheet sheet = GetSheet(sheetName);
            return GetCellValue(sheet, rowIndex, cellIndex);
        }

        /// <summary>返回指定的工作表中指定的行索引与列索引内的单元值</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <returns>返回指定的工作表中指定的行索引与列索引内的单元值</returns>
        public string GetCellValue(ISheet sheet, int rowIndex, int cellIndex)
        {
            SetErrInfo();
            try
            {
                return sheet.GetRow(rowIndex).GetCell(cellIndex).StringCellValue;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
                return "";
            }
        }

        #endregion

        #region 设置单元值

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(int rowIndex, int cellIndex, string value)
        {
            return SetCellValue(Sheet, rowIndex, cellIndex, value, 0);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(int sheetIndex, int rowIndex, int cellIndex, string value)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCellValue(sheet, rowIndex, cellIndex, value, 0);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(string sheetName, int rowIndex, int cellIndex, string value)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCellValue(sheet, rowIndex, cellIndex, value, 0);
        }

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(int rowIndex, int cellIndex, string value, int cellType)
        {
            return SetCellValue(Sheet, rowIndex, cellIndex, value, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(int sheetIndex, int rowIndex, int cellIndex, string value, int cellType)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCellValue(sheet, rowIndex, cellIndex, value, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(string sheetName, int rowIndex, int cellIndex, string value, int cellType)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCellValue(sheet, rowIndex, cellIndex, value, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="value">单元值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值,设置成功返回true,否则返回false</returns>
        public bool SetCellValue(ISheet sheet, int rowIndex, int cellIndex, string value, int cellType)
        {
            SetErrInfo();
            try
            {
                if (cellType == 3)
                {
                    SetCelFormula(sheet, rowIndex, cellIndex, value);
                }
                else
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null)
                    {
                        row = sheet.CreateRow(rowIndex);
                    }
                    ICell cell = row.GetCell(cellIndex);
                    CellType ctype = GetCellType(cellType);
                    if (cell == null)
                    {
                        cell = row.CreateCell(cellIndex, ctype);
                    }
                    if (cellType == 1 && Often.IsNum(value))
                    {
                        double db = Convert.ToDouble(value);
                        cell.SetCellValue(db);
                        cell.SetCellType(ctype);
                    }
                    else
                    {
                        cell.SetCellValue(value);
                        cell.SetCellType(ctype);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置单元值类型

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</returns>
        public bool SetCellType(int rowIndex, int cellIndex, int cellType)
        {
            return SetCellType(Sheet, rowIndex, cellIndex, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</returns>
        public bool SetCellType(int sheetIndex, int rowIndex, int cellIndex, int cellType)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCellType(sheet, rowIndex, cellIndex, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</returns>
        public bool SetCellType(string sheetName, int rowIndex, int cellIndex, int cellType)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCellType(sheet, rowIndex, cellIndex, cellType);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="cellType">单元值类型：0：字符串，1：数字，2：布尔，3：公式，4：空，5：未知，6：错误</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元值类型,设置成功返回true,否则返回false</returns>
        public bool SetCellType(ISheet sheet, int rowIndex, int cellIndex, int cellType)
        {
            SetErrInfo();
            try
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    ICell cell = row.GetCell(cellIndex);
                    if (cell != null)
                    {
                        cell.SetCellType(GetCellType(cellType));
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置单元公式

        /// <summary>设置默认的工作表中指定列索引单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置默认的工作表中指定列索引单元公式,设置成功返回true,否则返回false</returns>
        public bool SetColumnFormula(int cellIndex, string formula)
        {
            return SetColumnFormula(Sheet, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</returns>
        public bool SetColumnFormula(int sheetIndex, int cellIndex, string formula)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetColumnFormula(sheet, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</returns>
        public bool SetColumnFormula(string sheetName, int cellIndex, string formula)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetColumnFormula(sheet, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定列索引单元公式,设置成功返回true,否则返回false</returns>
        public bool SetColumnFormula(ISheet sheet, int cellIndex, string formula)
        {
            SetErrInfo();
            try
            {
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    for (int ii = row.FirstCellNum; ii < row.LastCellNum; ii++)
                    {
                        SetCelFormula(sheet, i, ii, formula);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</returns>
        public bool SetCelFormula(int rowIndex, int cellIndex, string formula)
        {
            return SetCelFormula(Sheet, rowIndex, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</returns>
        public bool SetCelFormula(int sheetIndex, int rowIndex, int cellIndex, string formula)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCelFormula(sheet, rowIndex, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</returns>
        public bool SetCelFormula(string sheetName, int rowIndex, int cellIndex, string formula)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCelFormula(sheet, rowIndex, cellIndex, formula);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellIndex">列索引</param>
        /// <param name="formula">公式表达式</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元公式,设置成功返回true,否则返回false</returns>
        public bool SetCelFormula(ISheet sheet, int rowIndex, int cellIndex, string formula)
        {
            SetErrInfo();
            try
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row == null)
                {
                    row = sheet.CreateRow(rowIndex);
                }
                ICell cell = row.GetCell(cellIndex);
                if (cell == null)
                {
                    cell = row.CreateCell(cellIndex, GetCellType(3));
                }
                cell.SetCellFormula(formula);
                sheet.ForceFormulaRecalculation = true; ;
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置单元超链接

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="linktype">链接类型(0:网址、1:档案、2:邮件、3:內文)</param>
        /// <param name="linkValue">链接地址</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</returns>
        public bool SetCellLink(int rowIndex, int cellIndex, int linktype, string linkValue)
        {
            return SetCellLink(Sheet, rowIndex, cellIndex, linktype, linkValue);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="linktype">链接类型(0:网址、1:档案、2:邮件、3:內文)</param>
        /// <param name="linkValue">链接地址</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</returns>
        public bool SetCellLink(int sheetIndex, int rowIndex, int cellIndex, int linktype, string linkValue)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCellLink(sheet, rowIndex, cellIndex, linktype, linkValue);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="linktype">链接类型(0:网址、1:档案、2:邮件、3:內文)</param>
        /// <param name="linkValue">链接地址</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</returns>
        public bool SetCellLink(string sheetName, int rowIndex, int cellIndex, int linktype, string linkValue)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCellLink(sheet, rowIndex, cellIndex, linktype, linkValue);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="linktype">链接类型(0:网址、1:档案、2:邮件、3:內文)</param>
        /// <param name="linkValue">链接地址</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元超链接,设置成功返回true,否则返回false</returns>
        public bool SetCellLink(ISheet sheet, int rowIndex, int cellIndex, int linktype, string linkValue)
        {
            SetErrInfo();
            try
            {
                ICellStyle linkstyle = Workbook.CreateCellStyle();
                IFont linkfont = Workbook.CreateFont();
                linkfont.Underline = FontUnderlineType.Single;
                linkfont.Color = HSSFColor.Blue.Index;
                linkstyle.SetFont(linkfont);
                ICell cell = sheet.GetRow(rowIndex).GetCell(cellIndex);
                string s = cell.StringCellValue + "";
                if (s.Trim() == "")
                {
                    cell.SetCellValue(linkValue);
                }
                HSSFHyperlink link;
                if (linktype == 1)
                {
                    link = new HSSFHyperlink(HyperlinkType.File);
                }
                else if (linktype == 2)
                {
                    link = new HSSFHyperlink(HyperlinkType.Email);
                    linkValue = "mailto:" + linkValue;
                }
                else if (linktype == 3)
                {
                    link = new HSSFHyperlink(HyperlinkType.Document);
                    linkValue = "'" + linkValue + "'!A1";
                }
                else
                {
                    link = new HSSFHyperlink(HyperlinkType.Url);
                }
                link.Address = (linkValue);
                cell.Hyperlink = (link);
                cell.CellStyle = (linkstyle);
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置字体样式

        /// <summary>设置默认的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置默认的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetCellFont(int rowIndex, int cellIndex, string fontName, short fontSize)
        {
            return SetCellFont(Sheet, rowIndex, cellIndex, fontName, fontSize);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetCellFont(int sheetIndex, int rowIndex, int cellIndex, string fontName, short fontSize)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetCellFont(sheet, rowIndex, cellIndex, fontName, fontSize);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetCellFont(string sheetName, int rowIndex, int cellIndex, string fontName, short fontSize)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetCellFont(sheet, rowIndex, cellIndex, fontName, fontSize);
        }

        /// <summary>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="rowIndex">行索引值</param>
        /// <param name="cellIndex">列索引值</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表中指定的行索引与列索引内的单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetCellFont(ISheet sheet, int rowIndex, int cellIndex, string fontName, short fontSize)
        {
            SetErrInfo();
            try
            {
                IFont font = Workbook.CreateFont();
                ICellStyle style = Workbook.CreateCellStyle();
                font.FontHeightInPoints = fontSize;
                font.FontName = fontName;
                style.SetFont(font);
                ICell cell = sheet.GetRow(rowIndex).GetCell(cellIndex);
                cell.CellStyle = style;
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        /// <summary>设置默认的工作表内所有单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置默认的工作表内所有单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetSheetFont(string fontName, short fontSize)
        {
            return SetSheetFont(Sheet, fontName, fontSize);
        }

        /// <summary>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetSheetFont(int sheetIndex, string fontName, short fontSize)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetSheetFont(sheet, fontName, fontSize);
        }

        /// <summary>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetSheetFont(string sheetName, string fontName, short fontSize)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetSheetFont(sheet, fontName, fontSize);
        }

        /// <summary>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置指定的工作表内所有单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetSheetFont(ISheet sheet, string fontName, short fontSize)
        {
            SetErrInfo();
            try
            {
                IFont font = Workbook.CreateFont();
                ICellStyle style = Workbook.CreateCellStyle();
                font.FontHeightInPoints = fontSize;
                font.FontName = fontName;
                style.SetFont(font);
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    for (int ii = row.FirstCellNum; ii < row.LastCellNum; ii++)
                    {
                        ICell Cell = row.GetCell(ii);
                        Cell.CellStyle = style;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        /// <summary>设置所有的工作表内所有单元字体样式,设置成功返回true,否则返回false</summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>设置所有的工作表内所有单元字体样式,设置成功返回true,否则返回false</returns>
        public bool SetAllSheetFont(string fontName, short fontSize)
        {
            SetErrInfo();
            try
            {
                IFont font = Workbook.CreateFont();
                ICellStyle style = Workbook.CreateCellStyle();
                font.FontHeightInPoints = fontSize;
                font.FontName = fontName;
                style.SetFont(font);
                for (int i = 0; i < Workbook.NumberOfSheets; i++)
                {
                    ISheet Sheets = Workbook.GetSheetAt(i);
                    for (int ii = Sheets.FirstRowNum; ii <= Sheets.LastRowNum; ii++)
                    {
                        IRow row = Sheets.GetRow(ii);
                        for (int iii = row.FirstCellNum; iii < row.LastCellNum; iii++)
                        {
                            ICell Cell = row.GetCell(iii);
                            Cell.CellStyle = style;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置网格线

        /// <summary>设置默认的工作表网格线,设置成功返回true,否则返回false</summary>
        /// <param name="haveGridLine">是否显示网格线</param>
        /// <returns>设置默认的工作表网格线,设置成功返回true,否则返回false</returns>
        public bool SetSheetGridLine(bool haveGridLine)
        {
            return SetSheetGridLine(Sheet, haveGridLine);
        }

        /// <summary>设置指定的工作表网格线,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="haveGridLine">是否显示网格线</param>
        /// <returns>设置指定的工作表网格线,设置成功返回true,否则返回false</returns>
        public bool SetSheetGridLine(int sheetIndex, bool haveGridLine)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetSheetGridLine(sheet, haveGridLine);
        }

        /// <summary>设置指定的工作表网格线,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="haveGridLine">是否显示网格线</param>
        /// <returns>设置指定的工作表网格线,设置成功返回true,否则返回false</returns>
        public bool SetSheetGridLine(string sheetName, bool haveGridLine)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetSheetGridLine(sheet, haveGridLine);
        }

        /// <summary>设置指定的工作表网格线,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="haveGridLine">是否显示网格线</param>
        /// <returns>设置指定的工作表网格线,设置成功返回true,否则返回false</returns>
        public bool SetSheetGridLine(ISheet sheet, bool haveGridLine)
        {
            SetErrInfo();
            try
            {
                sheet.DisplayGridlines = haveGridLine;
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        /// <summary>设置所有工作表网格线,设置成功返回true,否则返回false</summary>
        /// <param name="haveGridLine">是否显示网格线</param>
        /// <returns>设置所有工作表网格线,设置成功返回true,否则返回false</returns>
        public bool SetAllSheetGridLine(bool haveGridLine)
        {
            SetErrInfo();
            try
            {
                for (int i = 0; i < Workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = Workbook.GetSheetAt(i);
                    sheet.DisplayGridlines = haveGridLine;
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 设置群组

        /// <summary>设置默认工作表行群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="startIndex">行群组开始索引</param>
        /// <param name="endIndex">行群组结束索引</param>
        /// <returns>设置默认工作表行群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupRow(int startIndex, int endIndex)
        {
            return SetGroupRow(Sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表行群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="startIndex">行群组开始索引</param>
        /// <param name="endIndex">行群组结束索引</param>
        /// <returns>设置指定工作表行群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupRow(int sheetIndex, int startIndex, int endIndex)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetGroupRow(sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表行群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="startIndex">行群组开始索引</param>
        /// <param name="endIndex">行群组结束索引</param>
        /// <returns>设置指定工作表行群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupRow(string sheetName, int startIndex, int endIndex)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetGroupRow(sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表行群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="startIndex">行群组开始索引</param>
        /// <param name="endIndex">行群组结束索引</param>
        /// <returns>设置指定工作表行群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupRow(ISheet sheet, int startIndex, int endIndex)
        {
            SetErrInfo();
            try
            {
                sheet.GroupRow(startIndex, endIndex);
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        /// <summary>设置默认工作表列群组,设置成功返回true,否则返回false</summary>
        /// <param name="startIndex">列群组开始索引</param>
        /// <param name="endIndex">列群组结束索引</param>
        /// <returns>设置默认工作表列群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupColumn(int startIndex, int endIndex)
        {
            return SetGroupColumn(Sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表列群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="startIndex">列群组开始索引</param>
        /// <param name="endIndex">列群组结束索引</param>
        /// <returns>设置指定工作表列群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupColumn(int sheetIndex, int startIndex, int endIndex)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return SetGroupColumn(sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表列群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="startIndex">列群组开始索引</param>
        /// <param name="endIndex">列群组结束索引</param>
        /// <returns>设置指定工作表列群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupColumn(string sheetName, int startIndex, int endIndex)
        {
            ISheet sheet = GetSheet(sheetName);
            return SetGroupColumn(sheet, startIndex, endIndex);
        }

        /// <summary>设置指定工作表列群组,设置成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="startIndex">列群组开始索引</param>
        /// <param name="endIndex">列群组结束索引</param>
        /// <returns>设置指定工作表列群组,设置成功返回true,否则返回false</returns>
        public bool SetGroupColumn(ISheet sheet, int startIndex, int endIndex)
        {
            SetErrInfo();
            try
            {
                sheet.GroupColumn(startIndex, endIndex);
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
            }
            return false;
        }

        #endregion

        #region 图片操作

        /// <summary>将指定的图片嵌入到Excel内默认的工作表中,嵌入成功返回true,否则返回false</summary>
        /// <param name="imgPath">图片路径</param>
        /// <param name="type">图片类型，0：JPEG，1：PNG，2：EMF，3：WMF，4：PICT，5：DIB，6：None</param>
        /// <param name="IsResize">是否根据窗口大小自动调整图片大小</param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <returns>将指定的图片嵌入到Excel内默认的工作表中,嵌入成功返回true,否则返回false</returns>
        public bool EmbedImage(string imgPath, int type, bool IsResize, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
        {
            return EmbedImage(Sheet, imgPath, GetPictureType(type), IsResize, dx1, dy1, dx2, dy2, col1, row1, col2, row2);
        }

        /// <summary>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="imgPath">图片路径</param>
        /// <param name="type">图片类型，0：JPEG，1：PNG，2：EMF，3：WMF，4：PICT，5：DIB，6：None</param>
        /// <param name="IsResize">是否根据窗口大小自动调整图片大小</param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <returns>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</returns>
        public bool EmbedImage(int sheetIndex, string imgPath, int type, bool IsResize, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return EmbedImage(sheet, imgPath, GetPictureType(type), IsResize, dx1, dy1, dx2, dy2, col1, row1, col2, row2);
        }

        /// <summary>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="imgPath">图片路径</param>
        /// <param name="type">图片类型，0：JPEG，1：PNG，2：EMF，3：WMF，4：PICT，5：DIB，6：None</param>
        /// <param name="IsResize">是否根据窗口大小自动调整图片大小</param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <returns>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</returns>
        public bool EmbedImage(string sheetName, string imgPath, int type, bool IsResize, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
        {
            ISheet sheet = GetSheet(sheetName);
            return EmbedImage(sheet, imgPath, GetPictureType(type), IsResize, dx1, dy1, dx2, dy2, col1, row1, col2, row2);
        }

        /// <summary>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="imgPath">图片路径</param>
        /// <param name="ptype">图片类型</param>
        /// <param name="IsResize">是否根据窗口大小自动调整图片大小</param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <returns>将指定的图片嵌入到Excel内指定的工作表中,嵌入成功返回true,否则返回false</returns>
        public bool EmbedImage(ISheet sheet, string imgPath, PictureType ptype, bool IsResize, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
        {
            SetErrInfo();
            try
            {
                IDrawing patriarch = sheet.CreateDrawingPatriarch();
                IClientAnchor anchor = new XSSFClientAnchor(dx1, dy1, dx2, dy2, col1, row1, col2, row2);
                anchor.AnchorType = 2;
                FileStream file = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                int picint = Workbook.AddPicture(buffer, ptype);
                IPicture picture = patriarch.CreatePicture(anchor, picint);
                if (IsResize == true)
                {
                    picture.Resize();
                }
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
                return false;
            }
        }

        #endregion

        #region 合并单元

        /// <summary>根据指定的始终单元在默认的工作表中合并单元格,合并成功返回true,否则返回false</summary>
        /// <param name="firstRow">起始行索引</param>
        /// <param name="lastRow">终止行索引</param>
        /// <param name="firstCol">起始列索引</param>
        /// <param name="lastCol">终止列索引</param>
        /// <returns>根据指定的始终单元在默认的工作表中合并单元格,合并成功返回true,否则返回false</returns>
        public bool MergeCell(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            return MergeCell(Sheet, firstRow, lastRow, firstCol, lastCol);
        }

        /// <summary>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</summary>
        /// <param name="sheetIndex">工作表索引值</param>
        /// <param name="firstRow">起始行索引</param>
        /// <param name="lastRow">终止行索引</param>
        /// <param name="firstCol">起始列索引</param>
        /// <param name="lastCol">终止列索引</param>
        /// <returns>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</returns>
        public bool MergeCell(int sheetIndex, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            ISheet sheet = GetSheet(sheetIndex);
            return MergeCell(sheet, firstRow, lastRow, firstCol, lastCol);
        }

        /// <summary>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="firstRow">起始行索引</param>
        /// <param name="lastRow">终止行索引</param>
        /// <param name="firstCol">起始列索引</param>
        /// <param name="lastCol">终止列索引</param>
        /// <returns>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</returns>
        public bool MergeCell(string sheetName, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            ISheet sheet = GetSheet(sheetName);
            return MergeCell(sheet, firstRow, lastRow, firstCol, lastCol);
        }

        /// <summary>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="firstRow">起始行索引</param>
        /// <param name="lastRow">终止行索引</param>
        /// <param name="firstCol">起始列索引</param>
        /// <param name="lastCol">终止列索引</param>
        /// <returns>根据指定的始终单元在指定的工作表中合并单元格,合并成功返回true,否则返回false</returns>
        public bool MergeCell(ISheet sheet, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            SetErrInfo();
            try
            {
                sheet.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
                return true;
            }
            catch (Exception ex)
            {
                SetErrInfo(true, ex.Message);
                return false;
            }
        }

        #endregion
    }
}
