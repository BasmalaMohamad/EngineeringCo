using Core.Entities.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize { get; set; } = 27;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

        }

        public int? documentId { get; set; }
        public string? productName { get; set; }
        public string? model { get; set; }
        //public string? imageURL { get; set; }
      
        public float? inletSizeTo { get; set; }
        public float? inletSizeFrom { get; set; }
        public float? outletSizeTo { get; set; }
        public float? outletSizeFrom { get; set; }
        public string? construction { get; set; }
        public string sortBy { get; set; } = SortByOptions.Name;
        public string sortDirection { get; set; } = SortByDirections.Asc;

        private string? _searchValue;

        public string SearchValue
        {
            get => (_searchValue is null) ? string.Empty : _searchValue;

            set => _searchValue = value;
        }



    }
}
