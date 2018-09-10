using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Collections.Generic;

namespace Kata04
{
    public class MinSpreadCommand : IRequest<MinSpreadResponse>
    {
    }

    public class MinSpreadResponse
    {

    }

    /// <summary>
    /// output the day number (column one) with the smallest temperature spread 
    /// (the maximum temperature is the second column, the minimum the third column
    /// </summary>
    public class MinSpreadHandler : IRequestHandler<MinSpreadCommand, MinSpreadResponse>
    {
        private readonly ILogger _logger;

        public MinSpreadHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task<MinSpreadResponse> Handle(MinSpreadCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public class WeatherData
    {
        public int Day { get; set; }
        public int MxT { get; set; }
        public int MnT { get; set; }
        public int HDDay { get; set; }
        public decimal AvDP { get; set; }
        public int? OneHrP { get; set; }
        public decimal TPcpn { get; set; }
        public string WxType { get; set; }
        public int PDir { get; set; }
        public decimal AvSp { get; set; }
        public int Dir { get; set; }
        public int MxS { get; set; }
        public decimal SkyC { get; set; }
        public int MxR { get; set; }
        public int MnR { get; set; }
        public decimal AvSLP { get; set; }
    }

    

    public class WeatherDataReader
    {

        private class ColumnMap
        {
            public int StartCol { get; set; }
            public int EndCol { get; set; }
            public string ColName { get; set; }
            public Type DataType { get; set; }
        }

        private static Dictionary<string, ColumnMap> ColumnMaps = new Dictionary<string, ColumnMap>
        {
            { "Day", new ColumnMap { ColName = "Day", DataType = typeof(int), StartCol = 0, EndCol = 3}},
            { "MxT", new ColumnMap { ColName = "MxT", DataType = typeof(int), StartCol = 7, EndCol = 7}},
            { "MnT", new ColumnMap { ColName = "MnT", DataType = typeof(int), StartCol = 11, EndCol = 13}},
            { "AvT", new ColumnMap { ColName = "AvT", DataType = typeof(int), StartCol = 11, EndCol = 19}},
            { "HDDay", new ColumnMap { ColName = "HDDay", DataType = typeof(int), StartCol = 17, EndCol = 27}},
            { "AvDP", new ColumnMap { ColName = "AvDP", DataType = typeof(decimal), StartCol = 23, EndCol = 33}},
            { "OneHrP", new ColumnMap { ColName = "OneHrP", DataType = typeof(int?), StartCol = 35, EndCol = 38}},
            { "TPcpn", new ColumnMap { ColName = "TPcpn", DataType = typeof(decimal), StartCol = 40, EndCol = 44}},
            { "WxType", new ColumnMap { ColName = "WxType", DataType = typeof(string), StartCol = 46, EndCol = 51}},
            { "PDir", new ColumnMap { ColName = "PDir", DataType = typeof(int), StartCol = 53, EndCol = 56}},
            { "AvSP", new ColumnMap { ColName = "AvSP", DataType = typeof(decimal), StartCol = 58, EndCol = 61}},
            { "Dir", new ColumnMap { ColName = "Dir", DataType = typeof(int), StartCol = 63, EndCol = 65}},
            { "MxS", new ColumnMap { ColName = "MxS", DataType = typeof(int), StartCol = 67, EndCol = 69}},
            { "SkyC", new ColumnMap { ColName = "SkyC", DataType = typeof(decimal), StartCol = 71, EndCol = 74}},
            { "MxR", new ColumnMap { ColName = "MxR", DataType = typeof(int), StartCol = 76, EndCol = 78}},
            { "MnR", new ColumnMap { ColName = "MnR", DataType = typeof(int), StartCol = 80, EndCol = 82}},
            { "AvSLP", new ColumnMap { ColName = "AvSLP", DataType = typeof(decimal), StartCol = 84, EndCol = 88}}
        };
    }
}