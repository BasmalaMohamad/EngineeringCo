using Core.Entities.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class AccessoriesSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize { get; set; } = 27;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? Name { get; set; }
        public string? PumpName { get; set; }
        public int? CategoryId { get; set; }
        public string? Model { get; set; }
        public float? Size { get; set; }
        public string? Construction { get; set; }
        public string sortBy { get; set; } = AccesSortByOptions.PumpName;
        public string sortDirection { get; set; } = SortByDirections.Asc;

        private string? _searchValue;

        public string SearchValue
        {
            get => (_searchValue is null) ? string.Empty : _searchValue;

            set => _searchValue = value;
        }



    }
}
